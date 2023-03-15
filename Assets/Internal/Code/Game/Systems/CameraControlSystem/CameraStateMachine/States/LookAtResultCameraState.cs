using Tools.WTools;

namespace Game.CameraStateMachine
{
	public class LookAtResultCameraState : State<ICameraState>
	{
		public LookAtResultCameraState(
			StateMachine<ICameraState> stateMachine
			) : base(stateMachine)
		{
		}
	}
}