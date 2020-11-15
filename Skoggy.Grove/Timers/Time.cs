using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Timers
{
    public static class Time
    {
        public static float Delta { get; private set; }
        public static float UnscaledDelta { get; private set; }
        public static float TimeScale { get; set; } = 1f;
        public static float Elapsed { get; private set; }
        public static float ElapsedUnscaled { get; private set; }
        public static int FPS { get; private set; }

        private const int FPSQueueBufferSize = 256;
        private static Queue<float> _fpsQueue = new Queue<float>(FPSQueueBufferSize);

        public static void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UnscaledDelta = dt;
            Delta = dt * TimeScale;
            Elapsed += Delta;
            ElapsedUnscaled += UnscaledDelta;

            _fpsQueue.Enqueue(UnscaledDelta);

            while(_fpsQueue.Count > FPSQueueBufferSize)
            {
                _fpsQueue.Dequeue();
            }

            FPS = (int)(1000f / (_fpsQueue.Average(x => x) * 1000f)) + 1;
        }
    }
}
