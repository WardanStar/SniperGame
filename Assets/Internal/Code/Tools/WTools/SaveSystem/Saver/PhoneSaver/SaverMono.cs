using System;
using UnityEngine;

namespace Tools.WTools
{
	public class SaverMono : MonoBehaviour
	{
		public Action OnSave;
		
		public void OnApplicationFocus(bool hasFocus)
		{
			if (!hasFocus)
			{
				OnSave?.Invoke();
			}
		}
	}
}