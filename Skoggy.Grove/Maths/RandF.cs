
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

        public static bool Bool()
        {
            return Next(0f, 1f) > 0.5f;
        }

        public static int NextInt(int max)
        {
            return _random.Next(0, max);
        }
        
        public static int NextInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Spreads an angle randomly
        /// </summary>
        /// <param name="angle">Base angle in radians</param>
        /// <param name="spreadInDegrees">Degrees, ex 15 will be angle + random(-7.5 to 7.5) radians</param>
        /// <returns></returns>
        public static float Spread(float angle, float spreadInDegrees)
        {
            return angle + MathHelper.ToRadians(Next(-spreadInDegrees * 0.5f, spreadInDegrees * 0.5f));
        }
    }
}