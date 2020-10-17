using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Modules;

namespace Skoggy.Grove.Entities.Components.Standard.Physics
{
    public class Rigidbody : Component, IInitialize, IUpdate
    {
        internal Body Body;

        public RigidbodyType Type = RigidbodyType.Dynamic;
        public bool FixedRotation;

        private bool _initialized;

        public void Initialize()
        {
            var physicsModule = World.GetModule<PhysicsEntityModule>();
            var bodyType = MapType(Type);

            Body = BodyFactory.CreateBody(
                physicsModule.World,
                ConvertUnits.ToSimUnits(Entity.WorldPosition),
                Entity.WorldRotation,
                bodyType,
                Entity.Id);

            Body.FixedRotation = FixedRotation;

            _initialized = true;
        }

        public void MovePosition(Vector2 position)
        {
            if (!_initialized) Initialize();

            Body.Position = ConvertUnits.ToSimUnits(position);
            Entity.WorldPosition = position;
        }

        public void MoveRotation(float rotation)
        {
            if (!_initialized) Initialize();

            Body.Rotation = rotation;
            Entity.WorldRotation = rotation;
        }

        public void AddForce(Vector2 force, ForceMode forceMode = ForceMode.Force)
        {
            if (!_initialized) Initialize();

            if (forceMode == ForceMode.Force)
            {
                Body.ApplyForce(force * Body.Mass);
            }
            else if (forceMode == ForceMode.Impulse)
            {
                Body.ApplyLinearImpulse(force * Body.Mass);
            }
        }

        public void Update()
        {
            Entity.WorldPosition = ConvertUnits.ToDisplayUnits(Body.Position);
            Entity.WorldRotation = Body.Rotation;
        }

        private BodyType MapType(RigidbodyType type)
        {
            switch (type)
            {
                case RigidbodyType.Static: return BodyType.Static;
                case RigidbodyType.Dynamic: return BodyType.Dynamic;
                case RigidbodyType.Kinematic: return BodyType.Kinematic;
            }
            throw new NotImplementedException(((int)type).ToString());
        }
    }
}