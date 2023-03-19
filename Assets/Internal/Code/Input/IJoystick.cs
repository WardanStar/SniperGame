using UniRx;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public interface IJoystick : ITickable
    {
        /// <summary>
        /// Returns a reactive property whose value changes when aiming starts.
        /// </summary>
        public IReadOnlyReactiveProperty<bool> OnStartAiming { get; }
        
        /// <summary>
        /// Returns a reactive property whose value changes when aiming stops.
        /// </summary>
        public IReadOnlyReactiveProperty<bool> OnEndAiming { get; }
        
        /// <summary>
        /// Returns the direction in which the player's eyes are moving.
        /// </summary>
        public Vector3 MoveDirection { get; }
        
        /// <summary>
        /// Change control activity from controller.
        /// </summary>
        /// <param name="isActive">Enable controller control. true - on false - off</param>
        public void ChangeActive(bool isActive);
    }
}