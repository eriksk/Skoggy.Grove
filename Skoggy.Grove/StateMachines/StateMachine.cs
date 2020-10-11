using System;
using System.Collections.Generic;
using System.Linq;

namespace Skoggy.Grove.StateMachines
{
    public class StateMachine<T> where T : IState
    {
        private Dictionary<Type, T> _states;
        private T _active;

        public StateMachine(params T[] states)
        {
            _states = states?.ToDictionary(x => x.GetType()) ?? new Dictionary<Type, T>();
        }

        public void Add(T state)
        {
            _states.Add(state.GetType(), state);
        }

        public T Active => _active;

        public void Start<TState>() where TState : T
        {
            Set<TState>();
        }

        public TState Get<TState>() where TState : T 
        {
            return (TState)_states[typeof(TState)];
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