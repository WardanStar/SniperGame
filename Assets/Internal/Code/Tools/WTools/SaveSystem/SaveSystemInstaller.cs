using Tools.WTools.Saver.PCSaver;
using Zenject;

namespace Tools.WTools
{
    public class SaveSystemInstaller : Installer<SaveSystemInstaller>
    {
        public override void InstallBindings()
        {
#if UNITY_EDITOR
            Container.BindInterfacesTo<PCSaver>().AsSingle().NonLazy();
#elif UNITY_ANDROID || UNITY_IOS
            Container.BindInterfacesTo<PhoneSaver>().AsSingle().NonLazy();
#endif
            Container.BindInterfacesTo<KeysSystem>().AsSingle().NonLazy();
        }
    }
}