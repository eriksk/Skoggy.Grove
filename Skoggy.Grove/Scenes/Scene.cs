
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Scenes
{
    public abstract class Scene : IScene
    {
        protected readonly ISceneManager Manager;

        public Scene(ISceneManager manager)
        {
            Manager = manager;
        }

        public abstract void Load();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}