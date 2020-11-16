
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;

namespace Skoggy.Grove.Rendering.PostFx
{
    public class PostFxPipeline
    {
        private List<PostFxPass> _passes;
        private RenderTarget2D _target1, _target2;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D _activeTarget;

        public RenderTarget2D ActiveTarget => _activeTarget;

        public PostFxPipeline()
        {
            _spriteBatch = new SpriteBatch(GameContext.Graphics);
            _passes = new List<PostFxPass>();
            GameContext.OnResolutionChanged += RecreateTargets;
            RecreateTargets();
        }

        public void AddPass(PostFxPass pass)
        {
            pass.Pipeline = this;
            _passes.Add(pass);
            pass.OnResolutionChanged(GameContext.Resolution);
        }

        private void RecreateTargets()
        {
            _target1?.Dispose();
            _target2?.Dispose();

            var width = GameContext.Resolution.X;
            var height = GameContext.Resolution.Y;

            _target1 = new RenderTarget2D(GameContext.Graphics, width, height) { Name = "PostFxTarget1" };
            _target2 = new RenderTarget2D(GameContext.Graphics, width, height) { Name = "PostFxTarget2" };

            foreach (var pass in _passes)
            {
                pass.OnResolutionChanged(GameContext.Resolution);
            }
        }

        public void SetTarget(RenderTarget2D target)
        {
            _activeTarget = target;
            GameContext.Graphics.SetRenderTarget(target);
        }

        public void Begin()
        {
            SetTarget(_target1);
            GameContext.Graphics.Clear(Color.Transparent);
        }

        public void End()
        {
            var source = _target1;
            var target = _target2;

            foreach (var pass in _passes)
            {
                SetTarget(target);
                pass.Render(_spriteBatch, source);

                // Swap
                var temp = source;
                source = target;
                target = source;
            }

            // TODO: Source to backbuffer
            // SetTarget(null);
            // GameContext.Graphics.Clear(Color.Magenta);
            // _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            // _spriteBatch.Draw(source, new Rectangle(0, 0, source.Width, source.Height), Color.White);
            // _spriteBatch.End();
        }
    }

    public class PostFxPass
    {
        public Effect Effect { get; }
        public PostFxPipeline Pipeline { get; internal set; }

        public PostFxPass(Effect effect)
        {
            Effect = effect;
        }

        public virtual void OnResolutionChanged(Point resolution)
        {

        }

        public virtual void Render(SpriteBatch spriteBatch, RenderTarget2D source)
        {
            spriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.NonPremultiplied,
                null,
                null,
                null,
                Effect);
            spriteBatch.Draw(source, new Rectangle(0, 0, source.Width, source.Height), Color.White);
            spriteBatch.End();
        }
    }
}