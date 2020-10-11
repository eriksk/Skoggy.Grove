using Microsoft.Xna.Framework.Graphics;

namespace Skoggy.Grove.Entities.Layers
{
    public class LayerConfigurationInfo
    {
        public readonly SpriteSortMode SpriteSortMode;
        public readonly BlendState BlendState;
        public readonly SamplerState SamplerState;

        public LayerConfigurationInfo(
            SpriteSortMode spriteSortMode,
            BlendState blendState,
            SamplerState samplerState)
        {
            SpriteSortMode = spriteSortMode;
            BlendState = blendState;
            SamplerState = samplerState;
        }
    }
}
