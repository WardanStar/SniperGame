using Cysharp.Threading.Tasks;
using InputSystem;
using Tools.WTools;
using UI.Forms;
using Zenject;

namespace ProjectSystems
{
    public class MenuProjectState : State<IProjectState>
    {
        private readonly IJoystick _joystick;
        private readonly SignalBus _signalBus;
        private readonly UIFormControlSystem _uiFormControlSystem;

        public MenuProjectState(
            StateMachine<IProjectState> stateMachine,
            IJoystick joystick,
            SignalBus signalBus,
            UIFormControlSystem uiFormControlSystem
            ) : base(stateMachine)
        {
            _joystick = joystick;
            _signalBus = signalBus;
            _uiFormControlSystem = uiFormControlSystem;
        }

        public override void OnEnter()
        {
            _joystick.ChangeActive(false);
            _uiFormControlSystem.ShowForm<MenuForm>().Forget();
        }

        public override void OnExit()
        {
            _uiFormControlSystem.HideForm<MenuForm>().Forget();
        }
    }
}