using System;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Settings/WeaponStorage")]
	public class WeaponStorage : ScriptableObject
	{
		public enum ShootingMode
		{
			OneBullet,
			FiveBullet
		}

		public enum SpeedAiming
		{
			First,
			Second
		}
		
		[Serializable]
		public class Weapon
		{
			public string ID => _id;
			public int Ammunition => _ammunition;
			public int QuantityBulletAtShot => _quantityBulletAtShot;
			public SpeedAiming SpeedAiming => _speedAiming;
			public float ScoringRatio => _scoringRatio;

			[SerializeField] private string _id;
			[SerializeField] private int _ammunition;
			[SerializeField] private int _quantityBulletAtShot;
			[SerializeField] private SpeedAiming _speedAiming;
			[SerializeField, Range(0, 1)] private float _scoringRatio;
		}

		public Weapon[] Weapons => _weapons;

		[SerializeField] private Weapon[] _weapons;
	}
}