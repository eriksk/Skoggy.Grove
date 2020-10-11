using Skoggy.Grove.Entities.Actions;
using System;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class LambdaInitialize : Component, IInitialize
    {
        public Action<LambdaInitialize> Callback;

        public void Initialize()
        {
            Callback?.Invoke(this);
        }
    }
}
