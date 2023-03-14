using UnityEngine;

namespace Game.Data
{
	public class SceneResourcesStorage : MonoBehaviour
	{
		public Transform TargetSpawnPoint => _targetSpawnPoint;

		[SerializeField] private Transform _targetSpawnPoint;
	}
}