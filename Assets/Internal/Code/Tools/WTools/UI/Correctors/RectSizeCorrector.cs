using UnityEngine;

namespace Tools.WTools
{
    public static class RectSizeCorrector
    {

        #region AdaptingMetods

        /// <summary>
        /// Resizing the RectTransform to the specified.
        /// </summary>
        /// <param name="rectTrans"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static RectTransform AdaptingSize(this RectTransform rectTrans, float width = 0f, float height = 0f)
        {
            AdaptingWidth(rectTrans, width);
            AdaptingHeight(rectTrans, height);
            return rectTrans;
        }
        
        /// <summary>
        /// Change the width of the RectTransform to the specified.
        /// </summary>
        /// <param name="rectTrans"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static RectTransform AdaptingWidth(this RectTransform rectTrans, float width)
        {
            var rect = rectTrans.rect;
            var changeWidth = width - rect.width;
            IncreaseWidth(rectTrans, changeWidth);
            return rectTrans;
        }
        
        /// <summary>
        /// Change the height of the RectTransform to the specified.
        /// </summary>
        /// <param name="rectTrans"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static RectTransform AdaptingHeight(this RectTransform rectTrans, float height)
        {
            var rect = rectTrans.rect;
            var changeHeight = height - rect.height;
            IncreaseHeight(rectTrans, changeHeight);
            return rectTrans;
        }

        #endregion

        #region IncreaseMetods

        /// <summary>
        /// Increase the RectTransform size by the specified amount.
        /// </summary>
        /// <param name="rectTrans"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static RectTransform IncreaseSize(this RectTransform rectTrans, float width = 0f, float height = 0f)
        {
            IncreaseWidth(rectTrans, width);
            IncreaseHeight(rectTrans, height);
            return rectTrans;
        }
        
        /// <summary>
        /// Increase the width of the window by the specified amount.
        /// </summary>
        /// <param name="rectTrans"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static RectTransform IncreaseWidth(this RectTransform rectTrans, float width)
        {
            rectTrans.offsetMax += (width / 2) * Vector2.right;
            rectTrans.offsetMin -= (width / 2) * Vector2.right;
            return rectTrans;
        }
        
        /// <summary>
        /// Increase the height of the window by the specified amount.
        /// </summary>
        /// <param name="rectTrans"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static RectTransform IncreaseHeight(this RectTransform rectTrans, float height)
        {
            rectTrans.offsetMax += (height / 2) * Vector2.up;
            rectTrans.offsetMin -= (height / 2) * Vector2.up;
            return rectTrans;
        }

        #endregion
    }
}