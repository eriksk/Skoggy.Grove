using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Components.Base;
using Skoggy.Grove.Tiled;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class TmxMapComponent : Renderer, IInitialize
    {
        public string Path;
        public TmxMap Map;

        private SpriteSheetComponent _spriteSheet;

        public void Initialize()
        {
            _spriteSheet = GetComponent<SpriteSheetComponent>();
            if(Map == null)
            {
                Map = GameContext.Content.LoadTmxMap(Path);
            }
        }

        public override void Render(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if(Map == null) return;

            var offset = Entity.WorldPosition;
            var texture = _spriteSheet.SpriteSheet.Texture;

            // TODO: Culling

            foreach(var layer in Map.Layers)
            {
                foreach(var chunk in layer.Chunks)
                {
                    for(var x = 0; x < chunk.Width; x++)
                    {
                        for(var y = 0; y < chunk.Height; y++)
                        {
                            var cell = chunk.Data[x + y * chunk.Width] - 1;
                            if(cell < 0) continue;

                            var source = _spriteSheet.SpriteSheet[cell];
                            var cellPosition = new Vector2(
                                x * _spriteSheet.CellSize,
                                y * _spriteSheet.CellSize
                            );
                                            
                            spriteBatch.Draw(
                                texture,
                                offset + cellPosition,
                                source,
                                Color.Lerp(Color.Black, Color.White, layer.Opacity),
                                0f,
                                Vector2.Zero,
                                Vector2.One,
                                SpriteEffects.None,
                                0f);
                        }
                    }
                }
            }
        }
    }
}
