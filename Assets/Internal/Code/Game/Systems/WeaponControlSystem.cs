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
		private readonly BulletsQueueStorage _bulletsQueueStorage;
		private readonly LevelsDataControlSystem _levelsDataControlSystem;
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
			BulletsQueueStorage bulletsQueueStorage,
			LevelsDataControlSystem levelsDataControlSystem,
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
			_bulletsQueueStorage = bulletsQueueStorage;
			_levelsDataControlSystem = levelsDataControlSystem;
			_contextDisposable = contextDisposable;
		}
		
		public void Initialize()
		{
			_signalBus.GetStream<StartGameSignal>().Subscribe(_ => SetWeapon(_weaponSaveDataSystem.GetIDCurrentWeapon())).AddTo(_contextDisposable);
			
			_joystick.OnStartAiming.Where(isStartAiming => isStartAiming).Subscribe(_ => _preparationToShoot = true)
				.AddTo(_contextDisposable);
			
			_joystick.OnEndAiming.Where(isEndAiming => isEndAiming).Subscribe(_ => Shoot()).AddTo(_contextDisposable);
		}

		public WeaponStorage.Weapon GetCurrentWeapon() =>
			_currentWeapon;
		
		private void Shoot()
		{
			if (!_preparationToShoot) return;
			
			Transform cameraTransform = _sceneResourcesStorage.Camera.transform;
						
			_preparationToShoot = false;
			IPoolObject bullet = _arm.PoolObjectGetter.GetPoolObject(ConstantKeys.BULLETS_COLLECTION_ID, ConstantKeys.DEFAULT_BULLET_ID,
				cameraTransform.position + (_gameSettings.ShootingDistanceFromTheCamera *Vector3.forward), cameraTransform.rotation);

			for (int i = 1; i < _currentWeapon.QuantityBulletAtShot; i++)
			{
				_arm.PoolObjectGetter.GetPoolObject(ConstantKeys.BULLETS_COLLECTION_ID, ConstantKeys.DEFAULT_BULLET_ID,
					cameraTransform.position + (_gameSettings.ShootingDistanceFromTheCamera * Vector3.forward) + 
					(_bulletsQueueStorage.DirectionsBullet[i] *
					 (((float)_levelsDataControlSystem.GetCurrentLevel().SizeTargetElement / 2) - bullet.GetTransform().localScale.x)),
					cameraTransform.rotation);
			}

			_ammunition.Value--;
			_signalBus.Fire(new ShootSignal(){BulletTransform = bullet.GetTransform()});
		}
		
		private void SetWeapon(string id)
		{
			WeaponStorage.Weapon currentWeapon = _weaponStorage.GetWeapon(id);
			
			_currentWeapon = currentWeapon;
			_ammunition.Value = currentWeapon.Ammunition;
		}
	}
}