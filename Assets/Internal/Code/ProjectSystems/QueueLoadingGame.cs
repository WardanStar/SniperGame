using Cysharp.Threading.Tasks;
using Save;
using Tools.WTools;
using UI.Forms;
using UniRx;

namespace ProjectSystems
{
	public class QueueLoadingGame
	{
		private readonly ISaveDataControlSystem _saveDataControlSystem;
		private readonly UIFormControlSystem _uiFormControlSystem;
		private readonly ProjectStateMachine _projectStateMachine;

		public QueueLoadingGame(
			ISaveDataControlSystem saveDataControlSystem,
			UIFormControlSystem uiFormControlSystem,
			ProjectStateMachine projectStateMachine
			)
		{
			_saveDataControlSystem = saveDataControlSystem;
			_uiFormControlSystem = uiFormControlSystem;
			_projectStateMachine = projectStateMachine;
			_uiFormControlSystem.OnReady.Where(isReady => isReady).Subscribe(_ => LoadingGame());
		}

		private void LoadingGame()
		{
			_saveDataControlSystem.Initialize();
			
			_projectStateMachine.SetState<MenuProjectState>();
		}
	}
}