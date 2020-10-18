using Microsoft.Xna.Framework;
using Skoggy.Grove.Physics.Shapes;

namespace Skoggy.Grove.Physics
{
    public struct Manifold
    {
        public Rigidbody BodyA;
        public Rigidbody BodyB;
        public Shape ShapeA;
        public Shape ShapeB;
        public Vector2 Normal;
        public float Penetration;
        public bool Valid;

        public Manifold(
            Rigidbody bodyA,
            Rigidbody bodyB,
            Shape shapeA,
            Shape shapeB,
            Vector2 normal,
            float penetration)
        {
            BodyA = bodyA ?? throw new System.ArgumentNullException(nameof(bodyA));
            BodyB = bodyB ?? throw new System.ArgumentNullException(nameof(bodyB));
            ShapeA = shapeA ?? throw new System.ArgumentNullException(nameof(shapeA));
            ShapeB = shapeB ?? throw new System.ArgumentNullException(nameof(shapeB));
            Normal = normal;
            Penetration = penetration;
            Valid = true;
        }

        public static Manifold Invalid => new Manifold() { Valid = false };
    }
}