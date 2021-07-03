using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Components.Base;
using Skoggy.Grove.Extensions;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class LineRenderer : Renderer
    {
        public Vector2 Start;
        public Vector2 End;
        public Color Color = Color.White;
        public int Thickness = 1;

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
            => spriteBatch.DrawLine(Start, End, Color, Thickness);
    }
}
