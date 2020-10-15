using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Timers;

namespace Skoggy.Grove.Entities.Modules
{
    public class PhysicsEntityModule : IEntityModule
    {
        private World _world;

        public World World => _world;
        public Vector2 Gravity
        {
            get { return _world.Gravity; }
            set { _world.Gravity = value; }
        }

        public PhysicsEntityModule()
        {
            _world = new World(Vector2.Zero);
        }

        public void Update()
        {
            _world.Step(MathF.Min(Time.Delta, (1f / 30f)));
        }

        public void Render()
        {
        }
    }
}