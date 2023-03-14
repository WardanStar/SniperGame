using UnityEngine;

namespace Game.Data
{
	public class SceneResourcesStorage : MonoBehaviour
	{
		public Transform TargetSpawnPoint => _targetSpawnPoint;
		public Camera Camera => _3dCamera;

		[SerializeField] private Transform _targetSpawnPoint;
		[SerializeField] private Camera _3dCamera;
	}
}