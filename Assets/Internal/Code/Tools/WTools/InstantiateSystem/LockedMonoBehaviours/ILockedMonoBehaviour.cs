using UnityEngine;

namespace Tools.WTools
{
    public interface ILockedMonoBehaviour
    {
        public GameObject GetGameObject { get; }
        public T GetComponentFromGameObject<T>() where T : Object;
        public Transform GetTransform();
        public void SetPosition(Vector3 position);
        public Vector3 GetPosition();
        public void SetRotation(Quaternion rotation);
        public Quaternion GetRotation();
        public Vector3 GetForwardRotation();
        public void SetScale(Vector3 scale);
        public Vector3 GetScale();
        public void SetParent(Transform parent);
        public void ChangeActive(bool isActive);
    }
}