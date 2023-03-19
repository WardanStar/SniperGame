using System;

namespace Tools.WTools
{
    public interface IStateMachine<T>
    {
        /// <summary>
        /// Makes the state with the specified type active.
        /// </summary>
        /// <typeparam name="TSetState">Activated state type</typeparam>
        public void SetState<TSetState>() where TSetState : State<T>;
        
        /// <summary>
        /// Returns a data container of the specified type.
        /// </summary>
        /// <typeparam name="TDataContainer">Type of container required.</typeparam>
        /// <returns></returns>
        public TDataContainer GetStateDataContainer<TDataContainer>() where TDataContainer : StateDataContainer<T>;
        
        /// <summary>
        /// Кesets the state machine to its original state.
        /// </summary>
        public void ResetStateMachine();
    }
}