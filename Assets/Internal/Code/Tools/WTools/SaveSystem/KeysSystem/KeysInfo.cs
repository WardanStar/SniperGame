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
		
		public T GetKey<T>(string idKey) =>
			(T)_keysSystem.GetKey<T>(idKey).Value;

		public bool HasKey(string idKey) =>
			PlayerPrefs.HasKey(idKey);
	}
}