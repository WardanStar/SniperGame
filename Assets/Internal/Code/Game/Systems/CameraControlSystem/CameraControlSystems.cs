using Game.CameraStateMachine;
using Game.Data;
using Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
	public class CameraControlSystems : IInitializable
	{
		private readonly CameraStateMachine.CameraStateMachine _cameraStateMachine;
		private readonly SignalBus _signalBus;
		private readonly Camera _mainCamera;


		public CameraControlSystems(
			CameraStateMachine.CameraStateMachine cameraStateMachine,
			SceneResourcesStorage sceneResourcesStorage, 
			SignalBus signalBus
			)
		{
			_cameraStateMachine = cameraStateMachine;
			_signalBus = signalBus;
			_mainCamera = sceneResourcesStorage.Camera;
		}
		
		public void Initialize()
		{
			_signalBus.GetStream<CameraSetPositionSignal>().Subscribe(signal => ChangeCameraPosition(signal.Position));
			_signalBus.GetStream<PreparationGameSignal>().Subscribe(signal => _cameraStateMachine.SetState<AimingCameraState>());
			
		}

		private void ChangeCameraPosition(Vector3 position)
		{
			_mainCamera.transform.position = position;
		}
	}
}