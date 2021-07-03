using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Entities.Modules
{
    public class CoroutineModule : IEntityModule
    {
        public EntityWorld EntityWorld { get; set; }

        private List<EntityCoroutine> _coroutines;

        public CoroutineModule()
        {
            _coroutines = new List<EntityCoroutine>();
        }

        public void Start(Entity owner, IEnumerator coroutine)
        {
            if (owner is null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            if (coroutine is null)
            {
                throw new ArgumentNullException(nameof(coroutine));
            }

            _coroutines.Add(new EntityCoroutine(owner, coroutine));
        }

        public void Update()
        {
            for (var i = 0; i < _coroutines.Count; i++)
            {
                var routine = _coroutines[i];
                if (routine.Owner.Deleted)
                {
                    _coroutines.RemoveAt(i);
                    i--;
                    continue;
                }

                if (!routine.Coroutine.MoveNext())
                {
                    _coroutines.RemoveAt(i);
                    i--;
                    continue;
                }

                // TODO: use this value?;
                // var value = routine.Coroutine.Current
            }
        }

        public void PreRender(Matrix view)
        {
        }

        public void Render(Matrix cameraView)
        {
        }

        class EntityCoroutine
        {
            public Entity Owner { get; }
            public IEnumerator Coroutine { get; }

            public EntityCoroutine(Entity owner, IEnumerator coroutine)
            {
                Owner = owner;
                Coroutine = coroutine;
            }
        }
    }
}