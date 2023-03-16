using System;
using Cysharp.Threading.Tasks;
using InputSystem;
using ProjectSystems;
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
		private readonly WeaponControlSystem _weaponControlSystem;
		private readonly UIFormControlSystem _uiFormControlSystem;
		private readonly Transform _cameraTransform;
		private readonly float _speedCameraRotation;
		private readonly float _minValueBias;
		private readonly float _maxValueBias;
		private readonly float _minTimeBias;
		private readonly float _maxTimeBias;
		private readonly float _speedBias;

		private IDisposable _biasDisposable;
		private Vector3 _directionBias;

		public AimingCameraState(
			StateMachine<ICameraState> stateMachine,
			IJoystick joystick,
			WeaponControlSystem weaponControlSystem,
			UIFormControlSystem uiFormControlSystem,
			Transform cameraTransform,
			float speedCameraRotation,
			float minValueBias,
			float maxValueBias,
			float minTimeBias,
			float maxTimeBias,
			float speedBias
			) : base(stateMachine)
		{
			_joystick = joystick;
			_weaponControlSystem = weaponControlSystem;
			_uiFormControlSystem = uiFormControlSystem;
			_cameraTransform = cameraTransform;
			_speedCameraRotation = speedCameraRotation;
			_minValueBias = minValueBias;
			_maxValueBias = maxValueBias;
			_minTimeBias = minTimeBias;
			_maxTimeBias = maxTimeBias;
			_speedBias = speedBias;
		}

		public override void OnEnter()
		{
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
			_cameraTransform.transform.Rotate(_speedBias * Time.deltaTime * _directionBias);
		}
		
		private void ChangeBiasRotate()
		{
			_directionBias = new Vector3(Random.Range(_minValueBias, _maxValueBias),
				Random.Range(_minValueBias, _maxValueBias), 0f).normalized;

			_biasDisposable = Observable.Timer(TimeSpan.FromSeconds(Random.Range(_minTimeBias, _maxTimeBias)))
				.Subscribe(_ =>
				{
					_biasDisposable.Dispose();
					ChangeBiasRotate();
				});
		}
	}
}