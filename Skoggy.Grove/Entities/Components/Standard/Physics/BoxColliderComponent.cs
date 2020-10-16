using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Modules;

namespace Skoggy.Grove.Entities.Components.Standard.Physics
{
    public class BoxColliderComponent : Component, IInitialize
    {
        public Fixture Fixture;

        public float Width = 64f;
        public float Height = 64f;
        public float Density = 1f;
        public Vector2 Offset = Vector2.Zero;

        public void Initialize()
        {
            var physicsModule = World.GetModule<PhysicsEntityModule>();
            var body = GetComponent<RigidbodyComponent>().Body;

            var vertices = PolygonTools.CreateRectangle(
                ConvertUnits.ToSimUnits(Width * 0.5f),
                ConvertUnits.ToSimUnits(Height * 0.5f),
                ConvertUnits.ToSimUnits(Offset), 
                angle: 0f);

            // TODO: this doesn't show up in the debugger
            Fixture = body.CreateFixture(new PolygonShape(vertices, Density));
        }
    }
}