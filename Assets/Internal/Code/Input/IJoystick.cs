using UniRx;
using UnityEngine;
using Zenject;

namespace InputSystem
{
	public interface IJoystick : ITickable
	{
		public IReadOnlyReactiveProperty<bool> OnStartAiming { get; }
		public IReadOnlyReactiveProperty<bool> OnEndAiming { get; }
		public Vector3 MoveDirection { get; }
	}
}