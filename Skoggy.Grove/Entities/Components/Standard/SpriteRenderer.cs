﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Components.Base;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class SpriteRenderer : Renderer, IUpdate
    {
        public Texture2D Texture;
        public Rectangle Source;
        public Vector2 Pivot = Vector2.One * 0.5f;
        public SpriteEffects SpriteEffects = SpriteEffects.None;
        public Color Color = Color.White;

        public void Update()
        {
        }

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (Texture == null) return;

            var origin = new Vector2(Source.Width, Source.Height) * Pivot;

            if(Entity.WorldScale.X < 0f)
            {

            }

            spriteBatch.Draw(
                Texture,
                Entity.WorldPosition,
                Source,
                Color,
                Entity.WorldRotation,
                origin,
                Entity.WorldScale,
                SpriteEffects,
                0f);
        }
    }
}
