using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Skoggy.Grove.Entities.Layers
{
    public class LayerConfiguration
    {
        private readonly LayerConfigurationInfo _defaultConfiguration;
        private Dictionary<int, LayerConfigurationInfo> _configurations;

        public LayerConfiguration(LayerConfigurationInfo defaultConfiguration = null)
        {
            _configurations = new Dictionary<int, LayerConfigurationInfo>();
            _defaultConfiguration = defaultConfiguration ?? new LayerConfigurationInfo(
                SpriteSortMode.Deferred,
                BlendState.NonPremultiplied,
                SamplerState.LinearClamp,
                RasterizerState.CullCounterClockwise);
        }

        internal LayerConfigurationInfo Get(int layer)
        {
            if (_configurations.TryGetValue(layer, out var config))
            {
                return config;
            }
            return _defaultConfiguration;
        }

        public void AddLayer(int layer, LayerConfigurationInfo config)
        {
            if (_configurations.ContainsKey(layer))
            {
                throw new Exception($"Layer {layer} already added");
            }

            _configurations.Add(layer, config);
        }
    }
}
