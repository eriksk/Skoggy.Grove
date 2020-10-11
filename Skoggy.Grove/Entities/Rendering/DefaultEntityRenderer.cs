using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Actions;

namespace Skoggy.Grove.Entities.Rendering
{
    internal sealed class DefaultEntityRenderer : IEntityRenderer
    {
        public void Render(EntityWorld entityWorld, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            // TODO: Maybe we can do a prepare step and group all components into a list of the action interfaces that we want to use
            var entities = entityWorld.Entities;

            foreach (var entity in entities)
            {
                foreach (var component in entity.Components)
                {
                    // TODO: ugh, make it good instead
                    if (component is IRender renderAction)
                    {
                        renderAction.Render(spriteBatch, graphics);
                    }
                }
            }
        }
    }
}
