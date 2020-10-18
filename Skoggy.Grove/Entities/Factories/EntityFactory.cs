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

        public Entity Create(string name, Vector2 position = new Vector2(), float rotation = 0f, Vector2? scale = null)
        {
            var entity = World.AddEntity(name);
            entity.LocalPosition = position;
            entity.LocalRotation = rotation;
            entity.LocalScale = scale ?? Vector2.One;
            AddComponents(entity);
            entity.Components.Sync();
            return entity;
        }

        protected abstract void AddComponents(Entity entity);
    }
}
