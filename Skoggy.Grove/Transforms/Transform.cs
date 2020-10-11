using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Skoggy.Grove.Transforms
{
    public class Transform
    {
        private Transform _parent;

        public virtual Transform Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent?.Children.Remove(this);
                _parent = value;
                _parent?.Children.Add(this);
            }
        }
        public List<Transform> Children { get; } = new List<Transform>();

        public int ParentCount => Parent != null ? 1 + Parent.ParentCount : 0;

        public Vector2 LocalPosition = Vector2.Zero;
        public float LocalRotation = 0f;
        public Vector2 LocalScale = Vector2.One;

        public Vector2 WorldPosition
        {
            get
            {
                return Vector2.Transform(LocalPosition, Parent?.Matrix ?? Matrix.Identity);
            }
            set
            {
                LocalPosition = Vector2.Transform(value, Matrix.Invert(Parent?.Matrix ?? Matrix.Identity));
            }
        }

        public float WorldRotation
        {
            get
            {
                return LocalRotation + (Parent?.WorldRotation ?? 0f);
            }
            set
            {
                LocalRotation = value - (Parent?.WorldRotation ?? 0f);
            }
        }

        public Vector2 WorldScale
        {
            get
            {
                return LocalScale * (Parent?.WorldScale ?? Vector2.One);
            }
            set
            {
                LocalScale = value / (Parent?.WorldScale ?? Vector2.One);
            }
        }

        public Matrix Matrix =>
            Matrix.CreateScale(WorldScale.X, WorldScale.Y, 1f) *
            Matrix.CreateRotationZ(WorldRotation) *
            Matrix.CreateTranslation(WorldPosition.X, WorldPosition.Y, 0f);
    }
}
