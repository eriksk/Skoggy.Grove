using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;

namespace Skoggy.Grove.Entities.Components.Standard.Physics
{
    public class BoxCollider : Collider
    {
        public float Width = 64f;
        public float Height = 64f;

        protected override Shape CreateShape()
        {
            var vertices = PolygonTools.CreateRectangle(
                ConvertUnits.ToSimUnits(Width * 0.5f),
                ConvertUnits.ToSimUnits(Height * 0.5f),
                ConvertUnits.ToSimUnits(Offset), 
                angle: 0f);

            return new PolygonShape(vertices, Density);
        }
    }
}