using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;

namespace Skoggy.Grove.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void DrawLine(this SpriteBatch sb, Vector2 start, Vector2 end, Color color)
        {
            var direction = (end - start);
            direction.Normalize();
            var distance = Vector2.Distance(start, end);

            sb.Draw(
                GameContext.Pixel,
                start,
                null,
                color,
                direction.ToAngle(),
                new Vector2(0f, 0.5f),
                new Vector2(distance, 1f),
                SpriteEffects.None, 0f);
        }

        public static void DrawShadowed(this SpriteBatch sb,
            Texture2D texture,
            Vector2 position,
            Rectangle source,
            Color color,
            Color shadowColor,
            float rotation,
            Vector2 origin,
            Vector2 scale)
        {
            sb.Draw(
                texture,
                position + new Vector2(1, 1),
                source,
                shadowColor,
                rotation,
                origin,
                scale,
                SpriteEffects.None, 0f);

            sb.Draw(
                texture,
                position,
                source,
                color,
                rotation,
                origin,
                scale,
                SpriteEffects.None, 0f);
        }

        public static void DrawStringPivotShadowed(this SpriteBatch sb,
            SpriteFont font,
            string text,
            Vector2 position,
            Color color,
            Color shadowColor,
            Vector2 pivot,
            float rotation,
            float scale)
        {
            var origin = font.MeasureString(text) * pivot;

            sb.DrawString(font,
                text,
                position + new Vector2(1, 1),
                shadowColor,
                rotation,
                origin,
                scale,
                SpriteEffects.None, 0f);

            sb.DrawString(font,
                text,
                position,
                color,
                rotation,
                origin,
                scale,
                SpriteEffects.None, 0f);
        }

        public static void DrawStringPivot(this SpriteBatch sb,
            SpriteFont font,
            string text,
            Vector2 position,
            Color color,
            Vector2 pivot,
            float rotation,
            float scale)
        {
            var origin = font.MeasureString(text) * pivot;

            sb.DrawString(font,
                text,
                position,
                color,
                rotation,
                origin,
                scale,
                SpriteEffects.None, 0f);
        }

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