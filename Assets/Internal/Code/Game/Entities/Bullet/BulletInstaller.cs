using UnityEngine;
using Zenject;

namespace Game.Entities
{
    [RequireComponent(typeof(GameObjectContext))]
    [RequireComponent(typeof(BulletMono))]
    public class BulletInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInstance(GetComponent<BulletMono>()).AsSingle();
            Container.BindInterfacesTo<BulletModel>().AsSingle().NonLazy();
        }
    }
}