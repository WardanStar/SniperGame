using System;
using Additional.Enums;
using Save;
using Save.Weapon;
using Settings;
using TMPro;
using Tools.WTools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Forms
{
    public class ChangeWeaponForm : UIForm
    {
        [Serializable]
        public class WeaponButton
        {
            public string WeaponId => _weaponId.ToString();
            public TMP_Text HeaderButton => _headerButton;
            public Button Button => _button;

            [SerializeField] private WeaponId _weaponId;
            [SerializeField] private TMP_Text _headerButton;
            [SerializeField] private Button _button;
            [SerializeField] private Image _backLight;

            public void SelectionWeapon(bool isSelection) =>
                _backLight.gameObject.SetActive(isSelection);
        }

        [SerializeField] private TMP_Text _weaponHeaderText;
        [SerializeField] private TMP_Text _ammunitionText;
        [SerializeField] private TMP_Text _quantityBulletAtShotText;
        [SerializeField] private TMP_Text _sightShiftSpeedWhenAimingText;
        [SerializeField] private TMP_Text _scoringRatioText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private WeaponButton[] _weaponButtons;

        private WeaponStorage.Weapon _selectWeapon;
        private WeaponStorage _weaponStorage;
        private ISaveDataControlSystem _saveDataControlSystem;

        [Inject]
        public void Construct(
            ISaveDataControlSystem saveDataControlSystem,
            WeaponStorage weaponStorage
            )
        {
            _weaponStorage = weaponStorage;
            _saveDataControlSystem = saveDataControlSystem;
        }

        private void Start()
        {
            WeaponSaveDataSystem weaponSaveDataSystem = _saveDataControlSystem.WeaponSaveDataSystem;

            string currentWeaponID = weaponSaveDataSystem.GetIDCurrentWeapon();
            
            foreach (WeaponButton weaponButton in _weaponButtons)
            {
                if (weaponButton.WeaponId == currentWeaponID)
                    weaponButton.SelectionWeapon(true);
                
                weaponButton.Button.onClick.AddListener(() =>
                    ChangeSelectWeapon(weaponButton.WeaponId));
            }

            _closeButton.onClick.AddListener(() => Hide<ChangeWeaponForm>(true));
            
            _acceptButton.onClick.AddListener(() =>
            {
                ChangeSelectedButton(_selectWeapon.ID);
                weaponSaveDataSystem.SetWeapon(_selectWeapon.ID);
            });
            
            ChangeSelectWeapon(currentWeaponID);
        }

        private void ChangeSelectWeapon(string idWeapon)
        {
            WeaponStorage.Weapon currentWeapon = _weaponStorage.GetWeapon(idWeapon);
            
            _selectWeapon = currentWeapon;
            _weaponHeaderText.text = idWeapon;
            _ammunitionText.text = $"Ammunition : {currentWeapon.Ammunition}";
            _quantityBulletAtShotText.text = $"QuantityBulletAtShot : {currentWeapon.QuantityBulletAtShot}";
            _sightShiftSpeedWhenAimingText.text = $"SightShiftSpeedWhenAiming : {currentWeapon.SightShiftSpeedWhenAiming}";
            _scoringRatioText.text = $"_scoringRatio : {currentWeapon.ScoringRatio}";
        }

        private void ChangeSelectedButton(string weaponID)
        {
            foreach (WeaponButton weaponButton in _weaponButtons)
            {
                if (weaponButton.WeaponId == weaponID)
                {
                    weaponButton.SelectionWeapon(true);
                    continue;
                }
                
                weaponButton.SelectionWeapon(false);
            }
        }
    }
}