using Game;
using Game.Misc;
using InputSystem;
using ProjectSystems;
using Settings;
using Tools.WTools;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] private StorageOfResourcesCollection _storageOfResourcesCollection;
		[SerializeField] private LevelStorage _levelStorage;
		
		public override void InstallBindings()
		{
			NonLazyInstall();
			CommonInstall();
			SignalBusInstaller.Install(Container);
			SignalInstaller.Install(Container);
			InputSystemInstaller.Install(Container);
		}

		private void NonLazyInstall()
		{
			Container.BindInterfacesTo<Arm>().AsSingle().NonLazy();
			Container.Bind<LevelsDataControlSystem>().AsSingle().NonLazy();
		}

		private void CommonInstall()
		{
			Container.Bind<TargetInfoGenerator>().AsSingle();
			Container.BindInterfacesTo<ResourcesLoader>().AsSingle();
			Container.Bind<ObjectInjector>().AsSingle();
			Container.Bind<PoolStorage>().AsSingle();
			Container.BindInstance(_storageOfResourcesCollection).AsSingle();
			Container.BindInstance(_levelStorage).AsSingle();
		}
	}
}