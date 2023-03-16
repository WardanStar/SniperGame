using System;
using Cysharp.Threading.Tasks;
using Tools.WTools;

namespace Game.CameraStateMachine
{
	public class LookAtResultCameraState : State<ICameraState>
	{
		private readonly float _timeToCameraLookResult;

		public LookAtResultCameraState(
			StateMachine<ICameraState> stateMachine,
			float timeToCameraLookResult
			) : base(stateMachine)
		{
			_timeToCameraLookResult = timeToCameraLookResult;
		}

		public override void OnEnter()
		{
			LookAtResult();
		}

		private async void LookAtResult()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(_timeToCameraLookResult));
			StateMachine.SetState<ReturnToStartPositionCameraState>();
		}
	}
}