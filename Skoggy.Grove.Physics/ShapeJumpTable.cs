using Skoggy.Grove.Physics.Shapes;

namespace Skoggy.Grove.Physics
{
    internal class ShapeJumpTable
    {
        // TODO: make actual jump table, can we even do that in C#?
        
        public bool DetectCollision(Rigidbody bodyA, Rigidbody bodyB, Shape shapeA, Shape shapeB, out Manifold manifold)
        {
            // Box vs Box
            {
                if (shapeA is Box boxA && shapeB is Box boxB)
                {
                    if (CollisionDetector.BoxVsBox(bodyA, bodyB, boxA, boxB, out manifold))
                    {
                        return true;
                    }
                    return false;
                }
            }

            // Box vs Circle
            {
                if (shapeA is Box boxA && shapeB is Circle circleB)
                {
                    if (CollisionDetector.BoxvsCircle(bodyA, bodyB, boxA, circleB, out manifold))
                    {
                        return true;
                    }
                    return false;
                }
            }

            // Circle vs Box
            {
                if (shapeA is Circle circleA && shapeB is Box boxB)
                {
                    if (CollisionDetector.BoxvsCircle(bodyB, bodyA, boxB, circleA, out manifold))
                    {
                        return true;
                    }
                    return false;
                }
            }

            // Circle vs Circle
            {
                if (shapeA is Circle circleA && shapeB is Circle circleB)
                {
                    if (CollisionDetector.CircleVsCircle(bodyA, bodyB, circleA, circleB, out manifold))
                    {
                        return true;
                    }
                    return false;
                }
            }

            manifold = Manifold.Invalid;
            return false;
        }
    }
}