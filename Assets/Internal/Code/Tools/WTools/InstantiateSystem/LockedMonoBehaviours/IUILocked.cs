using UnityEngine;

namespace Tools.WTools
{
    public interface IUILocked : ILockedMonoBehaviour
    {
        public RectTransform CurrentRectTransform { get; } 
        public void SetPosition(Vector2 position);
    }
}