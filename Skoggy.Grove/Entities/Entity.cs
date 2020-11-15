using Microsoft.Xna.Framework;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Skoggy.Grove.Entities
{
    public sealed class Entity
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
            
            World.Remove(this);
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

        private Entity _parent;

        public Entity Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent?.Children.Remove(this);
                _parent = value;
                _parent?.Children.Add(this);
            }
        }

        public List<Entity> Children { get; } = new List<Entity>();
        public int ParentCount => Parent != null ? 1 + Parent.ParentCount : 0;

        public Vector2 LocalPosition = Vector2.Zero;
        public float LocalRotation = 0f;
        public Vector2 LocalScale = Vector2.One;

        public Vector2 WorldPosition
        {
            get
            {
                return Vector2.Transform(LocalPosition, Parent?.Matrix ?? Matrix.Identity);
            }
            set
            {
                LocalPosition = Vector2.Transform(value, Matrix.Invert(Parent?.Matrix ?? Matrix.Identity));
            }
        }

        public float WorldRotation
        {
            get
            {
                return LocalRotation + (Parent?.WorldRotation ?? 0f);
            }
            set
            {
                LocalRotation = value - (Parent?.WorldRotation ?? 0f);
            }
        }

        public Vector2 WorldScale
        {
            get
            {
                return LocalScale * (Parent?.WorldScale ?? Vector2.One);
            }
            set
            {
                LocalScale = value / (Parent?.WorldScale ?? Vector2.One);
            }
        }

        public Matrix Matrix =>
            Matrix.CreateScale(WorldScale.X, WorldScale.Y, 1f) *
            Matrix.CreateRotationZ(WorldRotation) *
            Matrix.CreateTranslation(WorldPosition.X, WorldPosition.Y, 0f);

    }
}
