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

		public QueueLoadingGame(
			ISaveDataControlSystem saveDataControlSystem,
			UIFormControlSystem uiFormControlSystem
			)
		{
			_saveDataControlSystem = saveDataControlSystem;
			_uiFormControlSystem = uiFormControlSystem;
			_uiFormControlSystem.OnReady.Where(isReady => isReady).Subscribe(_ => LoadingGame());
		}

		private void LoadingGame()
		{
			_saveDataControlSystem.Initialize();
			
			_uiFormControlSystem.ShowForm<MenuForm>().Forget();
		}
	}
}