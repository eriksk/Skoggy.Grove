using System;
using Skoggy.Grove.Timers;

namespace Skoggy.Grove.Animations
{
    public class Animation
    {
        public readonly int[] Frames;
        public readonly bool Loop;
        public readonly float Duration;

        private TimerTrig _timer;
        private int _currentFrameIndex;

        public Animation(float duration, int[] frames, bool loop)
        {
            Frames = frames ?? throw new ArgumentNullException(nameof(frames));
            if (frames.Length == 0) throw new ArgumentException($"{nameof(frames)} must have at least one frame.");

            Loop = loop;
            Duration = duration;
            _timer = new TimerTrig(duration / frames.Length);
        }

        public int Frame => Frames[_currentFrameIndex];

        public void Reset()
        {
            _timer.Reset();
            _currentFrameIndex = 0;
        }

        public void Update(float dt)
        {
            if (_timer.Update(dt).Triggered)
            {
                _currentFrameIndex++;
                if (_currentFrameIndex > Frames.Length - 1)
                {
                    if (Loop)
                    {
                        _currentFrameIndex = 0;
                    }
                    else
                    {
                        _currentFrameIndex = Frames.Length - 1;
                    }
                }
            }
        }
    }
}