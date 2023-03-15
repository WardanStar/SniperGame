using System;
using Tools.WTools.Saver;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.WTools
{
	public class PhoneSaver : ISaver
	{
		public event Action OnSave;
		
		private readonly KeysAdmin _keysAdmin;

		protected PhoneSaver(
			IKeysSystem keysSystem
			)
		{
			_keysAdmin = keysSystem.KeysAdmin;
			var newGO = new GameObject("Saver");
			var saverMono = newGO.AddComponent<SaverMono>();
			saverMono.OnSave += SaveData;
			Object.DontDestroyOnLoad(newGO);
		}

		public void SaveData()
		{
			OnSave?.Invoke();
			_keysAdmin.Save();
		}
	}
}