using UnityEngine;

namespace Tools.WTools
{
    public class PhysicalPoolObject : PoolObjectWithCollider
    {
        public Rigidbody CommonRigidbody => _commonRigidbody;
        [SerializeField] private Rigidbody _commonRigidbody;
    }
}