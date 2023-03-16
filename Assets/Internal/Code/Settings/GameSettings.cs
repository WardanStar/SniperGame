using Additional.Enums;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Settings/GameSettings")]
	public class GameSettings : ScriptableObject
	{
		public int QuantityScoreOnVictory => _quantityScoreOnVictory;

		public string DefaultWeaponID => _defaultWeaponID.ToString();

		public float RepulsiveForceAtCollision => _repulsiveForceAtCollision;

		public float BulletSpeed => _bulletSpeed;
		public float ShootingDistanceFromTheCamera => _shootingDistanceFromTheCamera;

		public float SpeedCameraRotation => _speedCameraRotation;
		public Vector3 IndentCameraWithBullet => _indentCameraWithBullet;
		public float CameraMovementTimeToTheTarget => _cameraMovementTimeToTheTarget;
		public float MAXCameraSpeed => _maxCameraSpeed;
		public float DistanceToTheCameraTransitionToTheResultMode => _distanceToTheCameraTransitionToTheResultMode;
		public float TimeToCameraLookResult => _timeToCameraLookResult;
		public float SpeedMoveReturnCameraOnStartPosition => _speedMoveReturnCameraOnStartPosition;
		public float SpeedRotateCameraOnStartRotation => _speedRotateCameraOnStartRotation;

		[Header("GameSettings")]
		[SerializeField] private int _quantityScoreOnVictory;
		
		[Header("WeaponSettings")]
		[SerializeField] private WeaponId _defaultWeaponID;

		[Header("TargetSettings")] 
		[SerializeField] private float _repulsiveForceAtCollision;

		[Header("BulletSettings")]
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private float _shootingDistanceFromTheCamera;
		
		[Header("CameraSettings")]
		[SerializeField] private float _speedCameraRotation;
		[SerializeField] private Vector3 _indentCameraWithBullet;
		[SerializeField] private float _cameraMovementTimeToTheTarget;
		[SerializeField] private float _maxCameraSpeed;
		[SerializeField] private float _distanceToTheCameraTransitionToTheResultMode;
		[SerializeField] private float _timeToCameraLookResult;
		[SerializeField] private float _speedMoveReturnCameraOnStartPosition;
		[SerializeField] private float _speedRotateCameraOnStartRotation;
		
		
		
	}
}