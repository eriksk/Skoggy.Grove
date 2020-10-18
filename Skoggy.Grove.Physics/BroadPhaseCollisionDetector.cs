using System.Collections.Generic;
using Skoggy.Grove.Physics.Shapes;

namespace Skoggy.Grove.Physics
{
    public struct ShapePair
    {
        public Rigidbody BodyA;
        public Rigidbody BodyB;
        public Shape ShapeA;
        public Shape ShapeB;
    }

    internal class BroadPhaseCollisionDetector
    {
        private List<ShapePair> _workingPairs;
        private List<ShapePair> _uniquePairs;
        private ShapePairComparer _comparer;

        public BroadPhaseCollisionDetector()
        {
            _comparer = new ShapePairComparer();
            _workingPairs = new List<ShapePair>();
            _uniquePairs = new List<ShapePair>();
        }

        public List<ShapePair> Pairs => _uniquePairs;

        public void GeneratePairs(List<Rigidbody> bodies)
        {
            _workingPairs.Clear();
            _uniquePairs.Clear();

            for (var i = 0; i < bodies.Count; i++)
            {
                var bodyA = bodies[i];
                var aabbA = bodyA.Shape.CalculateAABB(bodyA);

                for (var j = 0; j < bodies.Count; j++)
                {
                    if (i == j) continue;

                    var bodyB = bodies[j];

                    if((bodyA.Shape.Layer & bodyB.Shape.Layer) == 0) // TODO: Maybe flip boolean op
                    {
                        continue;
                    }

                    var aabbB = bodyB.Shape.CalculateAABB(bodyB);

                    if (CollisionDetector.Intersect(ref aabbA, ref aabbB))
                    {
                        _workingPairs.Add(new ShapePair()
                        {
                            BodyA = bodyA,
                            BodyB = bodyB,
                            ShapeA = bodyA.Shape,
                            ShapeB = bodyB.Shape
                        });
                    }
                }
            }

            // TODO: Optimize duplicate culling
            _workingPairs.Sort(_comparer);

            var index = 0;
            while (index < _workingPairs.Count)
            {
                var pair = _workingPairs[index];
                _uniquePairs.Add(pair);
                index++;

                while(index < _workingPairs.Count)
                {
                    var potentialDuplicate = _workingPairs[index];
                    if(pair.ShapeA != potentialDuplicate.ShapeB || pair.ShapeB != potentialDuplicate.ShapeA)
                    {
                        break;
                    }
                    index++;
                }
            }
            _workingPairs.Clear();
        }

        class ShapePairComparer : IComparer<ShapePair>
        {
            public int Compare(ShapePair lhs, ShapePair rhs)
            {
                // TODO: Not sure if this works, maybe add unit tests to verify
                if (lhs.ShapeA.Id < rhs.ShapeA.Id)
                    return 1;

                if (lhs.ShapeA.Id == rhs.ShapeA.Id)
                    return lhs.ShapeB.Id < rhs.ShapeB.Id ? 1 : 0;

                return -1;
            }
        }
    }
}