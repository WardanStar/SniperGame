using Signals;
using Zenject;

namespace Installers
{
	public class SignalInstaller : Installer<SignalInstaller>
	{
		public override void InstallBindings()
		{
			Container.DeclareSignal<PreparationGameSignal>();
			Container.DeclareSignal<StartGameSignal>();
			Container.DeclareSignal<CameraSetPositionSignal>();
		}
	}
}