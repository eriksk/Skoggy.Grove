﻿using Skoggy.Grove.Entities.Actions;
using System;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class LambdaUpdate : Component, IUpdate
    {
        public Action<LambdaUpdate> Callback;

        public void Update()
        {
            Callback?.Invoke(this);
        }
    }
}
