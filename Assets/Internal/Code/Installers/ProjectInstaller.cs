using Game;
using Game.Misc;
using Settings;
using Tools.WTools;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] private StorageOfResourcesCollection _storageOfResourcesCollection;
		[SerializeField] private LevelSettings _levelSettings;
		
		public override void InstallBindings()
		{
			NonLazyInstall();
			CommonInstall();
		}

		private void NonLazyInstall()
		{
			Container.BindInterfacesTo<Arm>().AsSingle().NonLazy();
			Container.Bind<TestTarget>().AsSingle().NonLazy();
		}

		private void CommonInstall()
		{
			Container.Bind<TargetInfoGenerator>().AsSingle();
			Container.BindInterfacesTo<ResourcesLoader>().AsSingle();
			Container.Bind<ObjectInjector>().AsSingle();
			Container.Bind<PoolStorage>().AsSingle();
			Container.BindInstance(_storageOfResourcesCollection).AsSingle();
			Container.BindInstance(_levelSettings).AsSingle();
		}
	}
}