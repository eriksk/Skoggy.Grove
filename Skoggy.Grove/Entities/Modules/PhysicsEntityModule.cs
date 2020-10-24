using System;
using FarseerPhysics;
using FarseerPhysics.DebugView;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Skoggy.Grove.Contexts;
using Skoggy.Grove.Physics;
using Skoggy.Grove.Timers;

namespace Skoggy.Grove.Entities.Modules
{
    public class PhysicsEntityModule : IEntityModule
    {
        private World _world;
        private MonoGameDebugView _physicsDebugger;
        private Matrix _projection;

        public World World => _world;
        public Vector2 Gravity
        {
            get { return _world.Gravity; }
            set { _world.Gravity = value; }
        }

        public EntityWorld EntityWorld { get; set; }

        public PhysicsEntityModule()
        {
            _world = new World(Vector2.Zero);
        }

        public void EnableDebugging(SpriteFont spriteFont)
        {
            _physicsDebugger = new MonoGameDebugView(_world);
            _physicsDebugger.LoadContent(GameContext.Graphics, spriteFont);
            _physicsDebugger.Enabled = true;
        }

        public void Update()
        {
            _world.Step(MathF.Min(Time.Delta, (1f / 30f)));

            _projection = Matrix.CreateOrthographicOffCenter(
                0f,
                ConvertUnits.ToSimUnits(GameContext.RenderResolution.X),
                ConvertUnits.ToSimUnits(GameContext.RenderResolution.Y),
                0f,
                0f, 1f);
        }

        public void Render(Matrix cameraView)
        {
            if (_physicsDebugger != null)
            {
                _physicsDebugger.RenderDebugData(_projection, EntityWorld.Camera.PhysicsView);
            }
        }
    }
}