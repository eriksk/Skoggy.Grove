using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Physics
{
    public class Rigidbody
    {
        public Vector2 Position;
        public Vector2 Velocity;

        public float Mass;
        public float InverseMass => Mass <= 0f ? 0f : 1f / Mass;

    }
}