using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Actions;

namespace Skoggy.Grove.Entities.Components.Base
{
    public abstract class Renderer : Component, IRender
    {
        public int Layer { get; set; }
        public int Order { get; set; }

        public abstract void Render(SpriteBatch spriteBatch, GraphicsDevice graphics);
    }
}
