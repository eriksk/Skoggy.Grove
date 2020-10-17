using System;
using System.Collections.Generic;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Physics
{
    public static class PhysicsExtensions
    {
        private static readonly List<Fixture> _explodeFixtureCache = new List<Fixture>();
        private static readonly HashSet<int> _usedCache = new HashSet<int>();

        public static void Explode(this World world,
            Vector2 worldPosition,
            float radius,
            float force) // TODO: Collision category
        {
            worldPosition = ConvertUnits.ToSimUnits(worldPosition);
            radius = ConvertUnits.ToSimUnits(radius);

            var aabb = new AABB(
                new Vector2(worldPosition.X - radius, worldPosition.Y - radius),
                new Vector2(worldPosition.X + radius, worldPosition.Y + radius)
            );

            world.QueryAABB((fixture) =>
            {
                _explodeFixtureCache.Add(fixture);
                return true;
            }, ref aabb);

            foreach (var fixture in _explodeFixtureCache)
            {
                var body = fixture.Body;
                if (body == null) continue;
                if (body.BodyType != BodyType.Dynamic) continue;
                if(_usedCache.Contains(body.BodyId)) continue;

                _usedCache.Add(body.BodyId);

                var direction = body.Position - worldPosition;
                var distance = Vector2.Distance(body.Position, worldPosition);
                if(distance > radius) continue;
                
                var magnitude = MathF.Pow(1f - (distance / radius), 2f);

                direction.Normalize();
                body.ApplyLinearImpulse(direction * (force * magnitude) * body.Mass, worldPosition);
            }

            _explodeFixtureCache.Clear();
            _usedCache.Clear();
        }
    }
}