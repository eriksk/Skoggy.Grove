using Skoggy.Grove.Entities.Actions;

namespace Skoggy.Grove.Entities.Updating
{
    internal sealed class DefaultEntityUpdater : IEntityUpdater
    {
        public void Update(EntityWorld entityWorld)
        {
            // TODO: Maybe we can do a prepare step and group all components into a list of the action interfaces that we want to use
            var entities = entityWorld.Entities;

            foreach (var entity in entities)
            {
                foreach (var component in entity.Components)
                {
                    // TODO: ugh, make it good instead
                    if (component is IUpdate updateAction)
                    {
                        updateAction.Update();
                    }
                }
            }
        }
    }
}
