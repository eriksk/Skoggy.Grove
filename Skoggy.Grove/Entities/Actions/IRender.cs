using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Entities.Actions
{
    public interface IRender
    {
        void Render(SpriteBatch spriteBatch, GraphicsDevice graphics);
    }
}
