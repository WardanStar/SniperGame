using Game.CameraStateMachine;
using Game.Data;
using InputSystem;
using ProjectSystems;
using Settings;
using Signals;
using Tools.DTools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
	public class CameraControlSystems : IInitializable
	{
		private readonly IJoystick _joystick;
		private readonly CameraStateMachine.CameraStateMachine _cameraStateMachine;
		private readonly SceneResourcesStorage _sceneResourcesStorage;
		private readonly SignalBus _signalBus;
		private readonly LevelsDataControlSystem _levelsDataControlSystem;
		private readonly ContextDisposable _contextDisposable;
		private Vector3 _startPosition;


		public CameraControlSystems(
			IJoystick joystick,
			CameraStateMachine.CameraStateMachine cameraStateMachine,
			SceneResourcesStorage sceneResourcesStorage, 
			SignalBus signalBus,
			LevelsDataControlSystem levelsDataControlSystem,
			ContextDisposable contextDisposable
			)
		{
			_joystick = joystick;
			_cameraStateMachine = cameraStateMachine;
			_sceneResourcesStorage = sceneResourcesStorage;
			_signalBus = signalBus;
			_levelsDataControlSystem = levelsDataControlSystem;
			_contextDisposable = contextDisposable;
		}
		
		public void Initialize()
		{
			_signalBus.GetStream<ShootSignal>().Subscribe(signal =>
			{
				_joystick.ChangeActive(false);
				var container = _cameraStateMachine.GetStateDataContainer<LookAtBulletDataContainer>(typeof(LookAtBulletDataContainer));
				container.BulletTransform = signal.BulletTransform;
				_cameraStateMachine.SetState<LookAtBulletCameraState>();

			}).AddTo(_contextDisposable);
			
			_signalBus.GetStream<StartGameSignal>().Subscribe(signal =>
			{
				LevelStorage.Level currentLevel = _levelsDataControlSystem.GetCurrentLevel();
				LevelStorage.TargetSettings targetSettings = currentLevel.TargetSettings;
				
				Vector3 sniperPosition = 
					_sceneResourcesStorage.TargetSpawnPoint.position - 
					(currentLevel.DistanceToTarget * Vector3.forward) + 
					((targetSettings.TargetCubes.Length - 1 + 
					  targetSettings.WeighCenterCube * 0.5f) * Vector3.up);
				
				var container = _cameraStateMachine.GetStateDataContainer<ReturnToStartPositionDataContainer>(
					typeof(ReturnToStartPositionDataContainer));

				container.StartPosition = sniperPosition;
				container.StartRotation = _sceneResourcesStorage.Camera.transform.rotation.eulerAngles;
				
				_sceneResourcesStorage.Camera.transform.position = sniperPosition;
				
				_cameraStateMachine.SetState<IdleCameraState>();
			}).AddTo(_contextDisposable);

			_joystick.OnStartAiming.Where(isStartAiming => isStartAiming)
				.Subscribe(_ => _cameraStateMachine.SetState<AimingCameraState>()).AddTo(_contextDisposable);
			
		}
	}
}