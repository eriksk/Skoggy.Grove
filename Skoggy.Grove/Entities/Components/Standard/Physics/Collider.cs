using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Modules;

namespace Skoggy.Grove.Entities.Components.Standard.Physics
{
    public abstract class Collider : Component, IInitialize
    {
        internal Fixture Fixture;
        public float Density = 1f;
        public Vector2 Offset = Vector2.Zero;
        public PhysicsMaterial PhysicsMaterial = PhysicsMaterial.Default;

        public virtual void Initialize()
        {
            var physicsModule = World.GetModule<PhysicsEntityModule>();
            var body = GetComponent<Rigidbody>().Body;

            if(body == null)
            {
                throw new System.Exception("Attaching collider to entity without body");
            }

            Fixture = body.CreateFixture(CreateShape());
            Fixture.Friction = PhysicsMaterial.Friction;
            Fixture.Restitution = PhysicsMaterial.Restitution;
        }

        protected abstract Shape CreateShape();
    }
}