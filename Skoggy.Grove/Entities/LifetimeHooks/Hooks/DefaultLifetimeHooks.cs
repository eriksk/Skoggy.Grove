﻿using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Actions;
using System.Linq;

namespace Skoggy.Grove.Entities.LifetimeHooks.Hooks
{
    internal class DefaultLifetimeHooks : ILifetimeHooks
    {
        public void Initialize(EntityWorld entityWorld)
        {
            var entities = entityWorld.Entities;

            foreach (var entity in entities)
            {
                foreach (var component in entity.Components)
                {
                    if (component.Initialized) continue;
                    if (component is IInitialize initialize)
                    {
                        initialize.Initialize();
                        component.Initialized = true;
                    }
                }
            }
        }

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

        public void Render(EntityWorld entityWorld, SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            // TODO: Maybe we can do a prepare step and group all components into a list of the action interfaces that we want to use
            var entities = entityWorld.Entities;

            // TODO: NO; BAD GC!!
            var renderables = entities.SelectMany(x => x.Components.Where(f => f is IRender))
                .Cast<IRender>()
                .GroupBy(x => x.Layer)
                .OrderBy(x => x.Key);

            foreach (var group in renderables)
            {
                var layerConfig = entityWorld.LayerConfiguration.Get(group.Key);
                spriteBatch.Begin(layerConfig.SpriteSortMode, layerConfig.BlendState, layerConfig.SamplerState);
                // TODO: NO; BAD GC!!
                foreach (var component in group.OrderBy(x => x.Order))
                {
                    component.Render(spriteBatch, graphics);
                }
                spriteBatch.End();
            }
        }
    }
}