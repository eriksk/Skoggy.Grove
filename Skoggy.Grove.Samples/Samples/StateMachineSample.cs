using Microsoft.Xna.Framework;
using Skoggy.Grove.StateMachines;

namespace Skoggy.Grove.Samples.Samples
{
    public class StateMachineSample : Sample
    {
        private StateMachine<BaseState> _stateMachine;

        public StateMachineSample(SampleGame game) : base(game)
        {
            // TODO: find a good way to let states change state
        }

        public override void Load()
        {
            _stateMachine = new StateMachine<BaseState>(new BaseState[]
            {
                new StartState(),
                new SecondState(),
                new EndState()
            });
            _stateMachine.Start<StartState>();
        }

        public override void Update(GameTime gameTime)
        {
            _stateMachine.Update();
        }

        class BaseState : IState
        {
            public virtual void OnEnter()
            {
                System.Console.WriteLine($"Enter State {GetType().Name}");
            }

            public virtual void OnLeave()
            {
                System.Console.WriteLine($"Leave State {GetType().Name}");
            }

            public virtual void OnUpdate()
            {
                System.Console.WriteLine($"Update State {GetType().Name}");
            }
        }

        class StartState : BaseState
        {
            private int _count;

            public override void OnUpdate()
            {
                base.OnUpdate();
                _count++;

                if (_count > 3)
                {
                    // Set<SecondState>();
                    return;
                }
            }
        }

        class SecondState : BaseState
        {
            private int _count;

            public override void OnUpdate()
            {
                base.OnUpdate();

                _count++;

                if (_count > 3)
                {
                    // Set<EndState>();
                    return;
                }
            }
        }

        class EndState : BaseState
        {
            public override void OnEnter()
            {
                base.OnEnter();

                throw new System.Exception("This is where the state machine ends");
            }
        }
    }
}