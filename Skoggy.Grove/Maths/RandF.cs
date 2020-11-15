
using System;

namespace Skoggy.Grove.Maths
{
    public static class RandF
    {
        private static Random _random = new Random();

        public static float Next(float min, float max)
        {
            return min + (max - min) * (float)_random.NextDouble();
        }

        public static float Rotation()
        {
            return Next(-MathF.PI, MathF.PI);
        }
    }
}