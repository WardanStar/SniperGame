using UnityEngine;

namespace Tools.WTools
{
    public class KeysInfo
    {
        private readonly KeysSystem _keysSystem;

        public KeysInfo(
            KeysSystem keysSystem
            )
        {
            _keysSystem = keysSystem;
        }
        
        /// <summary>
        /// Returns a value of the specified type at the specified id.
        /// </summary>
        /// <param name="idKey">Specified id.</param>
        /// <typeparam name="T">Specified type values. Available types : int, float, string, bool.</typeparam>
        /// <returns></returns>
        public T GetKeyValue<T>(string idKey) =>
            (T)_keysSystem.GetKey<T>(idKey).Value;
        
        /// <summary>
        /// Whether the stored value at the specified id.
        /// </summary>
        /// <param name="idKey">Specified id.</param>
        /// <returns></returns>
        public bool HasKey(string idKey) =>
            PlayerPrefs.HasKey(idKey);
    }
}