using Cysharp.Threading.Tasks;
using Game.Data;
using Settings;
using Signals;
using Tools.WTools;
using UI.Forms;
using UnityEngine;
using Zenject;

namespace Game.CameraStateMachine
{
    public class ReturnToStartPositionCameraState : State<ICameraState>
    {
        private readonly SignalBus _signalBus;
        
        private readonly ReturnToStartPositionDataContainer _returnToStartPositionDataContainer;
        private readonly UIFormControlSystem _uiFormControlSystem;
        private readonly Transform _cameraTransform;
        private readonly float _speedMoveReturnCameraToStartPosition;
        private readonly float _speedRotateCameraToStartRotation;

        private bool _stopRotation;

        public ReturnToStartPositionCameraState(
            StateMachine<ICameraState> stateMachine,
            SignalBus signalBus,
            ReturnToStartPositionDataContainer returnToStartPositionDataContainer,
            UIFormControlSystem uiFormControlSystem,
            SceneResourcesStorage sceneResourcesStorage,
            GameSettings gameSettings
            ) : base(stateMachine)
        {
            _returnToStartPositionDataContainer = returnToStartPositionDataContainer;
            _uiFormControlSystem = uiFormControlSystem;
            _cameraTransform = sceneResourcesStorage.Camera.transform;
            _speedMoveReturnCameraToStartPosition = gameSettings.SpeedMoveReturnCameraOnStartPosition;
            _speedRotateCameraToStartRotation = gameSettings.SpeedRotateCameraOnStartRotation;
            _signalBus = signalBus;
        }

        public override void OnEnter() =>
            _stopRotation = false;

        public override void Tick()
        {
            Transform cameraTransform = _cameraTransform;
            Vector3 startPosition = _returnToStartPositionDataContainer.StartPosition;
            Vector3 startRotation = _returnToStartPositionDataContainer.StartRotation;

            startRotation.y += 360;
            
            Vector3 nextCameraPosition = Vector3.MoveTowards(_cameraTransform.position,
                startPosition, _speedMoveReturnCameraToStartPosition * Time.deltaTime);

            Vector3 nextCameraRotation = Vector3.MoveTowards(_cameraTransform.eulerAngles,
                startRotation, _speedMoveReturnCameraToStartPosition * Time.deltaTime);
            
            if ((cameraTransform.position - startPosition).magnitude < 0.01f)
            {
                StateMachine.SetState<IdleCameraState>();
                return;
            }

            if (nextCameraRotation.y + 360f - startRotation.y < 0.3f)
            {
                cameraTransform.eulerAngles = startRotation;
                _stopRotation = true;
            }
            
            cameraTransform.position = nextCameraPosition;
            if (!_stopRotation)
                cameraTransform.eulerAngles = nextCameraRotation;
        }

        public override void OnExit()
        {
            _uiFormControlSystem.ShowForm<InscriptionBeforeAimingForm>().Forget();
            _signalBus.Fire<CameraReturnToStartPositionSignal>();
        }
    }
}