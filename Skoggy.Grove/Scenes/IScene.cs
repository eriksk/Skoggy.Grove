
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Scenes
{
    public interface IScene
    {
        void Load();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}