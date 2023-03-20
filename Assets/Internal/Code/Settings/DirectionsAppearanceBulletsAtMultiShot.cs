using System;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/DirectionsAppearanceBulletsAtMultiShot")]
    public class DirectionsAppearanceBulletsAtMultiShot : ScriptableObject
    {
        [Serializable]
        public class DirectionAppearanceBulletAtMultiShot
        {
            public float XValue => _xValue;
            public float YValue => _yValue;

            [SerializeField, Range(-1, 1)] private float _xValue;
            [SerializeField, Range(-1, 1)] private float _yValue;
        }
        
        /// <summary>
        /// Returns an array of directions in which projectiles will be placed when multi-shot
        /// </summary>
        public DirectionAppearanceBulletAtMultiShot[] DirectionsBullet => _directionsBullet;

        [SerializeField] private DirectionAppearanceBulletAtMultiShot[] _directionsBullet;
    }
}