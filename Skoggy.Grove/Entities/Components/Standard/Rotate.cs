using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Timers;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class Rotate : Component, IUpdate
    {
        public float Speed = 1f;

        public void Update()
            => Entity.LocalRotation += Speed * Time.Delta;
    }
}