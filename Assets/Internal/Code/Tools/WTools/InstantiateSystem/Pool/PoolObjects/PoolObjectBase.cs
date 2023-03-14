using UnityEngine;

namespace Tools.WTools
{
	public abstract class PoolObjectBase : LockedMonoBehaviour, IPoolObject
	{
		public string ID => _id;
		public bool IsUsed => _isUsed;

		private bool _isUsed;
		private string _id;
		
		public virtual void Using()
		{
			_isUsed = true;
		}

		public virtual void ReturnToPool()
		{
			_isUsed = false;
			gameObject.SetActive(false);
		}

		public void SetID(string id)
		{
			_id = id;
		}
	}
}