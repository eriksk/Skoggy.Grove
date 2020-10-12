namespace Skoggy.Grove.Entities.Components
{
    public abstract class Component
    {
        public Entity Entity { get; internal set; }
        public EntityWorld World => Entity.World;
        public bool Enabled { get; set; } = true;
        internal bool Initialized;

        public void Delete() => Entity.DeleteComponent(this);
        public T GetComponent<T>() where T : Component => Entity.GetComponent<T>();

        /// <summary>
        /// Finds the immidiate child by name
        /// </summary>
        /// <param name="name">Name of the child</param>
        /// <returns></returns>
        protected Entity FindChild(string name)
        {
            foreach(var child in Entity.Children)
            {
                if(child.Name == name)
                {
                    return child;
                }
            }

            return null;
        }
    }
}
