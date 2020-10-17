using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Entities.Layers
{
    public class LayerConfigurationInfo
    {
        public readonly SpriteSortMode SpriteSortMode;
        public readonly BlendState BlendState;
        public readonly SamplerState SamplerState;
        public readonly RasterizerState RasterizerState;
        public readonly bool ScreenSpace;

        public LayerConfigurationInfo(
            SpriteSortMode spriteSortMode,
            BlendState blendState,
            SamplerState samplerState,
            RasterizerState rasterizerState,
            bool screenSpace = false)
        {
            SpriteSortMode = spriteSortMode;
            BlendState = blendState;
            SamplerState = samplerState;
            RasterizerState = rasterizerState;
            ScreenSpace = screenSpace;
        }
    }
}
