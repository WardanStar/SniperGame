using Tools.WTools;

namespace Game.CameraStateMachine
{
	public class LookAtBulletCameraState : State<ICameraState>
	{
		public LookAtBulletCameraState(StateMachine<ICameraState> stateMachine) : base(stateMachine)
		{
		}
	}
}