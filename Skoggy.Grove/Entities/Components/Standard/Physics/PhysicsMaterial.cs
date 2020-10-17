namespace Skoggy.Grove.Entities.Components.Standard.Physics
{
    public struct PhysicsMaterial
    {
        public float Friction;
        public float Restitution;

        public static PhysicsMaterial Default => new PhysicsMaterial()
        {
            Friction = 0.6f,
            Restitution = 0.2f
        };
        
        public static PhysicsMaterial Zero => new PhysicsMaterial()
        {
            Friction = 0f,
            Restitution = 0f
        };
    }
}