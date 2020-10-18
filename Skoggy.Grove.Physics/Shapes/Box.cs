using System;

namespace Skoggy.Grove.Physics.Shapes
{
    public class Box : Shape
    {
        public float Width;
        public float Height;

        public Box(float width, float height)
        {
            if(width <= 0f) throw new ArgumentException(nameof(width) + " must be more than zero");
            if(height <= 0f) throw new ArgumentException(nameof(height) + " must be more than zero");

            Width = width;
            Height = height;
        }

        public override AABB CalculateAABB(Rigidbody body) =>
            new AABB()
            {
                MinX = body.Position.X - Width / 2f,
                MaxX = body.Position.X + Width / 2f,
                MinY = body.Position.Y - Height / 2f,
                MaxY = body.Position.Y + Height / 2f
            };
    }
}