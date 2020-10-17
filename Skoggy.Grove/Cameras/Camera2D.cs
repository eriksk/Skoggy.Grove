using FarseerPhysics;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Contexts;

namespace Skoggy.Grove.Cameras
{
    public class Camera2D
    {
        public Vector2 Position;
        public float Rotation;
        public float Scale = 1f;
        public Vector2 Offset;

        private Vector2 _center;
        public Vector2 Center => _center;

        public Matrix View =>
            Matrix.CreateTranslation(-new Vector3(Position.X + Offset.X, Position.Y + Offset.Y, 0f)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(Scale) *
            Matrix.CreateTranslation(new Vector3(_center.X, _center.Y, 0f));

        public Matrix PhysicsView =>
            Matrix.CreateTranslation(ConvertUnits.ToSimUnits(-new Vector3(Position.X + Offset.X, Position.Y + Offset.Y, 0f))) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(Scale) *
            Matrix.CreateTranslation(ConvertUnits.ToSimUnits(new Vector3(_center.X, _center.Y, 0f)));

        public Camera2D()
        {
            GameContext.OnResolutionChanged += UpdateCenter;
            UpdateCenter();
        }

        private void UpdateCenter()
        {
            _center = GameContext.RenderResolution.ToVector2() * 0.5f;
        }
    }
}