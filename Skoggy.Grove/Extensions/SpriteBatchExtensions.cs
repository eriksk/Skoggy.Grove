using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;

namespace Skoggy.Grove.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void FillRect(this SpriteBatch sb, Rectangle rectangle, Color color)
        {
            sb.Draw(GameContext.Pixel, rectangle, color);
        }
        
        public static void FillRect(this SpriteBatch sb, Vector2 position, Vector2 size, Color color)
        {
            var rectangle = new Rectangle(
                (int)(position.X - size.X * 0.5f),
                (int)(position.Y - size.Y * 0.5f),
                (int)size.X,
                (int)size.Y
            );
            sb.Draw(GameContext.Pixel, rectangle, color);
        }
    }
}