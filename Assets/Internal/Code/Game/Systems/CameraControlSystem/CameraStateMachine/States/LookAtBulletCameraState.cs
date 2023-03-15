using Tools.WTools;
using UnityEngine;

namespace Game.CameraStateMachine
{
	public class LookAtBulletCameraState : State<ICameraState>
	{
		private readonly Transform _cameraTransform;
		private readonly Vector3 _indentCameraWithBullet;
		private readonly float _cameraMovementTimeToTheTarget;
		private readonly float _maxCameraSpeed;
		private readonly LookAtBulletDataContainer _lookAtBulletContainer;
		private Vector3 _currentVelocity;

		public LookAtBulletCameraState(
			StateMachine<ICameraState> stateMachine,
			LookAtBulletDataContainer lookAtBulletDataContainer,
			Transform cameraTransform,
			Vector3 indentCameraWithBullet,
			float cameraMovementTimeToTheTarget,
			float maxCameraSpeed
			) : base(stateMachine)
		{
			_cameraTransform = cameraTransform;
			_indentCameraWithBullet = indentCameraWithBullet;
			_cameraMovementTimeToTheTarget = cameraMovementTimeToTheTarget;
			_maxCameraSpeed = maxCameraSpeed;
			_lookAtBulletContainer = lookAtBulletDataContainer;
		}

		public override void Tick()
		{
			_cameraTransform.position =
				Vector3.SmoothDamp(_cameraTransform.position,
					_lookAtBulletContainer.BulletTransform.position + _indentCameraWithBullet,
					ref _currentVelocity, _cameraMovementTimeToTheTarget,
					_maxCameraSpeed);
		}
	}
}