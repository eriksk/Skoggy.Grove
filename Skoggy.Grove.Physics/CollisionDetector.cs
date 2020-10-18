using System;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Physics.Shapes;

namespace Skoggy.Grove.Physics
{
    public static class CollisionDetector
    {
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

        public static bool AABBvsAABB(
            Rigidbody bodyA,
            Rigidbody bodyB,
            AABB aabbA,
            AABB aabbB,
            out Manifold manifold)
        {
            var directionVector = bodyB.Position - bodyA.Position;

            var aboxMin = new Vector2(
                bodyA.Position.X - aabbA.Width / 2f,
                bodyA.Position.Y - aabbA.Height / 2f
            );
            var aboxMax = new Vector2(
                bodyA.Position.X + aabbA.Width / 2f,
                bodyA.Position.Y + aabbA.Height / 2f
            );
            var bboxMin = new Vector2(
                bodyB.Position.X - aabbB.Width / 2f,
                bodyB.Position.Y - aabbB.Height / 2f
            );
            var bboxMax = new Vector2(
                bodyB.Position.X + aabbB.Width / 2f,
                bodyB.Position.Y + aabbB.Height / 2f
            );

            // Calculate half extents along x axis for each object
            var aExtentX = (aboxMax.X - aboxMin.X) / 2f;
            var bExtentX = (bboxMax.X - bboxMin.X) / 2f;

            // Calculate overlap on x axis
            var xOverlap = aExtentX + bExtentX - MathF.Abs(directionVector.X);

            // SAT test on x axis
            if (xOverlap <= 0f)
            {
                manifold = Manifold.Invalid;
                return false;
            }

            // Calculate half extents along y axis for each object
            var aExtentY = (aboxMax.Y - aboxMin.Y) / 2f;
            var bExtentY = (bboxMax.Y - bboxMin.Y) / 2f;

            // Calculate overlap on y axis
            var yOverlap = aExtentY + bExtentY - MathF.Abs(directionVector.Y);

            // SAT test on y axis
            if (yOverlap <= 0f)
            {
                manifold = Manifold.Invalid;
                return false;
            }

            // Find out which axis is axis of least penetration
            if (xOverlap > yOverlap)
            {
                // Point towards B knowing that n points from A to B
                var normal = new Vector2(directionVector.X < 0f ? -1f : 1f, 0f);
                manifold = new Manifold(bodyA, bodyB, aabbA, aabbB, normal, xOverlap);
            }
            else
            {
                // Point toward B knowing that n points from A to B
                var normal = new Vector2(0f, directionVector.Y < 0f ? -1f : 1f);
                manifold = new Manifold(bodyA, bodyB, aabbA, aabbB, normal, yOverlap);
            }
            return true;
        }

        public static bool AABBvsCircle(
            Rigidbody bodyA,
            Rigidbody bodyB,
            AABB aabbA,
            Circle circleB,
            out Manifold manifold)
        {
            // Vector from A to B
            var directionVector = bodyB.Position - bodyA.Position;

            // Closest point on A to center of B
            var closest = directionVector;

            var aboxMin = new Vector2(
                bodyA.Position.X - aabbA.Width / 2f,
                bodyA.Position.Y - aabbA.Height / 2f
            );
            var aboxMax = new Vector2(
                bodyA.Position.X + aabbA.Width / 2f,
                bodyA.Position.Y + aabbA.Height / 2f
            );

            // Calculate half extents along each axis
            var xExtent = (aboxMax.X - aboxMin.X) / 2f;
            var yExtent = (aboxMax.Y - aboxMin.Y) / 2f;

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
                manifold = new Manifold(bodyA, bodyB, aabbA, circleB, -directionVector, circleRadius - distance);
                return true;
            }
            else
            {
                manifold = new Manifold(bodyA, bodyB, aabbA, circleB, directionVector, circleRadius - distance);
                return true;
            }
        }
    }
}