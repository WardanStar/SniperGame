using Cysharp.Threading.Tasks;
using Signals;
using Tools.WTools;
using UI.Forms;
using Zenject;

namespace ProjectSystems
{
	public class GameProjectState : State<IProjectState>
	{
		private readonly SignalBus _signalBus;
		private readonly UIFormControlSystem _uiFormControlSystem;

		public GameProjectState(
			StateMachine<IProjectState> stateMachine,
			SignalBus signalBus,
			UIFormControlSystem uiFormControlSystem
		) : base(stateMachine)
		{
			_signalBus = signalBus;
			_uiFormControlSystem = uiFormControlSystem;
		}

		public override void OnEnter()
		{
			_uiFormControlSystem.ShowForm<GameForm>().Forget();
			_uiFormControlSystem.ShowForm<YouCanShootForm>().Forget();
			_signalBus.Fire<PlayGameSignal>();
		}

		public override void OnExit()
		{
			_uiFormControlSystem.HideForm<GameForm>().Forget();
			_uiFormControlSystem.HideForm<YouCanShootForm>().Forget();
		}
	}
}