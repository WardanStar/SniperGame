using System;

namespace Tools.WTools
{
	public interface IMonoDamageable
	{
		public event Action<float> OnDamage;
		public void Damage(float damage);
	}
}