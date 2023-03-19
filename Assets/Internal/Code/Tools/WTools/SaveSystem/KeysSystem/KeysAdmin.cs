using UnityEngine;

namespace Tools.WTools
{
    public class KeysAdmin
    {
        private readonly KeysSystem _keysSystem;

        public KeysAdmin(
            KeysSystem keysSystem
            )
        {
            _keysSystem = keysSystem;
        }
        
        /// <summary>
        /// Set integer key value by id.
        /// </summary>
        /// <param name="idKey">Set value id.</param>
        /// <param name="value">Set value.</param>
        /// <param name="defaultValue">Whether the value being set is the default value.</param>
        public void SetKeyValue(string idKey, int value, bool defaultValue = false) =>
            _keysSystem.SetKey<int>(idKey, value, defaultValue);
        
        /// <summary>
        /// Set fractional key value by id.
        /// </summary>
        /// <param name="idKey">Set value id.</param>
        /// <param name="value">Set value.</param>
        /// <param name="defaultValue">Whether the value being set is the default value.</param>
        public void SetKeyValue(string idKey, float value, bool defaultValue = false) =>
            _keysSystem.SetKey<float>(idKey, value, defaultValue);
        
        /// <summary>
        /// Set string key value by id.
        /// </summary>
        /// <param name="idKey">Set value id.</param>
        /// <param name="value">Set value.</param>
        /// <param name="defaultValue">Whether the value being set is the default value.</param>
        public void SetKeyValue(string idKey, string value, bool defaultValue = false) =>
            _keysSystem.SetKey<string>(idKey, value, defaultValue);

        /// <summary>
        /// Set condition key value by id.
        /// </summary>
        /// <param name="idKey">Set value id.</param>
        /// <param name="value">Set value.</param>
        /// <param name="defaultValue">Whether the value being set is the default value.</param>
        public void SetKeyValue(string idKey, bool value, bool defaultValue = false) =>
            _keysSystem.SetKey<bool>(idKey, value, defaultValue);

        /// <summary>
        /// Increase the key value by id by the specified value.
        /// </summary>
        /// <param name="idKey">Set valie id.</param>
        /// <param name="value">Specified value.</param>
        public void ChangeValueKeyTo(string idKey, int value) =>
            _keysSystem.ChangeValueKeyToBase<int>(idKey, value);
        
        /// <summary>
        /// Increase the key value by id by the specified value.
        /// </summary>
        /// <param name="idKey">Set valie id.</param>
        /// <param name="value">Specified value.</param>
        public void ChangeValueKeyTo(string idKey, float value) =>
            _keysSystem.ChangeValueKeyToBase<float>(idKey, value);

        /// <summary>
        /// Increase the key value by id by the specified value.
        /// </summary>
        /// <param name="idKey">Set valie id.</param>
        /// <param name="value">Specified value.</param>
        public void ChangeValueKeyTo(string idKey, string value) =>
            _keysSystem.ChangeValueKeyToBase<string>(idKey, value);

        /// <summary>
        /// Delete all saved values.
        /// </summary>
        public void DeleteAllKeys() =>
            _keysSystem.DeleteAllKeys();

        /// <summary>
        /// Save all saved values.
        /// </summary>
        public void Save() =>
            PlayerPrefs.Save();
    }
}