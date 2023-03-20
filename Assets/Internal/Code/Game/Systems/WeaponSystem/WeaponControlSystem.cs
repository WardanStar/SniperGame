using System;
using Additional;
using Cysharp.Threading.Tasks;
using Game.Data;
using InputSystem;
using Settings;
using Signals;
using Tools.DTools;
using Tools.WTools;
using UI.Forms;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectSystems
{
    public class WeaponControlSystem
    {
        private readonly IArm _arm;
        private readonly IJoystick _joystick;
        private readonly SignalBus _signalBus;
        private readonly WeaponInfo _weaponInfo;
        private readonly GameSettings _gameSettings;
        private readonly SceneResourcesStorage _sceneResourcesStorage;
        private readonly DirectionsAppearanceBulletsAtMultiShot _directionsAppearanceBulletsAtMultiShot;
        private readonly LevelsDataControlSystem _levelsDataControlSystem;
        private readonly UIFormControlSystem _uiFormControlSystem;
        private readonly ContextDisposable _contextDisposable;
        private bool _isPreparationShot;
        
        public WeaponControlSystem(
            IArm arm,
            IJoystick joystick,
            SignalBus signalBus,
            WeaponInfo weaponInfo,
            GameSettings gameSettings,
            SceneResourcesStorage sceneResourcesStorage,
            DirectionsAppearanceBulletsAtMultiShot directionsAppearanceBulletsAtMultiShot,
            LevelsDataControlSystem levelsDataControlSystem,
            UIFormControlSystem uiFormControlSystem,
            ContextDisposable contextDisposable
            )
        {
            _arm = arm;
            _joystick = joystick;
            _signalBus = signalBus;
            _weaponInfo = weaponInfo;
            _gameSettings = gameSettings;
            _sceneResourcesStorage = sceneResourcesStorage;
            _directionsAppearanceBulletsAtMultiShot = directionsAppearanceBulletsAtMultiShot;
            _levelsDataControlSystem = levelsDataControlSystem;
            _uiFormControlSystem = uiFormControlSystem;
            _contextDisposable = contextDisposable;
            
            _joystick.OnStartAiming.Where(isStartAiming => isStartAiming).Subscribe(_ =>
                {
                    if (_isPreparationShot)
                        return;
                    
                    Aiming().Forget();
                    
                    _isPreparationShot = true;
                })
                .AddTo(_contextDisposable);
        }

        private async UniTaskVoid Aiming()
        {
            PreShotReportForm preShotReportForm = await _uiFormControlSystem.ShowForm<PreShotReportForm>();
            
            float aimingTime = _gameSettings.AimingTime;
            
            preShotReportForm.SetText($"{aimingTime}...");
            
            for (int i = 0; i < _gameSettings.AimingTime; i++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));

                aimingTime--;
                preShotReportForm.SetText($"{aimingTime}...");
            }
            
            _uiFormControlSystem.HideForm<PreShotReportForm>().Forget();
            
            _isPreparationShot = false;
            
            Shoot();
        }
        
        private void Shoot()
        {
            Transform cameraTransform = _sceneResourcesStorage.Camera.transform;

            string bulletCollectionID = ConstantKeys.BULLETS_COLLECTION_ID;
            string bulletId = _weaponInfo.GetCurrentWeapon().BulletID;
            
            IPoolObject bullet = _arm.PoolObjectGetter.GetPoolObject(bulletCollectionID, bulletId,
                cameraTransform.position + (_gameSettings.ShootingDistanceFromTheCamera *Vector3.forward), cameraTransform.rotation);

            for (int i = 1; i < _weaponInfo.GetCurrentWeapon().QuantityBulletAtShot; i++)
            {
                _arm.PoolObjectGetter.GetPoolObject(bulletCollectionID, bulletId,
                    cameraTransform.position +
                    (_gameSettings.ShootingDistanceFromTheCamera * Vector3.forward) + 
                    new Vector3(_directionsAppearanceBulletsAtMultiShot.DirectionsBullet[i].XValue, _directionsAppearanceBulletsAtMultiShot.DirectionsBullet[i].YValue, 0f *
                     ((float)_levelsDataControlSystem.GetCurrentLevel().SizeTargetElement / 2 - bullet.GetTransform().localScale.x)),
                    cameraTransform.rotation);
            }

            _weaponInfo.DecreaseAmmunition();
            
            _joystick.ChangeActive(false);
            
            _signalBus.Fire(new ShootSignal(){BulletTransform = bullet.GetTransform()});
        }
    }
}