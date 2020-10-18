namespace Skoggy.Grove.Physics.Shapes
{
    public class Shape
    {
        public float Restitution;
    }

    public class Circle : Shape
    {
        public float Radius;
    }

    public class Box : Shape
    {
        public float Width;
        public float Height;

        public AABB CalculateAABB(Rigidbody body) =>
            new AABB()
            {
                MinX = body.Position.X - Width / 2f,
                MaxX = body.Position.X + Width / 2f,
                MinY = body.Position.Y - Height / 2f,
                MaxY = body.Position.Y + Height / 2f
            };
    }
}