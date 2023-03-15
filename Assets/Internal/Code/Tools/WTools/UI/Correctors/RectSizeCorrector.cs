using UnityEngine;

namespace Tools.WTools
{
	public static class RectSizeCorrector
	{

		#region AdaptingMetods

		public static RectTransform AdaptingSize(this RectTransform rectTrans, float width = 0f, float height = 0f)
        {
        	AdaptingWidth(rectTrans, width);
        	AdaptingHeight(rectTrans, height);
            return rectTrans;
        }
		
		public static RectTransform AdaptingWidth(this RectTransform rectTrans, float width)
        {
        	var rect = rectTrans.rect;
        	var changeWidth = width - rect.width;
        	IncreaseWidth(rectTrans, changeWidth);
            return rectTrans;
        }
        public static RectTransform AdaptingHeight(this RectTransform rectTrans, float height)
        {
        	var rect = rectTrans.rect;
        	var changeHeight = height - rect.height;
        	IncreaseHeight(rectTrans, changeHeight);
            return rectTrans;
        }

		#endregion

		#region IncreaseMetods

		public static RectTransform IncreaseSize(this RectTransform rectTrans, float width = 0f, float height = 0f)
		{
			IncreaseWidth(rectTrans, width);
			IncreaseHeight(rectTrans, height);
			return rectTrans;
		}
		
		public static RectTransform IncreaseWidth(this RectTransform rectTrans, float width)
		{
			rectTrans.offsetMax += (width / 2) * Vector2.right;
			rectTrans.offsetMin -= (width / 2) * Vector2.right;
			return rectTrans;
		}
		public static RectTransform IncreaseHeight(this RectTransform rectTrans, float height)
		{
			rectTrans.offsetMax += (height / 2) * Vector2.up;
			rectTrans.offsetMin -= (height / 2) * Vector2.up;
			return rectTrans;
		}

		#endregion
	}
}