using Skoggy.Grove.Entities.Actions;
using System;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class LambdaUpdate : Component, IUpdate
    {
        public Action<LambdaUpdate> UpdateFunction;

        public void Update()
        {
            UpdateFunction?.Invoke(this);
        }
    }
}
