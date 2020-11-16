using System;
using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.TexturePacker
{
    public class Atlas
    {
        private readonly TexturePackerAtlas _source;
        public readonly Texture2D Texture;

        public Atlas(TexturePackerAtlas source, Texture2D texture)
        {
            _source = source;
            Texture = texture;
        }

        public SpriteSource GetOrDefault(string name) => _source.GetOrDefault(name);
    }
}
