using System.Runtime.InteropServices;

namespace Skoggy.Grove.Physics.Shapes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AABB
    {
        public float MinX;
        public float MaxX;
        public float MinY;
        public float MaxY;
    }
}