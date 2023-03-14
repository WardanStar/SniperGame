using InputSystem;
using Tools.WTools;
using UnityEngine;

namespace Game.CameraStateMachine
{
	public class AimingCameraState : State<ICameraState>
	{
		private readonly IJoystick _joystick;
		private readonly Camera _mainCamera;
		private readonly float _speedCameraRotation;

		public AimingCameraState(
			StateMachine<ICameraState> stateMachine,
			IJoystick joystick,
			Camera mainCamera,
			float speedCameraRotation
			) : base(stateMachine)
		{
			_joystick = joystick;
			_mainCamera = mainCamera;
			_speedCameraRotation = speedCameraRotation;
		}

		public override void Tick()
		{
			var moveVector = _joystick.MoveDirection;
			
			_mainCamera.transform.Rotate(_speedCameraRotation * Time.deltaTime * new Vector3(moveVector.y, -moveVector.x));
		}
	}
}