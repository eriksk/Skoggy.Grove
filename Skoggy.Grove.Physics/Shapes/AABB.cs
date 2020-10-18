using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Physics.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AABB
    {
        public float MinX;
        public float MaxX;
        public float MinY;
        public float MaxY;

        public Vector2 Center => new Vector2(MaxX - MinX, MaxY - MinY);
    }
}