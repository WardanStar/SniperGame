using System.Collections.Generic;
using UnityEngine;

namespace Tools.WTools
{
    public class Pool
    {
        public List<PoolObjectBase> PoolObjects => _poolObjects;

        private readonly List<PoolObjectBase> _poolObjects = new();

        public PoolObjectBase GetPoolObject()
        {
            foreach (var poolObject in _poolObjects)
            {
                if (!poolObject.IsUsed)
                    return poolObject;
            }

            return null;
        }

        public void AddPoolObject(PoolObjectBase poolObject)
        {
            
            
            _poolObjects.Add(poolObject);
        }

        public void ReturnToPoolAllObject()
        {
            foreach (var poolObject in _poolObjects)
            {
                poolObject.ReturnToPool();
            }
        }
    }
}