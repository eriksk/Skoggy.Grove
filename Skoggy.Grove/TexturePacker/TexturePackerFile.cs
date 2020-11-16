using System;
using System.Collections.Generic;
using System.Text;

namespace Skoggy.Grove.TexturePacker
{
    internal class TexturePackerFile
    {
        public Frame[] frames { get; set; }
        public Meta meta { get; set; }
    }

    internal class Meta
    {
        public string app { get; set; }
        public string version { get; set; }
        public string image { get; set; }
        public string format { get; set; }
        public Size size { get; set; }
        public string scale { get; set; }
        public string smartupdate { get; set; }
    }

    internal class Size
    {
        public int w { get; set; }
        public int h { get; set; }
    }

    internal class Frame
    {
        public string filename { get; set; }
        public Rect frame { get; set; }
        public bool rotated { get; set; }
        public bool trimmed { get; set; }
        public Rect spriteSourceSize { get; set; }
        public Sourcesize sourceSize { get; set; }
        public Pivot pivot { get; set; }
    }

    internal class Rect
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    internal class Sourcesize
    {
        public int w { get; set; }
        public int h { get; set; }
    }

    internal class Pivot
    {
        public float x { get; set; }
        public float y { get; set; }
    }
}
