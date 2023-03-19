using System;

namespace Tools.WTools
{
    public interface IMonoDamageable : ILockedMonoBehaviour
    {
        public event Action<float> OnDamage;
        public void Damage(float damage);
    }
}