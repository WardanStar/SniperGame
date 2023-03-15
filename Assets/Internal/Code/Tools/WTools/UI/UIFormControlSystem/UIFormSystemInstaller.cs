using Zenject;

namespace Tools.WTools
{
	public class UIFormSystemInstaller : Installer<UIFormSystemInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<UIFormAnimator>().AsSingle();
			Container.Bind<UIStorage>().AsSingle();
			Container.BindInterfacesAndSelfTo<UIFormControlSystem>().AsSingle();
		}
	}
}