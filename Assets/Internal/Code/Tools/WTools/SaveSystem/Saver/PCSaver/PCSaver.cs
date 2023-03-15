using System;

namespace Tools.WTools.Saver.PCSaver
{
	public class PCSaver : ISaver, IDisposable
	{
		public event Action OnSave;
		
		private readonly KeysAdmin _keysAdmin;

		public PCSaver(
			IKeysSystem keysSystem
			)
		{
			_keysAdmin = keysSystem.KeysAdmin;
		}
		
		public void SaveData()
		{
			OnSave?.Invoke();
			_keysAdmin.Save();
		}
		
		public void Dispose()
		{
			SaveData();
		}
	}
}