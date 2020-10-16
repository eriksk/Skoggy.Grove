using System;

namespace Skoggy.Grove.Entities.Components
{
    public abstract class Component
    {
        private Entity _entity;
        public Entity Entity
        {
            get { return _entity; }
            internal set
            {
                _entity = value;
            }
        }

        public EntityWorld World => Entity.World;
        public bool Enabled { get; set; } = true;
        internal bool Initialized;

        public void Delete() => Entity.DeleteComponent(this);
        public T GetComponent<T>() where T : Component => Entity.GetComponent<T>();
        protected virtual void OnCreated()
        {
        }

        /// <summary>
        /// Finds the immidiate child by name
        /// </summary>
        /// <param name="name">Name of the child</param>
        /// <returns></returns>
        protected Entity FindChild(string name)
        {
            foreach (var child in Entity.Children)
            {
                if (child.Name == name)
                {
                    return child;
                }
            }

            return null;
        }
    }
}
