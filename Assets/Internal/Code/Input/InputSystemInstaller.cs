using Zenject;

namespace InputSystem
{
	public class InputSystemInstaller : Installer<InputSystemInstaller>
	{
		public override void InstallBindings()
		{
#if UNITY_EDITOR
			Container.BindInterfacesTo<PCJoystick>().AsSingle().NonLazy();
#endif
		}
	}
}