using System;

namespace Tools.WTools
{
	public interface IStateMachine<T>
	{
		public void SetState<TSetState>() where TSetState : State<T>;
		public TDataContainer GetStateDataContainer<TDataContainer>(Type typeState) where TDataContainer : StateDataContainer<T>;
		public void ResetStateMachine();
	}
}