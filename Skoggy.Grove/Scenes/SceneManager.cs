using Microsoft.Xna.Framework;
using Skoggy.Grove.Timers;
using System;

namespace Skoggy.Grove.Scenes
{
    public class SceneManager : ISceneManager
    {
        private IScene _activeScene;
        private IScene _targetScene;

        private TransitionState _state;
        private TimerTrig _inTimer;
        private TimerTrig _outTimer;
        private TimerTrig _loadingTimer;
        private readonly ISceneTransition _transition;

        public SceneManager(
            ISceneTransition transition,
            float transitionInDuration,
            float transitionOutDuration)
        {
            _transition = transition;
            _inTimer = new TimerTrig(transitionInDuration);
            _outTimer = new TimerTrig(transitionOutDuration);
            _loadingTimer = new TimerTrig(0.2f);
            SetState(TransitionState.None);
        }

        public void Load(IScene scene)
        {
            _targetScene = scene;
            if (_activeScene == null)
            {
                SetState(TransitionState.Loading);
            }
            else
            {
                SetState(TransitionState.In);
            }
        }

        private void SetState(TransitionState state)
        {
            _state = state;
            _inTimer.Reset();
            _outTimer.Reset();
            _loadingTimer.Reset();

            if (state == TransitionState.Loading)
            {
            }
        }

        public void Update(GameTime gameTime)
        {
            if (_state == TransitionState.None)
            {
                _activeScene?.Update(gameTime);
            }

            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (_state)
            {
                case TransitionState.In:
                    _inTimer.UpdateWithoutTrigger(dt);
                    if (_inTimer.Triggered)
                    {
                        SetState(TransitionState.Loading);
                    }
                    break;
                case TransitionState.Out:
                    _outTimer.UpdateWithoutTrigger(dt);
                    if (_outTimer.Triggered)
                    {
                        SetState(TransitionState.None);
                    }
                    break;
                case TransitionState.Loading:
                    _loadingTimer.UpdateWithoutTrigger(dt);
                    if (_loadingTimer.Triggered)
                    {
                        _targetScene.Load();
                        _activeScene = _targetScene;
                        _targetScene = null; // TODO: Dispose?
                        SetState(TransitionState.Out);
                    }
                    break;
            }
        }

        public void Draw(GameTime gameTime)
        {
            _activeScene?.Draw(gameTime);

            switch (_state)
            {
                case TransitionState.In:
                    _transition.Draw(MathHelper.Clamp(_inTimer.Progress, 0f, 1f));
                    break;
                case TransitionState.Out:
                    _transition.Draw(1f - MathHelper.Clamp(_outTimer.Progress, 0f, 1f));
                    break;
                case TransitionState.Loading:
                    _transition.Draw(1f);
                    break;
            }
        }

        internal enum TransitionState
        {
            In,
            Out,
            Loading,
            None
        }
    }
}