namespace Skoggy.Grove.Entities.Components
{
    public abstract class Component
    {
        public Entity Entity { get; internal set; }
        public EntityWorld World => Entity.World;
        internal bool Initialized;

        public void Delete() => Entity.DeleteComponent(this);
        public T GetComponent<T>() where T : Component => Entity.GetComponent<T>();
    }
}
