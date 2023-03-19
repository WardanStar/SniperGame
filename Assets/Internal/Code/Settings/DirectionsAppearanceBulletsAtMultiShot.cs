using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/DirectionsAppearanceBulletsAtMultiShot")]
    public class DirectionsAppearanceBulletsAtMultiShot : ScriptableObject
    {
        /// <summary>
        /// Returns an array of directions in which projectiles will be placed when multi-shot
        /// </summary>
        public Vector3[] DirectionsBullet => _directionsBullet;

        [SerializeField] private Vector3[] _directionsBullet;
    }
}