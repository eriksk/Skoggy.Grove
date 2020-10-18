using System;

namespace Skoggy.Grove.Physics.Shapes
{
    public class Circle : Shape
    {
        public float Radius;

        public Circle(float radius)
        {
            Radius = radius;
            if(radius <= 0f)
            {
                throw new ArgumentException(nameof(radius) + " must be more than zero");
            }
        }

        public override AABB CalculateAABB(Rigidbody body)
            => new AABB()
            {
                MinX = body.Position.X - Radius,
                MaxX = body.Position.X + Radius,
                MinY = body.Position.Y - Radius,
                MaxY = body.Position.Y + Radius
            };
    }
}