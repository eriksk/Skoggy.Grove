namespace Skoggy.Grove.Physics
{
    public struct PhysicsMaterial
    {
        public float Restitution;
        public float Friction;
        public float Density;

        public float StaticFriction;
        public float DynamicFriction;

        // TODO: Friction for these
        public static PhysicsMaterial Rock => new PhysicsMaterial()
        {
            Density = 0.6f,
            Restitution = 0.1f,
            StaticFriction = 0.2f,
            DynamicFriction = 0.5f,
        };
        public static PhysicsMaterial Wood => new PhysicsMaterial()
        {
            Density = 0.3f,
            Restitution = 0.2f,
            StaticFriction = 0.05f,
            DynamicFriction = 0.4f,
        };
        public static PhysicsMaterial Metal => new PhysicsMaterial()
        {
            Density = 1.2f,
            Restitution = 0.05f,
            StaticFriction = 0.2f,
            DynamicFriction = 0.5f,
        };
        public static PhysicsMaterial BouncyBall => new PhysicsMaterial()
        {
            Density = 0.3f,
            Restitution = 0.8f,
            StaticFriction = 0.2f,
            DynamicFriction = 0.5f,
        };
        public static PhysicsMaterial SuperBall => new PhysicsMaterial()
        {
            Density = 0.3f,
            Restitution = 0.95f,
            StaticFriction = 0.2f,
            DynamicFriction = 0.5f,
        };
        public static PhysicsMaterial Pillow => new PhysicsMaterial()
        {
            Density = 0.1f,
            Restitution = 0.2f,
            StaticFriction = 0.2f,
            DynamicFriction = 0.5f,
        };
        public static PhysicsMaterial Static => new PhysicsMaterial()
        {
            Density = 0.0f,
            Restitution = 0.4f,
            StaticFriction = 0.2f,
            DynamicFriction = 0.5f,
        };
    }
}