using Additional.Enums;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Settings/GameSettings")]
	public class GameSettings : ScriptableObject
	{
		public string DefaultWeaponID => _defaultWeaponID.ToString();
		public float SpeedCameraRotation => _speedCameraRotation;

		[SerializeField] private WeaponId _defaultWeaponID;
		[SerializeField] private float _speedCameraRotation;
	}
}