using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Components.Base;
using Skoggy.Grove.Extensions;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class RectangleRenderer : Renderer
    {
        public float Width = 64;
        public float Height = 64;
        public Color Color = Color.White;
        public float Thickness = 1f;

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            var halfSize = new Vector2(Width, Height) * 0.5f;

            var rotation = Matrix.CreateRotationZ(Entity.WorldRotation);

            var scale = Entity.WorldScale;

            var topLeft = Entity.WorldPosition + Vector2.Transform(new Vector2(-halfSize.X, -halfSize.Y) * scale, rotation);
            var topRight = Entity.WorldPosition + Vector2.Transform(new Vector2(halfSize.X, -halfSize.Y) * scale, rotation);
            var bottomLeft = Entity.WorldPosition + Vector2.Transform(new Vector2(-halfSize.X, halfSize.Y) * scale, rotation);
            var bottomRight = Entity.WorldPosition + Vector2.Transform(new Vector2(halfSize.X, halfSize.Y) * scale, rotation);

            spriteBatch.DrawLine(topLeft, topRight, Color, Thickness);
            spriteBatch.DrawLine(topRight, bottomRight, Color, Thickness);
            spriteBatch.DrawLine(bottomRight, bottomLeft, Color, Thickness);
            spriteBatch.DrawLine(bottomLeft, topLeft, Color, Thickness);
        }
    }
}
