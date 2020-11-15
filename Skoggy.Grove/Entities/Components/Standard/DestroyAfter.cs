using Skoggy.Grove.Entities.Actions;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class DestroyAfter : Component, IUpdate
    {
        public float Time = 1f;
        private float _current;

        public void Update()
        {
            _current += Timers.Time.Delta;
            if(_current >= Time)
            {
                Entity.Delete();
            }
        }
    }
}
