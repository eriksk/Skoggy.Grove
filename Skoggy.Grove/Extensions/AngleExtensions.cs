using System;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Extensions
{
    public static class AngleExtensions
    {
        public static Vector2 ToDirection(this float radians)
            => new Vector2(
                MathF.Cos(radians),
                MathF.Sin(radians)
            );

        public static float ToAngle(this Vector2 direction) => MathF.Atan2(direction.Y, direction.X);
    }
}