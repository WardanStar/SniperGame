using UnityEngine;
using Zenject;

namespace Game.Misc
{
	public class ObjectInjector
	{
		private readonly DiContainer _container;

		public ObjectInjector(DiContainer container)
		{
			_container = container;
		}

		public void InjectGO(GameObject gameObject)
		{
			_container.InjectGameObject(gameObject);
		}

		public void Inject(object injectable)
		{
			_container.Inject(injectable);
		}
	}
}