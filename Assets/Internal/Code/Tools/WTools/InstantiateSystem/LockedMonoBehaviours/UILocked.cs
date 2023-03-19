using UnityEngine;

namespace Tools.WTools
{
    public class UILocked : PoolObjectBase, IUILocked
    {
        public RectTransform CurrentRectTransform => (RectTransform)GetTransform();

        private bool _isUsed;
        
        public void SetPosition(Vector2 position)
        {
            CurrentRectTransform.anchoredPosition = position;
        }
    }
}