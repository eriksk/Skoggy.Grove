using System;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Modules;

namespace Skoggy.Grove.Entities.Components.Standard.Physics
{
    public class RigidbodyComponent : Component, IInitialize, IUpdate
    {
        public Body Body;
        public RigidbodyType Type = RigidbodyType.Dynamic;
        public bool FixedRotation;

        public void Initialize()
        {
            var physicsModule = World.GetModule<PhysicsEntityModule>();
            var bodyType = MapType(Type);
            
            Body = BodyFactory.CreateBody(
                physicsModule.World,
                ConvertUnits.ToSimUnits(Entity.WorldPosition),
                ConvertUnits.ToSimUnits(Entity.WorldRotation),
                bodyType,
                Entity.Id);
                
            Body.FixedRotation = FixedRotation;
        }

        public void Update()
        {
            Entity.WorldPosition = ConvertUnits.ToDisplayUnits(Body.Position);
            Entity.WorldRotation = ConvertUnits.ToDisplayUnits(Body.Rotation);
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