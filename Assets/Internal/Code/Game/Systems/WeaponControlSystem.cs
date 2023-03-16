using System;
using Additional;
using Game.Data;
using InputSystem;
using Save;
using Save.Weapon;
using Settings;
using Signals;
using Tools.DTools;
using Tools.WTools;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectSystems
{
	public class WeaponControlSystem : IInitializable
	{
		public IReadOnlyReactiveProperty<int> Ammunition => _ammunition;

		private readonly IArm _arm;
		private readonly IJoystick _joystick;
		private readonly WeaponStorage _weaponStorage;
		private readonly SceneResourcesStorage _sceneResourcesStorage;
		private readonly WeaponSaveDataSystem _weaponSaveDataSystem;
		private readonly SignalBus _signalBus;
		private readonly GameSettings _gameSettings;
		private readonly ContextDisposable _contextDisposable;

		private readonly ReactiveProperty<int> _ammunition = new(); 
		private WeaponStorage.Weapon _currentWeapon;
		private bool _preparationToShoot;

		public WeaponControlSystem(
			IArm arm,
			IJoystick joystick,
			ISaveDataControlSystem saveDataControlSystem,
			WeaponStorage weaponStorage,
			SceneResourcesStorage sceneResourcesStorage,
			SignalBus signalBus,
			GameSettings gameSettings,
			ContextDisposable contextDisposable
		)
		{
			_arm = arm;
			_joystick = joystick;
			_weaponStorage = weaponStorage;
			_sceneResourcesStorage = sceneResourcesStorage;
			_weaponSaveDataSystem = saveDataControlSystem.WeaponSaveDataSystem;
			_signalBus = signalBus;
			_gameSettings = gameSettings;
			_contextDisposable = contextDisposable;
		}
		
		public void Initialize()
		{
			_signalBus.GetStream<StartGameSignal>().Subscribe(_ => SetWeapon(_weaponSaveDataSystem.GetIDCurrentWeapon())).AddTo(_contextDisposable);
			
			_joystick.OnStartAiming.Where(isStartAiming => isStartAiming).Subscribe(_ => _preparationToShoot = true)
				.AddTo(_contextDisposable);
			
			_joystick.OnEndAiming.Where(isEndAiming => isEndAiming).Subscribe(_ =>
				{
					if (_preparationToShoot)
					{
						Transform cameraTransform = _sceneResourcesStorage.Camera.transform;
						
						_preparationToShoot = false;
						IPoolObject bullet = _arm.PoolObjectGetter.GetPoolObject(ConstantKeys.BULLETS_COLLECTION_ID, ConstantKeys.DEFAULT_BULLET_ID,
							cameraTransform.position + (2f *Vector3.forward), cameraTransform.rotation);
						
						_signalBus.Fire(new ShootSignal(){BulletTransform = bullet.GetTransform()});
					}
				}).AddTo(_contextDisposable);
		}

		public WeaponStorage.Weapon GetCurrentWeapon() =>
			_currentWeapon;
		
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
		
		private void SetWeapon(string id)
		{
			foreach (WeaponStorage.Weapon weapon in _weaponStorage.Weapons)
			{
				if (weapon.ID != id) continue;
				
				_currentWeapon = weapon;
				_ammunition.Value = weapon.Ammunition;
				return;
			}

			throw new NullReferenceException("No weapons found for this id");
		}

		
	}
}