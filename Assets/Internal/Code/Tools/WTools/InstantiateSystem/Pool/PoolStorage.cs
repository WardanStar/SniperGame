using System.Collections.Generic;

namespace Tools.WTools
{
	public class PoolStorage
	{
		public IReadOnlyDictionary<string, Pool> Pools => _pools;

		private readonly Dictionary<string, Pool> _pools = new();
		
		public Pool GetPool(string pathToObject)
		{
			if (_pools.TryGetValue(pathToObject, out Pool pool))
				return pool;

			var newPool = new Pool();
			
			_pools.Add(pathToObject, newPool);

			return newPool;
		}

		public void ClearPools() => _pools.Clear();
	}
}