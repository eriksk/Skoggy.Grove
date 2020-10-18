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

    public class AABB : Shape
    {
        public float Width;
        public float Height;
    }
}