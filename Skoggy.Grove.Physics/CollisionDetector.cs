using System;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Physics.Shapes;

namespace Skoggy.Grove.Physics
{
    public static class CollisionDetector
    {
        public static bool Intersect(ref AABB a, ref AABB b)
        {
            if (a.MaxX < b.MinX) return false;
            if (b.MaxX < a.MinX) return false;
            if (a.MaxY < b.MinY) return false;
            if (b.MaxY < a.MinY) return false;

            return true;
        }

        public static bool CircleVsCircle(
            Rigidbody bodyA,
            Rigidbody bodyB,
            Circle circleA,
            Circle circleB,
            out Manifold manifold)
        {
            var normal = bodyB.Position - bodyA.Position;
            var radiusSquared = circleA.Radius + circleB.Radius;
            radiusSquared *= radiusSquared;

            if (normal.LengthSquared() > radiusSquared)
            {
                manifold = Manifold.Invalid;
                return false;
            }

            // Circles have collided
            var distance = normal.Length();

            if (distance > 0f) // Actual valid collision
            {
                normal.Normalize();
                manifold = new Manifold(bodyA, bodyB, circleA, circleB, normal, radiusSquared - distance);
            }
            else // Circles are on same position
            {
                manifold = new Manifold(bodyA, bodyB, circleA, circleB, new Vector2(1f, 0f), circleA.Radius);
            }

            return true;
        }

        public static bool BoxVsBox(
            Rigidbody bodyA,
            Rigidbody bodyB,
            Box boxA,
            Box boxB,
            out Manifold manifold)
        {
            var directionVector = bodyB.Position - bodyA.Position;

            var aabbA = boxA.CalculateAABB(bodyA);
            var aabbB = boxB.CalculateAABB(bodyB);

            // Calculate half extents along x axis for each object
            var aExtentX = (aabbA.MaxX - aabbA.MinX) / 2f;
            var bExtentX = (aabbB.MaxX - aabbB.MinX) / 2f;

            // Calculate overlap on x axis
            var xOverlap = aExtentX + bExtentX - MathF.Abs(directionVector.X);

            // SAT test on x axis
            if (xOverlap <= 0f)
            {
                manifold = Manifold.Invalid;
                return false;
            }

            // Calculate half extents along y axis for each object
            var aExtentY = (aabbA.MaxY - aabbA.MinY) / 2f;
            var bExtentY = (aabbB.MaxY - aabbB.MinY) / 2f;

            // Calculate overlap on y axis
            var yOverlap = aExtentY + bExtentY - MathF.Abs(directionVector.Y);

            // SAT test on y axis
            if (yOverlap <= 0f)
            {
                manifold = Manifold.Invalid;
                return false;
            }

            // Find out which axis is axis of least penetration
            if (xOverlap < yOverlap)
            {
                // Point towards B knowing that n points from A to B
                var normal = new Vector2(directionVector.X < 0f ? -1f : 1f, 0f);
                manifold = new Manifold(bodyA, bodyB, boxA, boxB, normal, xOverlap);
            }
            else
            {
                // Point toward B knowing that n points from A to B
                var normal = new Vector2(0f, directionVector.Y < 0f ? -1f : 1f);
                manifold = new Manifold(bodyA, bodyB, boxA, boxB, normal, yOverlap);
            }
            return true;
        }

        public static bool BoxvsCircle(
            Rigidbody bodyA,
            Rigidbody bodyB,
            Box boxA,
            Circle circleB,
            out Manifold manifold)
        {
            // Vector from A to B
            var directionVector = bodyB.Position - bodyA.Position;

            // Closest point on A to center of B
            var closest = directionVector;

            var aabbA = boxA.CalculateAABB(bodyA);

            // Calculate half extents along each axis
            var xExtent = (aabbA.MaxX - aabbA.MinX) / 2f;
            var yExtent = (aabbA.MaxY - aabbA.MinY) / 2f;

            // Clamp point to edges of the AABB
            closest.X = MathHelper.Clamp(closest.X, -xExtent, xExtent);
            closest.Y = MathHelper.Clamp(closest.Y, -yExtent, yExtent);

            var inside = false;

            // Circle is inside the AABB, so we need to clamp the circle's center to the closest edge
            if (directionVector == closest)
            {
                inside = true;

                // Find closest axis
                if (MathF.Abs(directionVector.X) > MathF.Abs(directionVector.Y))
                {
                    // Clamp to closest extent
                    if (closest.X > 0f)
                    {
                        closest.X = xExtent;
                    }
                    else
                    {
                        closest.X = -xExtent;
                    }
                }
                else // y axis is shorter
                {
                    // Clamp to closest extent
                    if (closest.Y > 0f)
                    {
                        closest.Y = yExtent;
                    }
                    else
                    {
                        closest.Y = -yExtent;
                    }
                }
            }

            var normal = directionVector - closest;
            var distance = normal.LengthSquared();
            var circleRadius = circleB.Radius;

            // Early out of the radius is shorter than distance to closest point and circle not inside the AABB
            if (distance > circleRadius * circleRadius && !inside)
            {
                manifold = Manifold.Invalid;
                return false;
            }

            // Avoided sqrt until we needed
            distance = MathF.Sqrt(distance);

            if (inside)
            {
                // Collision normal needs to be flipped to point outside if circle was inside the AABB
                manifold = new Manifold(bodyA, bodyB, boxA, circleB, -directionVector, circleRadius - distance);
                return true;
            }
            else
            {
                manifold = new Manifold(bodyA, bodyB, boxA, circleB, directionVector, circleRadius - distance);
                return true;
            }
        }
    }
}