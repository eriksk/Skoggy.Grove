using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Entities.Components;
using Skoggy.Grove.Entities.Rendering;
using Skoggy.Grove.Entities.Updating;

namespace Skoggy.Grove.Entities
{
    public sealed class EntityWorld
    {
        private DesynchronizedList<Entity> _entities;

        private int _entityIdCount;
        private IEntityUpdater _updater;
        private IEntityRenderer _renderer;

        internal readonly ComponentActionCache ComponentActionCache;
        internal DesynchronizedList<Entity> Entities => _entities;

        public EntityWorld(
            IEntityUpdater updater = null,
            IEntityRenderer renderer = null)
        {
            _updater = updater ?? new DefaultEntityUpdater();
            _renderer = renderer ?? new DefaultEntityRenderer();
            _entities = new DesynchronizedList<Entity>();
            ComponentActionCache = new ComponentActionCache();
        }

        internal int NextEntityId() => _entityIdCount++;

        public Entity AddEntity(string name)
        {
            var entity = new Entity(name, this);
            _entities.Add(entity);
            return entity;
        }

        public void Update()
        {
            _entities.Sync();
            foreach (var entity in _entities)
            {
                entity.Components.Sync();
            }
            _updater?.Update(this);
        }

        public void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            _entities.Sync();
            foreach (var entity in _entities)
            {
                entity.Components.Sync();
            }
            _renderer?.Render(this, spriteBatch, graphics);
        }
    }
}
