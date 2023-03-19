using UnityEngine;

namespace Tools.WTools
{
    public class PoolObjectWithCollider : PoolObjectBase
    {
        public Collider CommonCollider => _commonCollider;
        [SerializeField] private Collider _commonCollider;
    }
}