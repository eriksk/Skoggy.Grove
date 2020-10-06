using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Textures;

namespace Skoggy.Grove.Samples.Samples
{
    public class SpriteSheetSample : Sample
    {
        private SpriteSheet _spriteSheet;

        public SpriteSheetSample(SampleGame game)
            : base(game)
        {
            // TODO: add the sprite sheet png to content
        }

        public override void Load()
        {
            _spriteSheet = new SpriteSheet(Content.Load<Texture2D>("Graphics/sprite_sheet"), 32, 32);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            
            // Get the source by index
            var source1 = _spriteSheet[0];
            
            SpriteBatch.Draw(
                _spriteSheet.Texture,
                Vector2.Zero,
                source1,
                Color.White);

            // Get the source by column/row
            var source2 = _spriteSheet[1, 0];
            
            SpriteBatch.Draw(
                _spriteSheet.Texture,
                new Vector2(_spriteSheet.CellWidth, 0f),
                source2,
                Color.White);

            SpriteBatch.End();
        }
    }
}