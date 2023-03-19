using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Zenject;

namespace Tools.WTools
{
    public abstract class StateMachine<T> : IStateMachine<T>, ITickable, IFixedTickable, ILateTickable
    {
        public event Action ResetStatesData;

        private State<T> _currentState;
        private State<T> _startState;

        private Dictionary<Type, State<T>> _states;
        private Dictionary<Type, object> _statesDataConatiners;

        protected void SetStates(Dictionary<Type, State<T>> states)
        {
            _states = states;
        }


        public void SetState<TSetState>() where TSetState : State<T>
        {
            if (_states.TryGetValue(typeof(TSetState), out State<T> dictionaryState) is false)
                throw new NullReferenceException();

            if (ReferenceEquals(_currentState, null))
                _startState = dictionaryState;
            else
                _currentState.OnExit();

            _currentState = dictionaryState;
            _currentState.OnEnter();
#if STATE_MACHINE_DEBUGING
            DisplayState(typeof(TSetState));
#endif
        }

        public void SetDataContainers(Dictionary<Type, object> dataContainers)
        {
            _statesDataConatiners = dataContainers;
        }
        
        public TDataContainer GetStateDataContainer<TDataContainer>() where TDataContainer : StateDataContainer<T>
        {
            if (_statesDataConatiners.TryGetValue(typeof(TDataContainer), out object obj) is false)
                throw new NullReferenceException();

            if (obj is TDataContainer value)
                return value;
            
            throw new NullReferenceException(); 
        }

        public void ResetStateMachine()
        {
            ResetStatesData?.Invoke();
            _currentState = _startState;
        }

        public virtual void Tick()
        {
            _currentState?.Tick();
        }

        public virtual void FixedTick()
        {
            _currentState?.FixedTick();
        }

        public virtual void LateTick()
        {
            _currentState?.LateTick();
        }

#if STATE_MACHINE_DEBUGING
        private void DisplayState(Type typeState)
        {
            string nameStateMachine = Regex.Replace(GetType().Name, "StateMachine", String.Empty);
            string regexState = Regex.Replace(typeState.Name, "State", String.Empty);
            regexState = Regex.Replace(regexState, $"{nameStateMachine}", String.Empty);
            
            Debug.LogError($"{nameStateMachine} : {regexState}");
        }
#endif
    }
}