
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.WTools
{
    public class KeysSystem : IKeysSystem
    {
        public class Key
        {
            public object Value => _value;

            private object _value;

            public Key(object value) =>
                _value = value;

            public void ChangeValue(object value) =>
                _value = value;
        }

        public KeysAdmin KeysAdmin => _keysAdmin;
        public KeysInfo KeysInfo => _keysInfo;

        private readonly KeysInfo _keysInfo;
        private readonly KeysAdmin _keysAdmin;
        
        private readonly Dictionary<string, Key> _keysCollection = new();

        private bool _disableSaveKeys;
        
        public KeysSystem()
        {
            _keysInfo = new KeysInfo(this);
            _keysAdmin = new KeysAdmin(this);
        }
        
        public void SetKey<T>(string id, object value, bool isDefaultValue = false)
        {
            if (_disableSaveKeys)
                return;
            
            Key oldKey = GetInternalKey(id);

            if (!isDefaultValue)
                SetKeyToPlayerPrefs<T>(id, value);

            if (!ReferenceEquals(oldKey, null))
            {
                oldKey.ChangeValue(value);
                return;
            }
            
            Key newKey = CreateKey<T>(id, value);

            _keysCollection.Add(id, newKey);
        }

        public void ChangeValueKeyToBase<T>(string id, object obj)
        {
            Key key = GetKey<T>(id);

            object sum = null;

            if (typeof(T) == typeof(int))
                sum = (int)key.Value + (int)obj;
            
            if (typeof(T) == typeof(float))
                sum = (float)key.Value + (float)obj;

            if (typeof(T) == typeof(string))
                sum = (string)key.Value + (string)obj;

            if (sum == null)
                throw new ArgumentException("Type not does not match requirements");
            
            key.ChangeValue(sum);
            SetKey<T>(id, obj);
        }
        
        public Key GetKey<T>(string id)
        {
            Key oldKey = GetInternalKey(id);

            if (!ReferenceEquals(oldKey, null))
                return oldKey;
            
            Key newKey = CreateKey<T>(id, null);

            _keysCollection.Add(id, newKey);

            return newKey;
        }

        public void DeleteAllKeys()
        {
            _disableSaveKeys = true;
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }

        private Key GetInternalKey(string id) =>
            _keysCollection.TryGetValue(id, out Key save) ? save : null;
        
        private Key CreateKey<T>(string id, object defaultValue)
        {
            if (typeof(T) == typeof(bool))
                return new Key(PlayerPrefs.GetInt(id, defaultValue == null ? 0 : (bool)defaultValue ? 1 : 0) != 0);
            
            if (typeof(T) == typeof(int))
                return new Key(PlayerPrefs.GetInt(id, defaultValue == null ? 0 : (int)defaultValue));

            if (typeof(T) == typeof(float))
                return new Key(PlayerPrefs.GetFloat(id, defaultValue == null ? 0f : (float)defaultValue));
            
            if (typeof(T) == typeof(string))
                return new Key(PlayerPrefs.GetString(id, defaultValue == null ? "" : (string)defaultValue));

            throw new ArgumentException("Type not does not match requirements");
        }

        private void SetKeyToPlayerPrefs<T>(string id, object value)
        {
#if DONT_SAVE_KEYS
            return;
#endif
            
#if KEYS_DEBUGING
            Debug.LogError($"ID {id}, Value{(bool)value}");
#endif

            if (typeof(T) == typeof(bool))
            {
#if KEYS_DEBUGING
            Debug.LogError($"ID {id}, Value{(bool)value}");
#endif
                
                PlayerPrefs.SetInt(id, (bool)value ? 1 : 0);
            }

            if (typeof(T) == typeof(int))
            {
#if KEYS_DEBUGING
            Debug.LogError($"ID {id}, Value{(int)value}");
#endif
                
                PlayerPrefs.SetInt(id, (int)value);
            }

            if (typeof(T) == typeof(float))
            {
#if KEYS_DEBUGING
            Debug.LogError($"ID {id}, Value{(int)value}");
#endif
                
                PlayerPrefs.SetFloat(id, (float)value);
            }
                

            if (typeof(T) == typeof(string))
            {
#if KEYS_DEBUGING
            Debug.LogError($"ID {id}, Value{(string)value}");
#endif
                
                PlayerPrefs.SetString(id, (string)value);
            }
        }
    }
}