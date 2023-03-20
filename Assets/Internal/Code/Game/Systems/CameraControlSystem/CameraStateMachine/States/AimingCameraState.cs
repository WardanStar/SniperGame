using System;
using Cysharp.Threading.Tasks;
using Game.Data;
using InputSystem;
using ProjectSystems;
using Settings;
using Tools.WTools;
using UI.Forms;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.CameraStateMachine
{
    public class AimingCameraState : State<ICameraState>
    {
        private readonly IJoystick _joystick;
        private readonly WeaponInfo _weaponInfo;
        private readonly UIFormControlSystem _uiFormControlSystem;
        private readonly Transform _cameraTransform;
        
        private readonly float _speedCameraRotation;
        private readonly float _minValueBias;
        private readonly float _maxValueBias;
        private readonly float _minSpeedBias;
        private readonly float _maxSpeedBias;
        private readonly float _minTimeBias;
        private readonly float _maxTimeBias;
        
        private IDisposable _biasDisposable;
        private Vector3 _directionBias;
        private float _speedBias;

        public AimingCameraState(
            StateMachine<ICameraState> stateMachine,
            IJoystick joystick,
            WeaponInfo weaponInfo,
            UIFormControlSystem uiFormControlSystem,
            SceneResourcesStorage sceneResourcesStorage,
            GameSettings gameSettings
            ) : base(stateMachine)
        {
            _joystick = joystick;
            _weaponInfo = weaponInfo;
            _uiFormControlSystem = uiFormControlSystem;
            _cameraTransform = sceneResourcesStorage.Camera.transform;
            _speedCameraRotation = gameSettings.SpeedCameraRotation;
            _minSpeedBias = gameSettings.MINSpeedBias;
            _maxSpeedBias = gameSettings.MAXSpeedBias;
            _minTimeBias = gameSettings.MINTimeBias;
            _maxTimeBias = gameSettings.MAXTimeBias;
        }

        public override void OnEnter()
        {
            _uiFormControlSystem.HideForm<InscriptionBeforeAimingForm>().Forget();
            _uiFormControlSystem.ShowForm<AimForm>().Forget();
            ChangeBiasRotate();
        }

        public override void OnExit()
        {
            _biasDisposable.Dispose();
            _uiFormControlSystem.HideForm<AimForm>().Forget();
        }

        public override void Tick()
        {
            ControlRotate();
            BiasRotate();
        }

        private void ControlRotate()
        {
            Vector3 moveVector = _joystick.MoveDirection;
            
            _cameraTransform.transform.Rotate(_speedCameraRotation * Time.deltaTime * new Vector3(moveVector.y, -moveVector.x));
        }

        private void BiasRotate()
        {
            _cameraTransform.transform.Rotate(_speedBias * _weaponInfo.GetCurrentWeapon().SightShiftSpeedWhenAiming * Time.deltaTime * _directionBias);
        }
        
        private void ChangeBiasRotate()
        {
            _directionBias = new Vector3(Random.Range(-1f, 1f),
                Random.Range(-1f, 1f), 0f).normalized;

            _speedBias = Random.Range(_minSpeedBias, _maxSpeedBias);

            _biasDisposable = Observable.Timer(TimeSpan.FromSeconds(Random.Range(_minTimeBias, _maxTimeBias)))
                .Subscribe(_ =>
                {
                    _biasDisposable.Dispose();
                    ChangeBiasRotate();
                });
        }
    }
}