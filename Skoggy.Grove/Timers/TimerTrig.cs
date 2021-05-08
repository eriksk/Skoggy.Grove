
using System;

namespace Skoggy.Grove.Timers
{
    public class TimerTrig
    {
        public float Duration { get; private set; }
        private float _current;

        public TimerTrig(float duration)
        {
            if (duration <= 0f) throw new ArgumentOutOfRangeException($"{nameof(duration)} must be greated than zero.");

            Duration = duration;
        }

        public float Progress => _current / Duration;
        public bool Triggered => Progress >= 1f;

        public void Reset()
        {
            _current = 0f;
        }

        public void Reset(float newDuration)
        {
            _current = 0f;
            Duration = newDuration;
        }

        public TimerUpdateResult Update(float dt)
        {
            _current += dt;

            if (_current >= Duration)
            {
                _current -= Duration;
                return new TimerUpdateResult() { Triggered = true };
            }
            return new TimerUpdateResult() { Triggered = false };
        }

        public void UpdateWithoutTrigger(float dt)
        {
            _current += dt;

            if (_current >= Duration)
            {
                _current = Duration;
            }
        }
    }
}