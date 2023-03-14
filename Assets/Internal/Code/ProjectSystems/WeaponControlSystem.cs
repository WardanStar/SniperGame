using System;
using Settings;
using UnityEngine;

namespace ProjectSystems
{
	public class WeaponControlSystem
	{
		private readonly WeaponStorage _weaponStorage;
		private WeaponStorage.Weapon _currentWeapon;

		public WeaponControlSystem(
			WeaponStorage weaponStorage,
			GameSettings gameSettings
			)
		{
			_weaponStorage = weaponStorage;
			SetWeapon(gameSettings.DefaultWeaponID);
		}

		public void Shoot(Vector3 startPosition)
		{
			
		}

		public float GetSpeedAiming()
		{
			//TODO
			
			switch (_currentWeapon.SpeedAiming)
			{
				case WeaponStorage.SpeedAiming.First:
					return 1f;
				case WeaponStorage.SpeedAiming.Second:
					return 2f;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		public void SetWeapon(string id)
		{
			foreach (WeaponStorage.Weapon weapon in _weaponStorage.Weapons)
			{
				if (weapon.ID != id) continue;
				
				_currentWeapon = weapon;
				return;
			}

			throw new NullReferenceException("No weapons found for this id");
		}
	}
}