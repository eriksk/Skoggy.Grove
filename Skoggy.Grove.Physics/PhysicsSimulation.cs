using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Skoggy.Grove.Physics.Shapes;

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

        public Vector2 Gravity;

        public PhysicsSimulation(int framePerSecond = 30)
        {
            _bodies = new List<Rigidbody>();
            _broadPhase = new BroadPhaseCollisionDetector();
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
                // Box vs Box
                {
                    if ((pair.ShapeA is Box boxA) && (pair.ShapeB is Box boxB))
                    {
                        if (CollisionDetector.BoxVsBox(pair.BodyA, pair.BodyB, boxA, boxB, out var manifold))
                        {
                            if (manifold.Valid)
                            {
                                CollisionResolver.Resolve(ref manifold);
                                CollisionResolver.PositionalCorrection(ref manifold);
                            }
                        }
                        continue;
                    }
                }

                // Box vs Circle
                {
                    if ((pair.ShapeA is Box boxA) && (pair.ShapeB is Circle circleB))
                    {
                        if (CollisionDetector.BoxvsCircle(pair.BodyA, pair.BodyB, boxA, circleB, out var manifold))
                        {
                            if (manifold.Valid)
                            {
                                CollisionResolver.Resolve(ref manifold);
                                CollisionResolver.PositionalCorrection(ref manifold);
                            }
                        }
                        continue;
                    }
                }
                
                // Circle vs Box
                {
                    if ((pair.ShapeA is Circle circleA) && (pair.ShapeB is Box boxB))
                    {
                        if (CollisionDetector.BoxvsCircle(pair.BodyB, pair.BodyA, boxB, circleA, out var manifold))
                        {
                            if (manifold.Valid)
                            {
                                CollisionResolver.Resolve(ref manifold);
                                CollisionResolver.PositionalCorrection(ref manifold);
                            }
                        }
                        continue;
                    }
                }
                
                // Circle vs Circle
                {
                    if ((pair.ShapeA is Circle circleA) && (pair.ShapeB is Circle circleB))
                    {
                        if (CollisionDetector.CircleVsCircle(pair.BodyA, pair.BodyB, circleA, circleB, out var manifold))
                        {
                            if (manifold.Valid)
                            {
                                CollisionResolver.Resolve(ref manifold);
                                CollisionResolver.PositionalCorrection(ref manifold);
                            }
                        }
                        continue;
                    }
                }
            }
        }
    }
}