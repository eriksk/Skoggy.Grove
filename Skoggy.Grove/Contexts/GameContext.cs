using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Contexts
{
    public class GameContext
    {
        public static ContentManager Content { get; private set; }
        public static GraphicsDevice Graphics { get; private set; }
        public static Texture2D Pixel { get; private set; }

        public static void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            Content = content;
            Graphics = graphics;
            Pixel = new Texture2D(graphics, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }
    }
}