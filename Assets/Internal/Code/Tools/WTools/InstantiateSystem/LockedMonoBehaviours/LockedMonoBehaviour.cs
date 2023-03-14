using UnityEngine;

namespace Tools.WTools
{
	public class LockedMonoBehaviour : MonoBehaviour, ILockedMonoBehaviour
	{
		public GameObject GetGameObject => gameObject;
		public T GetComponentFromGameObject<T>() where T : Object =>
			gameObject.GetComponent<T>();

		public Transform GetTransform() =>
			transform;

		public void SetPosition(Vector3 position) =>
			transform.position = position;
		public Vector3 GetPosition() =>
			transform.position;

		public void SetRotation(Quaternion rotation) =>
			transform.rotation = rotation;
		public Quaternion GetRotation() =>
			transform.rotation;

		public Vector3 GetForwardRotation() =>
			transform.forward;

		public void SetScale(Vector3 scale) =>
			transform.localScale = scale;
		public Vector3 GetScale() =>
			transform.localScale;

		public void SetParent(Transform parent) =>
			transform.parent = parent;

		public void ChangeActive(bool isActive) =>
			gameObject.SetActive(isActive);
	}
}