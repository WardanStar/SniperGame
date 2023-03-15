using Additional.Enums;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Settings/GameSettings")]
	public class GameSettings : ScriptableObject
	{
		public string DefaultWeaponID => _defaultWeaponID.ToString();
		public float SpeedCameraRotation => _speedCameraRotation;
		public Vector3 IndentCameraWithBullet => _indentCameraWithBullet;
		public float CameraMovementTimeToTheTarget => _cameraMovementTimeToTheTarget;
		public float MAXCameraSpeed => _maxCameraSpeed;

		[SerializeField] private WeaponId _defaultWeaponID;
		[SerializeField] private float _speedCameraRotation;
		[SerializeField] private Vector3 _indentCameraWithBullet;
		[SerializeField] private float _cameraMovementTimeToTheTarget;
		[SerializeField] private float _maxCameraSpeed;
	}
}