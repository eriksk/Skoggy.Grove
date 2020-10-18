using System;
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
            if (relativeVelocityAlongNormal > 0)
                return;

            // Calculate restitution
            var restitution = MathF.Min(manifold.ShapeA.Material.Restitution, manifold.ShapeB.Material.Restitution);

            // Calculate impulse scalar
            var impulseScalar = -(1f + restitution) * relativeVelocityAlongNormal;
            impulseScalar = impulseScalar / (manifold.BodyA.InverseMass + manifold.BodyB.InverseMass);

            // Apply impulse
            var impulse = impulseScalar * manifold.Normal;
            
            manifold.BodyA.Velocity -= manifold.BodyA.InverseMass * impulse;
            manifold.BodyB.Velocity += manifold.BodyB.InverseMass * impulse;
        }
    }
}