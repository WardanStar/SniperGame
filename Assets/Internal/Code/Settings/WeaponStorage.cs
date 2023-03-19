using System;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/WeaponStorage")]
    public class WeaponStorage : ScriptableObject
    {
        [Serializable]
        public class Weapon
        {
            public string ID => _id;
            public string BulletID => _bulletID;
            public string AmmunitionIconId => _ammunitionIconId;
            public int Ammunition => _ammunition;
            public int QuantityBulletAtShot => _quantityBulletAtShot;
            public float SightShiftSpeedWhenAiming => _sightShiftSpeedWhenAiming;
            public float ScoringRatio => _scoringRatio;

            [SerializeField] private string _id;
            [SerializeField] private string _bulletID;
            [SerializeField] private string _ammunitionIconId;
            [SerializeField] private int _ammunition;
            [SerializeField] private int _quantityBulletAtShot;
            [SerializeField] private float _sightShiftSpeedWhenAiming;
            [SerializeField, Range(0, 1)] private float _scoringRatio;
        }

        [SerializeField] private Weapon[] _weapons;

        /// <summary>
        /// Return weapon settings by id
        /// </summary>
        /// <param name="id">Weapon id</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">No weapons found for this id</exception>
        public Weapon GetWeapon(string id)
        {
            foreach (Weapon weapon in _weapons)
            {
                if (weapon.ID != id) continue;
                
                return weapon;
            }

            throw new NullReferenceException("No weapons found for this id");
        }
    }
}