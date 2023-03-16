using Cysharp.Threading.Tasks;
using InputSystem;
using ProjectSystems;
using Settings;
using Signals;
using Tools.DTools;
using Tools.WTools;
using UI.Forms;
using UniRx;
using Zenject;

namespace Game.Systems
{
	public class LevelEndedSystem : IInitializable
	{
		private readonly IJoystick _joystick;
		private readonly SignalBus _signalBus;
		private readonly WeaponControlSystem _weaponControlSystem;
		private readonly ScoreCounter _scoreCounter;
		private readonly GameSettings _gameSettings;
		private readonly UIFormControlSystem _uiFormControlSystem;
		private readonly ContextDisposable _contextDisposable;

		public LevelEndedSystem(
			IJoystick joystick,
			SignalBus signalBus,
			WeaponControlSystem weaponControlSystem,
			ScoreCounter scoreCounter,
			GameSettings gameSettings,
			UIFormControlSystem uiFormControlSystem,
			ContextDisposable contextDisposable
		)
		{
			_joystick = joystick;
			_signalBus = signalBus;
			_weaponControlSystem = weaponControlSystem;
			_scoreCounter = scoreCounter;
			_gameSettings = gameSettings;
			_uiFormControlSystem = uiFormControlSystem;
			_contextDisposable = contextDisposable;
		}
		
		public void Initialize()
		{
			_signalBus.GetStream<CameraReturnToStartPositionSignal>().Subscribe(_ =>
			{
				if (_scoreCounter.Score.Value < _gameSettings.QuantityScoreOnVictory)
				{
					if (_weaponControlSystem.Ammunition.Value == 0)
					{
						_uiFormControlSystem.ShowForm<LostForm>(true, 1f).Forget();
						return;
					}
					
					_joystick.ChangeActive(true);
					return;
				}
				
				_uiFormControlSystem.ShowForm<VictoryForms>(true, 1f).Forget();
				_signalBus.Fire<NextLevelSignal>();

			}).AddTo(_contextDisposable);
		}
	}
}