using Additional;
using Game.Data;
using InputSystem;
using Save;
using Save.Weapon;
using Settings;
using Signals;
using Tools.DTools;
using UniRx;
using Zenject;

namespace ProjectSystems
{
    public class WeaponInfo : IInitializable
    {
        /// <summary>
        /// Returns a reactive property with the value of the amount of ammo the selected weapon currently has.
        /// </summary>
        public IReadOnlyReactiveProperty<int> Ammunition => _ammunition;

        private readonly WeaponStorage _weaponStorage;
        private readonly WeaponSaveDataSystem _weaponSaveDataSystem;
        private readonly SignalBus _signalBus;
        private readonly ContextDisposable _contextDisposable;

        private readonly ReactiveProperty<int> _ammunition = new(); 
        private WeaponStorage.Weapon _currentWeapon;
        private bool _preparationToShoot;

        public WeaponInfo(
            ISaveDataControlSystem saveDataControlSystem,
            WeaponStorage weaponStorage,
            SignalBus signalBus,
            ContextDisposable contextDisposable
        )
        {
            _weaponStorage = weaponStorage;
            _weaponSaveDataSystem = saveDataControlSystem.WeaponSaveDataSystem;
            _signalBus = signalBus;
            _contextDisposable = contextDisposable;
        }
        
        public void Initialize() =>
            _signalBus.GetStream<StartGameSignal>().Subscribe(_ => SetWeapon(_weaponSaveDataSystem.GetIDCurrentWeapon())).AddTo(_contextDisposable);
        
        /// <summary>
        /// Returns the settings for the currently selected weapon.
        /// </summary>
        /// <returns></returns>
        public WeaponStorage.Weapon GetCurrentWeapon() =>
            _currentWeapon;

        /// <summary>
        /// Reduction by one piece of ammunition
        /// </summary>
        public void DecreaseAmmunition() =>
            _ammunition.Value--;

        private void SetWeapon(string id)
        {
            WeaponStorage.Weapon currentWeapon = _weaponStorage.GetWeapon(id);
            
            _currentWeapon = currentWeapon;
            _ammunition.Value = currentWeapon.Ammunition;
        }
    }
}