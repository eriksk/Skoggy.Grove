using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Textures;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class SpriteSheetComponent : Component, IInitialize
    {
        public string Path;
        public int CellSize;
        public SpriteSheet SpriteSheet;

        public void Initialize()
        {
            SpriteSheet = new SpriteSheet(
                GameContext.Content.Load<Texture2D>(Path),
                CellSize);
        }
    }
}
