using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Skoggy.Grove.Physics
{
    public sealed class PhysicsSimulation
    {
        private readonly int _framePerSecond;
        private float _deltaTime => 1f / (float)_framePerSecond;
        private float _accumulator;
        private float _renderAlpha;
        private List<Rigidbody> _bodies;
        private BroadPhaseCollisionDetector _broadPhase;
        private ShapeJumpTable _shapeJumpTable;

        public Vector2 Gravity;
        public int BodyCount => _bodies.Count;

        public PhysicsSimulation(int framePerSecond = 30)
        {
            _bodies = new List<Rigidbody>();
            _broadPhase = new BroadPhaseCollisionDetector();
            _shapeJumpTable = new ShapeJumpTable();
            Gravity = Vector2.Zero;
            _framePerSecond = framePerSecond;
        }

        public void AddBody(Rigidbody body)
        {
            _bodies.Add(body);
        }

        public void Step(float dt)
        {
            _accumulator += dt;

            // Avoid to many steps per update
            if (_accumulator > 0.2f)
            {
                _accumulator = 0.2f;
            }

            var stepTime = _deltaTime;

            while (_accumulator > stepTime)
            {
                Step();
                _accumulator -= stepTime;
            }
            _renderAlpha = _accumulator / stepTime;
        }

        public Vector2 InterpolatePosition(Vector2 previous, Vector2 current)
        {
            return previous * _renderAlpha + current * (1.0f - _renderAlpha);
        }

        public float InterpolateRotation(float previous, float current)
        {
            // TODO: This may not actually work.. Guess! May need slerp here
            return previous * _renderAlpha + current * (1.0f - _renderAlpha);
        }

        private void Step()
        {
            Integrate();
            BroadPhase();
            ResolveCollisions();
        }

        private void Integrate()
        {
            var dt = _deltaTime;
            foreach (var body in _bodies)
            {
                body.Force += Gravity * dt; // TODO: mass?

                // Classic Euler integration
                body.Velocity += (body.InverseMass * body.Force) * dt;
                body.Position += body.Velocity * dt;

                // Clear forces
                body.Force = Vector2.Zero;
            }
        }

        private void BroadPhase()
        {
            _broadPhase.GeneratePairs(_bodies);
        }

        private void ResolveCollisions()
        {
            foreach (var pair in _broadPhase.Pairs)
            {
                if (!_shapeJumpTable.DetectCollision(pair.BodyA, pair.BodyB, pair.ShapeA, pair.ShapeB, out var manifold)) continue;
                if (!manifold.Valid) continue;
                
                CollisionResolver.Resolve(ref manifold);
                CollisionResolver.PositionalCorrection(ref manifold);
            }
        }
    }
}