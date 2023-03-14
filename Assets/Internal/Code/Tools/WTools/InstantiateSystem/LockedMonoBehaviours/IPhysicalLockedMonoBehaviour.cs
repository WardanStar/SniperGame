using UnityEngine;

namespace Tools.WTools
{
	public interface IPhysicalLockedMonoBehaviour : ILockedMonoBehaviour
	{
		public Rigidbody CommonRigidbody { get; }
		public Collider CommonCollider { get; }
	}
}