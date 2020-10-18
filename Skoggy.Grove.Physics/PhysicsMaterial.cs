namespace Skoggy.Grove.Physics
{
    public struct PhysicsMaterial
    {
        public float Restitution;
        public float Friction;
        public float Density;

        // TODO: Friction for these
        public static PhysicsMaterial Rock => new PhysicsMaterial() { Density = 0.6f, Restitution = 0.1f };
        public static PhysicsMaterial Wood => new PhysicsMaterial() { Density = 0.3f, Restitution = 0.2f };
        public static PhysicsMaterial Metal => new PhysicsMaterial() { Density = 1.2f, Restitution = 0.05f };
        public static PhysicsMaterial BouncyBall => new PhysicsMaterial() { Density = 0.3f, Restitution = 0.8f };
        public static PhysicsMaterial SuperBall => new PhysicsMaterial() { Density = 0.3f, Restitution = 0.95f };
        public static PhysicsMaterial Pillow => new PhysicsMaterial() { Density = 0.1f, Restitution = 0.2f };
        public static PhysicsMaterial Static => new PhysicsMaterial() { Density = 0.0f, Restitution = 0.4f };
    }
}