using Skoggy.Grove.Contexts;
using Skoggy.Grove.Entities.Components;
using Skoggy.Grove.Transforms;
using System;
using System.Linq;

namespace Skoggy.Grove.Entities
{
    public sealed class Entity : Transform
    {
        public readonly int Id;
        public string Name;
        public readonly EntityWorld World;

        internal bool Deleted;
        internal readonly DesynchronizedList<Component> Components;

        internal Entity(string name, EntityWorld world)
        {
            Id = world.NextEntityId();
            Name = name;
            World = world;
            Components = new DesynchronizedList<Component>();
        }

        public void Delete()
        {
            if (Deleted) return;
            
            World.Entities.Remove(this);
            Deleted = true;
        }

        public T AddComponent<T>(Action<T> initializationCallback = null) where T : Component
        {
            var component = Activator.CreateInstance<T>();
            component.Entity = this;
            Components.Add(component);
            initializationCallback?.Invoke(component);
            return component;
        }

        public T GetComponent<T>() where T : Component
        {
            // TODO: Make much much better
            return (T)Components.FirstOrDefault(x => x.GetType() == typeof(T)); // TODO: Should also accept an interface for example
        }

        internal void DeleteComponent(Component component)
        {
            Components.Remove(component);
        }
    }
}
