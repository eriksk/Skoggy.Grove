using Microsoft.Xna.Framework;
using System;

namespace Skoggy.Grove.TexturePacker
{
    public class TexturePackerAtlas
    {
        public SpriteSource[] Sources { get; internal set; }
        public string Name { get; internal set; }
        public Vector2 Size { get; internal set; }

        public SpriteSource GetOrDefault(string name)
        {
            // TODO: GC and optimize
            foreach(var source in Sources)
            {
                if (source.Name == name) return source;
            }
            return null;
        }
    }

    public class SpriteSource
    {
        public string Name { get; internal set; }
        public Rectangle Source { get; internal set; }
        public Vector2 Origin { get; internal set; }
    }
}
