using Cysharp.Threading.Tasks;
using InputSystem;
using ProjectSystems;
using Tools.WTools;
using UI.Forms;
using UnityEngine;

namespace Game.CameraStateMachine
{
	public class AimingCameraState : State<ICameraState>
	{
		private readonly IJoystick _joystick;
		private readonly WeaponControlSystem _weaponControlSystem;
		private readonly UIFormControlSystem _uiFormControlSystem;
		private readonly Transform _cameraTransform;
		private readonly float _speedCameraRotation;

		public AimingCameraState(
			StateMachine<ICameraState> stateMachine,
			IJoystick joystick,
			WeaponControlSystem weaponControlSystem,
			UIFormControlSystem uiFormControlSystem,
			Transform cameraTransform,
			float speedCameraRotation
			) : base(stateMachine)
		{
			_joystick = joystick;
			_weaponControlSystem = weaponControlSystem;
			_uiFormControlSystem = uiFormControlSystem;
			_cameraTransform = cameraTransform;
			_speedCameraRotation = speedCameraRotation;
		}

		public override void OnEnter()
		{
			_uiFormControlSystem.ShowForm<AimForm>().Forget();
		}

		public override void OnExit()
		{
			_uiFormControlSystem.HideForm<AimForm>().Forget();
		}

		public override void Tick()
		{
			var moveVector = _joystick.MoveDirection;
			
			_cameraTransform.transform.Rotate(_speedCameraRotation * Time.deltaTime * new Vector3(moveVector.y, -moveVector.x));
		}
	}
}