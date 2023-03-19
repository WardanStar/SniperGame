using Game.Data;
using Settings;
using Signals;
using Tools.WTools;
using UnityEngine;
using Zenject;

namespace Game.CameraStateMachine
{
    public class ReturnToStartPositionCameraState : State<ICameraState>
    {
        private readonly SignalBus _signalBus;
        
        private readonly ReturnToStartPositionDataContainer _returnToStartPositionDataContainer;
        private readonly Transform _cameraTransform;
        private readonly float _speedMoveReturnCameraToStartPosition;
        private readonly float _speedRotateCameraToStartRotation;

        public ReturnToStartPositionCameraState(
            StateMachine<ICameraState> stateMachine,
            SignalBus signalBus,
            ReturnToStartPositionDataContainer returnToStartPositionDataContainer,
            SceneResourcesStorage sceneResourcesStorage,
            GameSettings gameSettings
            ) : base(stateMachine)
        {
            _returnToStartPositionDataContainer = returnToStartPositionDataContainer;
            _cameraTransform = sceneResourcesStorage.Camera.transform;
            _speedMoveReturnCameraToStartPosition = gameSettings.SpeedMoveReturnCameraOnStartPosition;
            _speedRotateCameraToStartRotation = gameSettings.SpeedRotateCameraOnStartRotation;
            _signalBus = signalBus;
        }

        public override void Tick()
        {
            Transform cameraTransform = _cameraTransform;
            var startPosition = _returnToStartPositionDataContainer.StartPosition;
            
            Vector3 nextCameraPosition = Vector3.MoveTowards(_cameraTransform.position,
                startPosition, _speedMoveReturnCameraToStartPosition * Time.deltaTime);

            var nextCameraRotation = Vector3.MoveTowards(_cameraTransform.rotation.eulerAngles,
                _returnToStartPositionDataContainer.StartRotation, _speedRotateCameraToStartRotation * Time.deltaTime);
            
            if ((cameraTransform.position - startPosition).magnitude < 0.01f)
            {
                StateMachine.SetState<IdleCameraState>();
                return;
            }
            
            cameraTransform.position = nextCameraPosition;
            cameraTransform.eulerAngles = nextCameraRotation;
        }

        public override void OnExit()
        {
            _signalBus.Fire<CameraReturnToStartPositionSignal>();
        }
    }
}