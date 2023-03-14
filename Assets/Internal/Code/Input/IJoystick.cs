using UniRx;
using Zenject;

namespace InputSystem
{
	public interface IJoystick : ITickable
	{
		public IReadOnlyReactiveProperty<bool> OnStartAiming { get; }
		public IReadOnlyReactiveProperty<bool> OnEndAiming { get; }
	}
}