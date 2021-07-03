using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Timers;

namespace Skoggy.Grove.Diagnostics
{
    public class GameDiagnostics
    {
        private SpriteFont _font;
        private Queue<float> _deltaTimeQueue;
        private int _currentFrame;
        private SpriteBatch _spriteBatch;

        public GameDiagnostics()
        {
            _deltaTimeQueue = new Queue<float>(120);
        }

        public void Load()
        {
            _font = GameContext.Content.Load<SpriteFont>(@"fonts/debug");
            _spriteBatch = new SpriteBatch(GameContext.Graphics);
        }

        public void Update(GameTime gameTime)
        {
            _currentFrame++;
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_currentFrame % 20 == 0)
            {
                _deltaTimeQueue.Enqueue(deltaTime);
            }
            while (_deltaTimeQueue.Count > 120)
            {
                _deltaTimeQueue.Dequeue();
            }
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            var offset = 0;
            offset = DrawLabelValue(offset, _spriteBatch, _font, "FPS:", Time.FPS.ToString());
            // offset = DrawLabelValue(offset, _spriteBatch, _font, "Resolution:", Constants.GetNumberOrDefault(_context.Graphics.Viewport.Width) + "X" + Constants.GetNumberOrDefault(_context.Graphics.Viewport.Height));
            // offset = DrawLabelValue(offset, _spriteBatch, _font, "GPU:", _context.Graphics.Adapter.Description);
            // offset = DrawLabelValue(offset, _spriteBatch, _font, "Sprites:", Constants.GetNumberOrDefault(_context.Graphics.Metrics.SpriteCount));
            // offset = DrawLabelValue(offset, _spriteBatch, _font, "Draw Calls:", Constants.GetNumberOrDefault(_context.Graphics.Metrics.DrawCount));
            _spriteBatch.End();
        }

        private int DrawLabelValue(int xOffset, SpriteBatch sb, SpriteFont font, string label, string value)
        {
            var spacing = 6;
            var padding = 2;

            var labelSize = font.MeasureString(label);
            var valueSize = font.MeasureString(value);

            var fullSize = new Vector2(labelSize.X + valueSize.X + spacing + padding + padding, labelSize.Y + padding);

            sb.Draw(GameContext.Pixel, new Rectangle(xOffset + padding, padding, (int)fullSize.X, (int)fullSize.Y), new Color(0, 0, 0, 200));
            sb.DrawString(_font, label, new Vector2(xOffset + padding * 2, padding * 2), new Color(180, 180, 180, 255));
            sb.DrawString(_font, value, new Vector2(xOffset + spacing + padding * 2 + labelSize.X, padding * 2), Color.White);

            return (xOffset + (int)fullSize.X) + 4;
        }
    }
}