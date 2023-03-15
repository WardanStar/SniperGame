using System;

namespace Tools.WTools.Saver
{
	public interface ISaver
	{
		public event Action OnSave;
		public void SaveData();
	}
}