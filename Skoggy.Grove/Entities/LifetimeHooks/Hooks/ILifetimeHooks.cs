﻿using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Entities.LifetimeHooks.Hooks
{
    public interface ILifetimeHooks
    {
        void Initialize(EntityWorld entityWorld);
        void Update(EntityWorld entityWorld);
        void Render(EntityWorld entityWorld, SpriteBatch spriteBatch, GraphicsDevice graphics);
    }
}