using Tools.WTools;

namespace Game.CameraStateMachine
{
	public class IdleCameraState : State<ICameraState>
	{
		public IdleCameraState(StateMachine<ICameraState> stateMachine) : base(stateMachine)
		{
		}
	}
}