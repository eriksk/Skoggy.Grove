using System;
using System.Collections.Generic;
using System.Linq;
using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Entities.Actions;
using Skoggy.Grove.Entities.Modules;
using Skoggy.Grove.Maths;

namespace Skoggy.Grove.Entities.Components.Standard
{
    public class TmxMapCollider : Component, IInitialize, IOnDestroy
    {
        public string[] CollisionLayerNames = new string[0];

        private TmxMapComponent _map;
        private Body _body;

        public void Initialize()
        {
            _map = GetComponent<TmxMapComponent>();

            var physicsModule = World.GetModule<PhysicsEntityModule>();

            _body = BodyFactory.CreateBody(
                physicsModule.World,
                ConvertUnits.ToSimUnits(Entity.WorldPosition),
                Entity.WorldRotation,
                BodyType.Static,
                Entity.Id);

            // TODO: Import this data
            const int cellSize = 16;

            foreach (var layer in _map.Map.Layers)
            {
                if (!CollisionLayerNames.Contains(layer.Name, StringComparer.OrdinalIgnoreCase)) continue;

                var cells = new List<Cell>();

                foreach (var chunk in layer.Chunks)
                {
                    var chunkOffset = new Vector2(chunk.X * cellSize, chunk.Y * cellSize);

                    for (var x = 0; x < chunk.Width; x++)
                    {
                        for (var y = 0; y < chunk.Height; y++)
                        {
                            var cell = chunk.Data[x + y * chunk.Width] - 1;
                            if (cell < 0) continue;

                            cells.Add(new Cell(chunk.X + x, chunk.Y + y));
                        }
                    }
                }

                var rectangles = new List<Rectangle>();

                // TODO: Greedy mesh algoad
                for (var i = 0; i < cells.Count; i++)
                {
                    var cell = cells[i];
                    rectangles.Add(new Rectangle(cell.X * cellSize, cell.Y * cellSize, cellSize, cellSize));
                }

                foreach (var rect in rectangles)
                {
                    var shape = PolygonTools.CreateRectangle(
                        ConvertUnits.ToSimUnits(rect.Width * 0.5f),
                        ConvertUnits.ToSimUnits(rect.Height * 0.5f),
                        ConvertUnits.ToSimUnits(rect.Center.ToVector2()),
                        angle: 0f);

                    _body.CreateFixture(new PolygonShape(shape, 1f));
                }
            }
        }

        public void OnDestroy()
        {
            var physicsModule = World.GetModule<PhysicsEntityModule>();
            physicsModule.World.RemoveBody(_body);
        }
    }
}
