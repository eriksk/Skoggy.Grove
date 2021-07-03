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
        public static event Action OnResolutionChanged;
        private static Game _game;
        private static bool _scaleRenderResolution;

        public static void Initialize(
            Game game,
            ContentManager content,
            GraphicsDevice graphics,
            Point? renderResolution)
        {
            _game = game;
            Content = content;
            Graphics = graphics;
            Pixel = new Texture2D(graphics, 1, 1);
            Pixel.SetData(new[] { Color.White });

            _scaleRenderResolution = !renderResolution.HasValue;
            if (!_scaleRenderResolution)
            {
                RenderResolution = renderResolution.Value;
            }
            else
            {
                RenderResolution = new Point(Graphics.Viewport.Width, Graphics.Viewport.Height);
            }
            Resolution = new Point(Graphics.Viewport.Width, Graphics.Viewport.Height);
            game.Window.ClientSizeChanged += ResolutionChanged;
        }

        internal static Vector2 ScreenPointToRenderPoint(Vector2 screenPoint)
        {
            var scaleRatio = RenderResolution.ToVector2() / Resolution.ToVector2();
            var scale = scaleRatio.X > scaleRatio.Y ? scaleRatio.X : scaleRatio.Y;

            float renderWidth = GameContext.RenderResolution.X / scale;
            float renderHeight = GameContext.RenderResolution.Y / scale;

            var divisor = new Vector2(
                renderWidth / RenderResolution.X,
                renderHeight / RenderResolution.Y
            );
            return screenPoint / divisor;
        }

        private static void ResolutionChanged(object sender, EventArgs e)
        {
            Resolution = new Point(Graphics.Viewport.Width, Graphics.Viewport.Height);
            if(_scaleRenderResolution)
            {
                RenderResolution = Resolution;
            }
            OnResolutionChanged?.Invoke();
        }
    }
}