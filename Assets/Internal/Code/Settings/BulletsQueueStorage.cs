using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Settings/BilletsQueueStorage")]
	public class BulletsQueueStorage : ScriptableObject
	{
		public Vector3[] DirectionsBullet => _directionsBullet;

		[SerializeField] private Vector3[] _directionsBullet;
	}
}