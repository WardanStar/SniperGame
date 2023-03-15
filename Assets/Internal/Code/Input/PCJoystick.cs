using UniRx;
using UnityEngine;

namespace InputSystem
{
	public class PCJoystick : IJoystick
	{
		public IReadOnlyReactiveProperty<bool> OnStartAiming => _onStartAiming;
		public IReadOnlyReactiveProperty<bool> OnEndAiming => _onEndAiming;
		public Vector3 MoveDirection => _moveDirection;

		private readonly ReactiveProperty<bool> _onStartAiming = new();
		private readonly ReactiveProperty<bool> _onEndAiming = new();
		private Vector3 _moveDirection;
		private Vector3 _mousePosition;
		private bool _isActive;
		
		public void ChangeActive(bool isActive) =>
			_isActive = isActive;
		
		public void Tick()
		{
			if (!_isActive)
				return;
			
			_onStartAiming.Value = Input.GetMouseButtonDown(1);
			_onEndAiming.Value = Input.GetMouseButtonUp(1);
			_moveDirection = new Vector3(_mousePosition.x - Input.mousePosition.x, _mousePosition.y - Input.mousePosition.y, 0f);
			_mousePosition = Input.mousePosition;
		}
	}
}