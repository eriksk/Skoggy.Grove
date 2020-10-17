using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Components.Base;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class TextRenderer : Renderer
    {
        public SpriteFont Font;
        public string Text;
        public Vector2 Pivot = Vector2.One * 0.5f;
        public SpriteEffects SpriteEffects = SpriteEffects.None;
        public Color Color = Color.White;

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (Font == null) return;
            if(Text == null) return;
            if(Text == string.Empty) return;

            var origin = Font.MeasureString(Text) * Pivot;

            spriteBatch.DrawString(
                Font,
                Text,
                Entity.WorldPosition,
                Color,
                Entity.WorldRotation,
                origin,
                Entity.WorldScale,
                SpriteEffects,
                0f);
        }

    }
}
