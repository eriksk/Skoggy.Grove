using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Entities.Factories
{
    public abstract class EntityFactory : IEntityFactory
    {
        private readonly EntityWorld _world;

        protected EntityFactory(EntityWorld world)
        {
            _world = world;
        }

        public Entity Create(string name, Vector2 position, float rotation, Vector2 scale)
        {
            var entity = _world.AddEntity(name);
            entity.LocalPosition = position;
            entity.LocalRotation = rotation;
            entity.LocalScale = scale;
            AddComponents(entity);
            return entity;
        }

        protected abstract void AddComponents(Entity entity);
    }
}
