using UniRx;
using UnityEngine;

namespace InputSystem
{
	public class PCJoystick : IJoystick
	{
		public IReadOnlyReactiveProperty<bool> OnStartAiming => _onStartAiming;
		public IReadOnlyReactiveProperty<bool> OnEndAiming => _onEndAiming;

		private readonly ReactiveProperty<bool> _onStartAiming = new();
		private readonly ReactiveProperty<bool> _onEndAiming = new();
		
		public void Tick()
		{
			_onStartAiming.Value = Input.GetMouseButton(1);
			_onEndAiming.Value = Input.GetMouseButtonUp(1);
		}
	}
}