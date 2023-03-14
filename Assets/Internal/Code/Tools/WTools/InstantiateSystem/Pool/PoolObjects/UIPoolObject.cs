using UnityEngine;

namespace Tools.WTools
{
	public class UIPoolObject : PoolObjectBase, IUILocked
	{
		public RectTransform CurrentRectTransform => (RectTransform)GetTransform();

		
		public void SetPosition(Vector2 position)
		{
			CurrentRectTransform.anchoredPosition = position;
		}
	}
}