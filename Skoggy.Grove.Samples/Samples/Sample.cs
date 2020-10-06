using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Samples.Samples
{
    public abstract class Sample
    {
        public readonly ContentManager Content;
        public readonly GraphicsDevice Graphics;
        public readonly SpriteBatch SpriteBatch;

        public Sample(SampleGame game)
        {
            Content = game.Content;
            Graphics = game.GraphicsDevice;
            SpriteBatch = new SpriteBatch(Graphics);
        }

        public virtual void Load() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
    }
}