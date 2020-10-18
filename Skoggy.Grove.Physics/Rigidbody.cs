using Microsoft.Xna.Framework;
using Skoggy.Grove.Physics.Shapes;

namespace Skoggy.Grove.Physics
{
    public class Rigidbody
    {
        private static int _idCounter;
        internal int Id = _idCounter++;

        public Vector2 Position;
        public float Rotation;
        public Vector2 Velocity;
        public float AngularVelocity;
        public Vector2 Force;
        public float Mass;
        public float InverseMass => Mass <= 0f ? 0f : 1f / Mass;
        public float Inertia;
        public float InverseInertia => Inertia <= 0f ? 0f : 1f - Inertia;

        // TODO: Multiple shapes
        public Shape Shape;
    }
}