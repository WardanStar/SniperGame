using Game;
using Game.CameraStateMachine;
using Game.Data;
using Game.Misc;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class LevelInstaller : MonoInstaller
	{
		[SerializeField] private SceneResourcesStorage _sceneResourcesStorage;

		public override void InstallBindings()
		{
			NonLazyInstall();
			CommonInstall();
		}

		private void NonLazyInstall()
		{
			Container.BindInterfacesTo<TargetGenerator>().AsSingle().NonLazy();
			Container.BindInterfacesTo<CameraControlSystems>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<CameraStateMachine>().AsSingle().NonLazy();
			Container.Bind<LevelStarter>().AsSingle().NonLazy();
		}
		
		private void CommonInstall()
		{
			Container.Bind<ContextDisposable>().AsSingle();
			Container.BindInstance(_sceneResourcesStorage).AsSingle();
		}
	}
}