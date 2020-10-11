using Skoggy.Grove.Transforms;

namespace Skoggy.Grove.Entities.Components
{
    public abstract class Component
    {
        public Entity Entity { get; internal set; }

        public void Delete() => Entity.DeleteComponent(this);

        public T GetComponent<T>() where T : Component => Entity.GetComponent<T>();
    }
}
