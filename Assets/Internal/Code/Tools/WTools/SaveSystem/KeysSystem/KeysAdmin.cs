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
		
		public void SetKey(string idKey, int value, bool defaultValue = false) =>
			_keysSystem.SetKey<int>(idKey, value, defaultValue);
		
		public void SetKey(string idKey, float value, bool defaultValue = false) =>
			_keysSystem.SetKey<float>(idKey, value, defaultValue);
		
		public void SetKey(string idKey, string value, bool defaultValue = false) =>
			_keysSystem.SetKey<string>(idKey, value, defaultValue);

		public void SetKey(string idKey, bool value, bool defaultValue = false) =>
			_keysSystem.SetKey<bool>(idKey, value, defaultValue);

		public void ChangeValueKeyTo(string idKey, int value) =>
			_keysSystem.ChangeValueKeyToBase<int>(idKey, value);
		
		public void ChangeValueKeyTo(string idKey, float value) =>
			_keysSystem.ChangeValueKeyToBase<float>(idKey, value);

		public void ChangeValueKeyTo(string idKey, string value) =>
			_keysSystem.ChangeValueKeyToBase<string>(idKey, value);

		public void DeleteAllKeys() =>
			_keysSystem.DeleteAllKeys();

		public void Save() =>
			PlayerPrefs.Save();
	}
}