using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Physics
{
    public static class CollisionResolver
    {
        public static void PositionalCorrection(ref Manifold manifold)
        {
            const float percent = 0.7f; // usually 20% to 80%, correction to avoid sinking
            const float slop = 0.01f; // usually 0.01 to 0.1, used to avoid jitter when always correcting resting bodies

            var correction = (MathF.Max(manifold.Penetration - slop, 0.0f) / (manifold.BodyA.InverseMass + manifold.BodyB.InverseMass)) *
                percent *
                manifold.Normal;

            manifold.BodyA.Position -= manifold.BodyA.InverseMass * correction;
            manifold.BodyB.Position += manifold.BodyB.InverseMass * correction;
        }

        public static void Resolve(ref Manifold manifold)
        {
            // Calculate relative velocity
            var relativeVelocity = manifold.BodyB.Velocity - manifold.BodyA.Velocity;

            // Calculate relative velocity in terms of the normal direction
            var relativeVelocityAlongNormal = Vector2.Dot(relativeVelocity, manifold.Normal);

            // Do not resolve if velocities are separating
            if (relativeVelocityAlongNormal >= 0f)
                return;

            // Calculate restitution
            var restitution = MathF.Min(manifold.ShapeA.Material.Restitution, manifold.ShapeB.Material.Restitution);

            // Calculate impulse scalar
            var impulseScalar = -(1f + restitution) * relativeVelocityAlongNormal;
            impulseScalar = impulseScalar / (manifold.BodyA.InverseMass + manifold.BodyB.InverseMass);

            // Apply impulse
            var impulse = impulseScalar * manifold.Normal;

            // TODO: Inline function
            var frictionImpulse = CalculateFriction(ref manifold, relativeVelocity, impulseScalar);

            // TODO: impulse + frictionImpulse is a guess. But it seems to work
            manifold.BodyA.Velocity -= manifold.BodyA.InverseMass * (impulse + frictionImpulse);
            manifold.BodyB.Velocity += manifold.BodyB.InverseMass * (impulse + frictionImpulse);
        }

        private static Vector2 CalculateFriction(ref Manifold manifold, Vector2 relativeVelocity, float impulseScalar)
        {
            // Solve for the tangent vector
            var tangent = relativeVelocity - Vector2.Dot(relativeVelocity, manifold.Normal) * manifold.Normal;
            if (tangent == Vector2.Zero) return Vector2.Zero;
            tangent.Normalize();

            // Solve for magnitude to apply along the friction vector
            var tangentScalar = -Vector2.Dot(relativeVelocity, tangent);
            tangentScalar = tangentScalar / (manifold.BodyA.InverseMass + manifold.BodyB.InverseMass);

            // PythagoreanSolve = A^2 + B^2 = C^2, solving for C given A and B
            // Use to approximate mu given friction coefficients of each body
            var staticFriction = CalculateFriction(manifold.ShapeA.Material.StaticFriction, manifold.ShapeB.Material.StaticFriction);

            // Clamp magnitude of friction and create impulse vector
            if (MathF.Abs(tangentScalar) < impulseScalar * staticFriction)
            {
                return tangentScalar * tangent;
            }
            else
            {
                var dynamicFriction = CalculateFriction(manifold.ShapeA.Material.DynamicFriction, manifold.ShapeB.Material.DynamicFriction);
                return -impulseScalar * tangent * dynamicFriction;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CalculateFriction(float frictionA, float frictionB)
        {
            // Just get the average, there are probably more realistic scenarios but this is probably good enough
            return (frictionA + frictionB) / 2f;
        }
    }
}