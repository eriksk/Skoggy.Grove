using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Skoggy.Grove.Contexts
{
    public class GameContext
    {
        public static ContentManager Content { get; private set; }
        public static GraphicsDevice Graphics { get; private set; }
        public static Texture2D Pixel { get; private set; }
        public static Point Resolution { get; private set; }
        public static Point RenderResolution { get; private set; }
        private static Game _game;

        public static void Initialize(
            Game game, 
            ContentManager content, 
            GraphicsDevice graphics,
            Point renderResolution)
        {
            _game = game;
            Content = content;
            Graphics = graphics;
            Pixel = new Texture2D(graphics, 1, 1);
            Pixel.SetData(new[] { Color.White });
            RenderResolution = renderResolution;

            Resolution = new Point(Graphics.Viewport.Width, Graphics.Viewport.Height);
            game.Window.ClientSizeChanged += ResolutionChanged;
        }

        private static void ResolutionChanged(object sender, EventArgs e)
        {
            Resolution = new Point(Graphics.Viewport.Width, Graphics.Viewport.Height);
        }
    }
}