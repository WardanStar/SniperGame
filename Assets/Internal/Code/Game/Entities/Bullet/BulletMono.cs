using System;
using Tools.WTools;

namespace Game.Entities
{
    public class BulletMono : PoolObject
    {
        public event Action OnBulletEnable;
        
        private void OnEnable() =>
            OnBulletEnable?.Invoke();
    }
}