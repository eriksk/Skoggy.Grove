using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Entities.Rendering
{
    public interface IEntityRenderer
    {
        void Render(EntityWorld entityWorld, SpriteBatch spriteBatch, GraphicsDevice graphics);
    }
}
