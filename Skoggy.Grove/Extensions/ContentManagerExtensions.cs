
using System;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Skoggy.Grove.Tiled;

namespace Skoggy.Grove
{
    public static class ContentManagerExtensions
    {
        public static TmxMap LoadTmxMap(this ContentManager content, string path)
        {
            var rootPath = $"{AppContext.BaseDirectory}/{content.RootDirectory}/";
            return TmxMapLoader.Load(File.ReadAllText(Path.Combine(rootPath, $"{path}.json")));
        }
    }
}