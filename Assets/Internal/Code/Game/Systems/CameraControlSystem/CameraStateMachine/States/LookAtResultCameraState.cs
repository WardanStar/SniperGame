using System;
using Cysharp.Threading.Tasks;
using Settings;
using Tools.WTools;

namespace Game.CameraStateMachine
{
    public class LookAtResultCameraState : State<ICameraState>
    {
        private readonly float _timeToCameraLookResult;

        public LookAtResultCameraState(
            StateMachine<ICameraState> stateMachine,
            GameSettings gameSettings
            ) : base(stateMachine)
        {
            _timeToCameraLookResult = gameSettings.TimeToCameraLookResult;
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