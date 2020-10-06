using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Textures
{
    public class SpriteSheet
    {
        public readonly Texture2D Texture;
        public readonly int CellWidth;
        public readonly int CellHeight;
        public readonly int Columns;
        public readonly int Rows;

        private Rectangle[] _sources;

        public SpriteSheet(Texture2D texture, int cellSize)
            : this(texture, cellSize, cellSize)
        {
        }

        public SpriteSheet(Texture2D texture, int cellWidth, int cellHeight)
        {
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            if (cellWidth < 1) throw new ArgumentOutOfRangeException(nameof(cellWidth));
            if (cellHeight < 1) throw new ArgumentOutOfRangeException(nameof(cellWidth));

            CellWidth = cellWidth;
            CellHeight = cellHeight;

            Columns = Texture.Width / CellWidth;
            Rows = Texture.Height / CellHeight;

            CreateSources();
        }

        private void CreateSources()
        {
            _sources = new Rectangle[Columns * Rows];

            for (var x = 0; x < Columns; x++)
            {
                for (var y = 0; y < Rows; y++)
                {
                    var index = x + y * Columns;
                    _sources[index] = new Rectangle(
                        x * CellWidth,
                        y * CellHeight,
                        CellWidth,
                        CellHeight
                    );
                }
            }
        }

        public int SpriteCount => _sources.Length;
        public Rectangle this[int index] => _sources[index];
        public Rectangle this[int column, int row] => _sources[column + row * Columns];
    }
}