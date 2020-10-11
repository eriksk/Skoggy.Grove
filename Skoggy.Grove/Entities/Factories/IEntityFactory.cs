using Microsoft.Xna.Framework;
using System;

namespace Skoggy.Grove.Entities.Factories
{
    public interface IEntityFactory
    {
        Entity Create(string name, Vector2 position, float rotation, Vector2 scale);
    }
}
