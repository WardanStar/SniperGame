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
		private readonly float _distanceToTheCameraTransitionToTheResultMode;
		private readonly LookAtBulletDataContainer _lookAtBulletContainer;
		private Vector3 _currentVelocity;

		public LookAtBulletCameraState(
			StateMachine<ICameraState> stateMachine,
			LookAtBulletDataContainer lookAtBulletDataContainer,
			Transform cameraTransform,
			Vector3 indentCameraWithBullet,
			float cameraMovementTimeToTheTarget,
			float maxCameraSpeed,
			float distanceToTheCameraTransitionToTheResultMode
			) : base(stateMachine)
		{
			_cameraTransform = cameraTransform;
			_indentCameraWithBullet = indentCameraWithBullet;
			_cameraMovementTimeToTheTarget = cameraMovementTimeToTheTarget;
			_maxCameraSpeed = maxCameraSpeed;
			_distanceToTheCameraTransitionToTheResultMode = distanceToTheCameraTransitionToTheResultMode;
			_lookAtBulletContainer = lookAtBulletDataContainer;
		}

		public override void Tick()
		{
			Transform bulletTransform = _lookAtBulletContainer.BulletTransform;
			Transform cameraTransform = _cameraTransform;
			
			Vector3 nextCameraPosition = Vector3.SmoothDamp(_cameraTransform.position,
				bulletTransform.position + _indentCameraWithBullet,
				ref _currentVelocity, _cameraMovementTimeToTheTarget,
				_maxCameraSpeed);

			if (nextCameraPosition.z > -_distanceToTheCameraTransitionToTheResultMode)
			{
				StateMachine.SetState<LookAtResultCameraState>();
				return;
			}
			
			cameraTransform.position = nextCameraPosition;
				

			cameraTransform.LookAt(bulletTransform);
		}
	}
}