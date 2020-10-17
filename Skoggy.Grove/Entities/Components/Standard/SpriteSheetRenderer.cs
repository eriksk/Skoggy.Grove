using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Components.Base;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class SpriteSheetRenderer : Renderer, IInitialize
    {
        public int SourceIndex;
        public Vector2 Pivot = Vector2.One * 0.5f;
        public SpriteEffects SpriteEffects = SpriteEffects.None;
        public Color Color = Color.White;
        private SpriteSheetComponent _spriteSheet;

        public void Initialize()
        {
            _spriteSheet = GetComponent<SpriteSheetComponent>();
        }

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (_spriteSheet == null) return;

            var sprite = _spriteSheet.SpriteSheet[SourceIndex];
            var origin = new Vector2(sprite.Width, sprite.Height) * Pivot;

            spriteBatch.Draw(
                _spriteSheet.SpriteSheet.Texture,
                Entity.WorldPosition,
                sprite,
                Color,
                Entity.WorldRotation,
                origin,
                Entity.WorldScale,
                SpriteEffects,
                0f);
        }
    }
}
