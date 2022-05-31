using System;
using System.Collections.Generic;

namespace Rokurei.FSM {
    public class Fsm<TTrigger> : IFsm<TTrigger> {
        private readonly Dictionary<Type, IState> _states = new();
        private readonly Dictionary<(Type, TTrigger), Type> _transitions = new();
        private (IState State, Type Type) _currentState;

        public void AddState<TState>(TState state) where TState : IState {
            _states[typeof(TState)] = state;
        }
        

        public void AddTransitionBetween<TStateFrom, TStateTo>(TTrigger trigger) where TStateFrom : IState
                                                                                 where TStateTo : IState {
            var typeFrom = typeof(TStateFrom);
            var typeTo = typeof(TStateTo);

            _transitions[(typeFrom, trigger)] = typeTo;
        }

        public void AddTransitionStart<TStateTo>(TTrigger trigger) where TStateTo : IState {
            var typeTo = typeof(TStateTo);
            _transitions[(null, trigger)] = typeTo;
        }

        public async void Fire(TTrigger trigger) {
            var transitionTo = (_currentState.Type, trigger);

            if (!_transitions.ContainsKey(transitionTo)) {
                throw new Exception($"Transition of type {transitionTo.Type} by {trigger} not exist");
            }

            var typeTo = _transitions[transitionTo];

            if (_currentState.State is IStateProcessExit exitState) {
                await exitState.ProcessExit();
            }

            _currentState = (_states[typeTo], typeTo);

            if (_currentState.State is IStateProcessEnter enterState) {
                await enterState.ProcessEnter();
            }
        }
    }
}