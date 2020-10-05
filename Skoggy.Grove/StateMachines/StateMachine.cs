using System;
using System.Collections.Generic;
using System.Linq;

namespace Skoggy.Grove.StateMachines
{
    public class StateMachine<T> where T : IState
    {
        private Dictionary<Type, IState> _states;
        private IState _active;

        public StateMachine(params IState[] states)
        {
            if (states is null) throw new ArgumentNullException(nameof(states));
            if (states.Length == 0) throw new ArgumentException($"{nameof(states)} statemachine must have at least one state");

            _states = states.ToDictionary(x => x.GetType());
        }

        public void Start<TState>() where TState : T
        {
            Set<TState>();
        }

        public void Set<TState>() where TState : T
        {
            if(!_states.TryGetValue(typeof(TState), out var state))
            {
                throw new ArgumentException($"{typeof(TState).FullName} does not exist in statemachine");
            }

            if(_active != null)
            {
                _active.OnLeave();
            }

            _active = state;
            _active.OnEnter();
        }

        public void Update()
        {
            _active?.OnUpdate();
        }
    }
}