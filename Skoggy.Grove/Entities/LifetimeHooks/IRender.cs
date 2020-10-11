using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Entities.Actions
{
    public interface IRender
    {
        int Layer { get; set; }
        int Order { get; set; }
        void Render(SpriteBatch spriteBatch, GraphicsDevice graphics);
    }
}
