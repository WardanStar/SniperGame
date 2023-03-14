using Game.Data;
using Game.Misc;
using InputSystem;
using ProjectSystems;
using Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
	public class LevelStarter
	{
		private readonly SignalBus _signalBus;
		private readonly LevelsDataControlSystem _levelsDataControlSystem;
		private readonly SceneResourcesStorage _sceneResourcesStorage;

		public LevelStarter(
			IJoystick joystick,
			SignalBus signalBus,
			LevelsDataControlSystem levelsDataControlSystem,
			SceneResourcesStorage sceneResourcesStorage,
			ContextDisposable contextDisposable
			)
		{
			_signalBus = signalBus;
			_levelsDataControlSystem = levelsDataControlSystem;
			_sceneResourcesStorage = sceneResourcesStorage;
			joystick.OnStartAiming.Where(isStart => isStart).Subscribe(_ => StartGame()).AddTo(contextDisposable);
		}

		private void StartGame()
		{
			var currentLevel = _levelsDataControlSystem.GetCurrentLevel();
			var targetSettings = currentLevel.TargetSettings;
			
			_signalBus.Fire<PreparationGameSignal>();

			Vector3 sniperPosition = 
				_sceneResourcesStorage.TargetSpawnPoint.position - 
				(currentLevel.DistanceToTarget * Vector3.forward) + 
				((targetSettings.TargetCubes.Length - 1 + 
				  targetSettings.WeighCenterCube * 0.5f) * Vector3.up);

			_signalBus.Fire(new CameraSetPositionSignal(){ Position = sniperPosition });

			Cursor.visible = false;
		}
	}
}