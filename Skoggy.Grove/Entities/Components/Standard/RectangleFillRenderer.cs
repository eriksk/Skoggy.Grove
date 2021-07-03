using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Entities.Components.Base;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class RectangleFillRenderer : Renderer
    {
        public float Width = 64;
        public float Height = 64;
        public Color Color = Color.White;

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            var halfSize = new Vector2(Width, Height) * 0.5f;

            var rotation = Matrix.CreateRotationZ(Entity.WorldRotation);
            var position = Entity.WorldPosition;

            spriteBatch.Draw(GameContext.Pixel, 
                position,
                new Rectangle(0, 0, (int)Width, (int)Height),
                Color,
                Entity.WorldRotation,
                halfSize,
                Entity.WorldScale,
                SpriteEffects.None, 0f);
        }
    }
}
