using Game;
using InputSystem;
using ProjectSystems;
using Save;
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
		[SerializeField] private WeaponStorage _weaponStorage;
		[SerializeField] private GameSettings _gameSettings;
		
		public override void InstallBindings()
		{
			NonLazyInstall();
			CommonInstall();
			SignalBusInstaller.Install(Container);
			SignalInstaller.Install(Container);
			InputSystemInstaller.Install(Container);
			UIFormSystemInstaller.Install(Container);
			SaveSystemInstaller.Install(Container);
		}

		private void NonLazyInstall()
		{
			Container.BindInterfacesTo<Arm>().AsSingle().NonLazy();
			Container.BindInterfacesTo<SaveDataControlSystem>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<LevelsDataControlSystem>().AsSingle().NonLazy();
			Container.Bind<QueueLoadingGame>().AsSingle().NonLazy();
			Container.Bind<ProjectStateMachine>().AsSingle().NonLazy();
		}

		private void CommonInstall()
		{
			Container.Bind<TargetInfoGenerator>().AsSingle();
			Container.BindInterfacesTo<ResourcesLoader>().AsSingle();
			Container.Bind<PoolStorage>().AsSingle();
			Container.BindInstance(_storageOfResourcesCollection).AsSingle();
			Container.BindInstance(_levelStorage).AsSingle();
			Container.BindInstance(_weaponStorage).AsSingle();
			Container.BindInstance(_gameSettings).AsSingle();
		}
	}
}