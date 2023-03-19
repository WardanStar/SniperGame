using Cysharp.Threading.Tasks;
using Tools.WTools;
using UI.Forms;

namespace Game.CameraStateMachine
{
    public class IdleCameraState : State<ICameraState>
    {
        private readonly UIFormControlSystem _uiFormControlSystem;

        public IdleCameraState(
            StateMachine<ICameraState> stateMachine,
            UIFormControlSystem uiFormControlSystem
            ) : base(stateMachine)
        {
            _uiFormControlSystem = uiFormControlSystem;
        }

        public override void OnEnter() =>
            _uiFormControlSystem.ShowForm<InscriptionBeforeAimingForm>().Forget();

        public override void OnExit() =>
            _uiFormControlSystem.HideForm<InscriptionBeforeAimingForm>().Forget();
    }
}