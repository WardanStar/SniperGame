using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
	[RequireComponent(typeof(GameObjectContext))]
	[RequireComponent(typeof(TargetCubeElementMono))]
	public class TargetCubeElementInstaller : MonoInstaller
	{
		[SerializeField] private MeshRenderer _meshRenderer;
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private TargetCubeElementMono _targetCubeElementMono;

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<TargetCubeElementModel>().AsSingle().NonLazy();
			
			Container.BindInstance(_meshRenderer).AsSingle();
			Container.BindInstance(_scoreText).AsSingle();
			Container.BindInstance(_targetCubeElementMono).AsSingle();
		}
	}
}