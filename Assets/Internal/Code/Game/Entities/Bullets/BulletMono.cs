using System;
using Tools.WTools;
using UnityEngine;

namespace Game.Entities.Bullets
{
	public class BulletMono : PoolObject
	{
		private void Update()
		{
			transform.position = transform.position + (10f * Time.deltaTime * transform.forward);
		}
	}
}