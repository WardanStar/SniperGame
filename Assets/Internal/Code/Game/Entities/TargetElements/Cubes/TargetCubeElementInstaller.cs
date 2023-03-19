using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
    [RequireComponent(typeof(GameObjectContext))]
    [RequireComponent(typeof(TargetCubeElementMono))]
    [RequireComponent(typeof(Rigidbody))]
    public class TargetCubeElementInstaller : MonoInstaller
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private MeshRenderer _meshRenderer;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TargetCubeElementModel>().AsSingle().NonLazy();
            
            Container.BindInstance(_meshRenderer).AsSingle();
            Container.BindInstance(_scoreText).AsSingle();
            Container.BindInstance(GetComponent<TargetCubeElementMono>()).AsSingle();
            Container.BindInstance(GetComponent<Rigidbody>()).AsSingle();
        }
    }
}