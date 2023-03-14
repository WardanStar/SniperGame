using System;
using Tools.WTools;
using UnityEngine;

namespace Game.Entities
{
	public class TargetCubeElementMono : PoolObject, IMonoDamageable
	{
		public event Action<float> OnDamage;
		public event Action<Material> OnChangeMaterial;
		public event Action<int> OnChangeQuantityScoreByDestroy;
		
		public void SetMaterials(Material material) =>
			OnChangeMaterial?.Invoke(material);

		public void SetQuantityScore(int score) =>
			OnChangeQuantityScoreByDestroy?.Invoke(score);

		public void Damage(float damage) =>
			OnDamage?.Invoke(damage);
	}
}