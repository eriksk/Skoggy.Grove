
using System;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Maths
{
    public static class RandF
    {
        private static Random _random = new Random();

        public static float Next(float min, float max)
        {
            return min + (max - min) * (float)_random.NextDouble();
        }
        
        public static Vector2 Position(float min, float max)
        {
            return new Vector2(
                Next(min, max),
                Next(min, max)
            );
        }

        public static Vector2 Direction()
        {
            var rotation = Rotation();
            return new Vector2(MathF.Cos(rotation), MathF.Sin(rotation));
        }

        public static float Rotation()
        {
            return Next(-MathF.PI, MathF.PI);
        }
    }
}