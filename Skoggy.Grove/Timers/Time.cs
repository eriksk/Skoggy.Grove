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

        public static void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            UnscaledDelta = dt;
            Delta = dt * TimeScale;
            Elapsed += Delta;
            ElapsedUnscaled += UnscaledDelta;
        }
    }
}
