using UnityEngine;

namespace Tools.WTools
{
    public class AnimationPoolObject : PoolObjectBase
    {
        public Animator CommonAnimator => _commonAnimator;

        [SerializeField] private Animator _commonAnimator;
    }
}