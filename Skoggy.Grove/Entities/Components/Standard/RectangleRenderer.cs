using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Components.Base;
using Skoggy.Grove.Extensions;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class RectangleRenderer : Renderer
    {
        public float Width;
        public float Height;
        public Color Color = Color.White;

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            var halfSize = new Vector2(Width, Height) * 0.5f;

            var rotation = Matrix.CreateRotationZ(Entity.WorldRotation);

            var topLeft = Entity.WorldPosition + Vector2.Transform(new Vector2(-halfSize.X, -halfSize.Y), rotation);
            var topRight = Entity.WorldPosition + Vector2.Transform(new Vector2(halfSize.X, -halfSize.Y), rotation);
            var bottomLeft = Entity.WorldPosition + Vector2.Transform(new Vector2(-halfSize.X, halfSize.Y), rotation);
            var bottomRight = Entity.WorldPosition + Vector2.Transform(new Vector2(halfSize.X, halfSize.Y), rotation);

            spriteBatch.DrawLine(topLeft, topRight, Color);
            spriteBatch.DrawLine(topRight, bottomRight, Color);
            spriteBatch.DrawLine(bottomRight, bottomLeft, Color);
            spriteBatch.DrawLine(bottomLeft, topLeft, Color);
        }
    }
}
