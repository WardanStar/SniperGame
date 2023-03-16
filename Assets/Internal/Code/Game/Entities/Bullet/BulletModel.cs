using Settings;
using Tools.WTools;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
	public class BulletModel : ITickable
	{
		private readonly BulletMono _bulletMono;
		private readonly float _bulletSpeed;
		private RaycastHit[] _raycastHits = new RaycastHit[5];
		private bool _bulletMonoIsActive;

		public BulletModel(
			BulletMono bulletMono,
			GameSettings gameSettings
			)
		{
			_bulletMono = bulletMono;
			_bulletSpeed = gameSettings.BulletSpeed;
			_bulletMono.OnBulletEnable += () => _bulletMonoIsActive = true;
		}
		
		
		public void Tick()
		{
			if (!_bulletMonoIsActive)
				return;
			
			CheckCollisionBullet();
			MoveBullet();
		}

		private void MoveBullet()
		{
			Transform bulletTransform = _bulletMono.GetTransform();
			
			bulletTransform.position = bulletTransform.position + (_bulletSpeed * Time.deltaTime * bulletTransform.forward);
		}

		private void CheckCollisionBullet()
		{
			Transform bulletTransform = _bulletMono.GetTransform();

			int quantityCollision = Physics.RaycastNonAlloc(bulletTransform.position, bulletTransform.forward, _raycastHits,
				_bulletSpeed * Time.deltaTime);
			
			for (int i = 0; i < quantityCollision; i++)
			{
				RaycastHit raycastHit = _raycastHits[i];
				IMonoDamageable damageable = raycastHit.collider.GetComponent<IMonoDamageable>();

				if (damageable == null) continue;
				_bulletMonoIsActive = false;
				
				damageable.Damage(1);
				bulletTransform.SetParent(damageable.GetTransform());
			}
		}
	}
}