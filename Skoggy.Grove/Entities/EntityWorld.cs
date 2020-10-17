using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Cameras;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Entities.Components;
using Skoggy.Grove.Entities.Layers;
using Skoggy.Grove.Entities.LifetimeHooks.Hooks;
using Skoggy.Grove.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skoggy.Grove.Entities
{
    public sealed class EntityWorld
    {
        private DesynchronizedList<Entity> _entities;

        private int _entityIdCount;
        private ILifetimeHooks _lifetimeHooks;
        private Camera2D _camera;

        internal readonly ComponentActionCache ComponentActionCache;
        internal DesynchronizedList<Entity> Entities => _entities;
        internal readonly LayerConfiguration LayerConfiguration;
        internal List<IEntityModule> _modules;
        public Camera2D Camera => _camera;

        public EntityWorld(
            LayerConfiguration layerConfiguration = null,
            ILifetimeHooks lifetimeHooks = null)
        {
            LayerConfiguration = layerConfiguration ?? new LayerConfiguration();
            _lifetimeHooks = lifetimeHooks ?? new DefaultLifetimeHooks();
            _entities = new DesynchronizedList<Entity>();
            ComponentActionCache = new ComponentActionCache();
            _modules = new List<IEntityModule>();
            _camera = new Camera2D();
        }

        internal int NextEntityId() => _entityIdCount++;

        public TModule RegisterModule<TModule>(TModule module, Action<TModule> callback = null) where TModule : IEntityModule
        {
            _modules.Add(module);
            module.EntityWorld = this;
            callback?.Invoke(module);
            return module;
        }

        public Vector2 ScreenPointToWorldPoint(Vector2 screenPoint)
        {
            return Vector2.Transform(GameContext.ScreenPointToRenderPoint(screenPoint), Matrix.Invert(Camera.View));
        }

        public TModule GetModule<TModule>() where TModule : IEntityModule
        {
            // TODO: Create lookup
            return (TModule)_modules.FirstOrDefault(x => x.GetType() == typeof(TModule));
        }

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
            _lifetimeHooks.Initialize(this);
            _lifetimeHooks.Update(this);
            
            foreach(var module in _modules)
            {
                module.Update();
            }
        }

        public void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            _entities.Sync();
            foreach (var entity in _entities)
            {
                entity.Components.Sync();
            }
            _lifetimeHooks.Render(this, spriteBatch, graphics, _camera.View);
            
            foreach(var module in _modules)
            {
                module.Render(_camera.View);
            }
        }

        public Entity FindById(int id)
        {
            foreach (var entity in _entities)
            {
                if (entity.Id == id) return entity;
            }
            return null;
        }

        public Entity Find(string name)
        {
            foreach (var entity in _entities)
            {
                if (entity.Name == name) return entity;
            }
            return null;
        }

        public IEnumerable<Entity> FindAll(string name)
        {
            foreach (var entity in _entities)
            {
                if (entity.Name == name) yield return entity;
            }
        }

        public T FindComponent<T>() where T : Component
        {
            // TODO: FASTER HARDER NO GARBEJE COLLECTSION
            foreach (var entity in _entities)
            {
                foreach (var component in entity.Components)
                {
                    if (component is T typedComponent)
                    {
                        return typedComponent;
                    }
                }
            }

            return null;
        }

        public IEnumerable<T> FindAllComponents<T>() where T : Component
        {
            // TODO: FASTER HARDER NO GARBEJE COLLECTSION
            foreach(var entity in _entities)
            {
                foreach(var component in entity.Components)
                {
                    if(component is T typedComponent)
                    {
                        yield return typedComponent;
                    }
                }
            }
        }
    }
}
