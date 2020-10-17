using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;

namespace Skoggy.Grove.Entities.Components.Standard.Physics
{
    public class CircleCollider : Collider
    {
        public float Radius = 64f;
        public int Edges = 16;

        protected override Shape CreateShape()
            => new CircleShape(ConvertUnits.ToSimUnits(Radius), 1f);
    }
}