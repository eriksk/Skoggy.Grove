using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Entities.Factories
{
    public interface IEntityFactory
    {
        Entity Create(string name, Vector2 position = new Vector2(), float rotation = 0f, Vector2? scale = null);
    }
}
