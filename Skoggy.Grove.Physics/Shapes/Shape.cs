namespace Skoggy.Grove.Physics.Shapes
{
    public abstract class Shape
    {
        public PhysicsMaterial Material;

        private static int _idCounter;
        internal int Id = _idCounter++;

        public int Layer = int.MaxValue;

        public abstract AABB CalculateAABB(Rigidbody body);
    }
}