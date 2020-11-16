using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;

namespace Skoggy.Grove.TexturePacker
{
    public static class TexturePackerLoader
    {
        public static TexturePackerAtlas Load(string path)
        {
            var content = File.ReadAllText(path, Encoding.UTF8);
            var model = JsonConvert.DeserializeObject<TexturePackerFile>(content);

            return new TexturePackerAtlas()
            {
                Name = model.meta.image,
                Size = new Vector2(model.meta.size.w, model.meta.size.h),
                Sources = model.frames.Select(MapFrame).ToArray()
            };
        }

        private static SpriteSource MapFrame(Frame frame)
        {
            var source = new SpriteSource()
            {
                Name = frame.filename.Replace(".png", ""),
                Source = new Rectangle(
                    frame.frame.x,
                    frame.frame.y,
                    frame.frame.w,
                    frame.frame.h),
            };
            source.Origin = new Vector2(
                frame.pivot.x * source.Source.Width,
                frame.pivot.y * source.Source.Height);

            return source;
        }
    }
}
