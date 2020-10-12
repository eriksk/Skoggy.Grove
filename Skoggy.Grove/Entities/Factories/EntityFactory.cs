using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Entities.Factories
{
    public abstract class EntityFactory : IEntityFactory
    {
        protected readonly EntityWorld World;

        protected EntityFactory(EntityWorld world)
        {
            World = world;
        }

        public Entity Create(string name, Vector2 position, float rotation, Vector2 scale)
        {
            var entity = World.AddEntity(name);
            entity.LocalPosition = position;
            entity.LocalRotation = rotation;
            entity.LocalScale = scale;
            AddComponents(entity);
            return entity;
        }

        protected abstract void AddComponents(Entity entity);
    }
}
