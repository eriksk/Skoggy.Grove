using Skoggy.Grove.Animations;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Timers;
using System.Collections.Generic;
using System.Linq;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class AnimationComponent : Component, IUpdate, IInitialize
    {
        public Dictionary<string, Animation> Animations;

        private SpriteRenderer _renderer;
        private SpriteSheetComponent _sprites;
        private string _current;

        public void Play(string name)
        {
            if (Animations == null) return;
            if (_current == name) return;

            _current = name;
            Animations[name].Reset();
        }

        public void Initialize()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _sprites = GetComponent<SpriteSheetComponent>();
            _renderer.Texture = _sprites.SpriteSheet.Texture;

            Play(Animations.Keys.First());
        }

        public void Update()
        {
            if (Animations == null) return;
            if (_renderer == null) return;
            if (string.IsNullOrEmpty(_current)) return;

            var animation = Animations[_current];
            animation.Update(Time.Delta);
            _renderer.Source = _sprites.SpriteSheet[animation.Frame];
        }
    }
}
