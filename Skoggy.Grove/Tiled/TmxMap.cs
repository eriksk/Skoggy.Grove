namespace Skoggy.Grove.Tiled
{
    public class TmxMap
    {
        public TmxMapLayer[] Layers { get; set; }
    }

    public class TmxMapLayer
    {
        public readonly int Id;
        public readonly string Name;
        public float Opacity = 1f;
        public TmxMapLayerChunk[] Chunks;

        public TmxMapLayer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class TmxMapLayerChunk
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public int[] Data;
    }
}