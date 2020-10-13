using System.Linq;
using Newtonsoft.Json;

namespace Skoggy.Grove.Tiled
{
    public class TmxMapLoader
    {
        public static TmxMap Load(string data)
        {
            var model = JsonConvert.DeserializeObject<TmxMapDataModel>(data);

            return new TmxMap()
            {
                Layers = model.Layers.Select(x => new TmxMapLayer(x.Id, x.Name)
                {
                    Opacity = x.Opacity,
                    Chunks = x.Chunks.Select(f => new TmxMapLayerChunk()
                    {
                        X = f.X,
                        Y = f.Y,
                        Width = f.Width,
                        Height = f.Height,
                        Data = f.Data
                    }).ToArray()
                }).ToArray()
            };
        }


        internal class TmxMapDataModel
        {
            public string Type { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int TileWidth { get; set; }
            public int TileHeight { get; set; }
            public TmxMapDataModelLayer[] Layers { get; set; }
        }

        internal class TmxMapDataModelLayer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public float Opacity { get; set; }
            public int Startx { get; set; }
            public int Starty { get; set; }
            public string Type { get; set; }
            public bool Visible { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public TmxMapDataModelChunk[] Chunks { get; set; }
        }

        internal class TmxMapDataModelChunk
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int[] Data { get; set; }
        }
    }
}