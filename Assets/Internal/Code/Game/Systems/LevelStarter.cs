using Game.Misc;
using InputSystem;
using Signals;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
	public class LevelStarter
	{
		public LevelStarter(
			IJoystick joystick,
			TargetGenerator targetGenerator,
			SignalBus signalBus,
			ContextDisposable contextDisposable
			)
		{
			joystick.OnStartAiming.Where(isStart => isStart).Subscribe(_ =>
			{
				signalBus.Fire<PreparationGameSignal>();
			}).AddTo(contextDisposable);
		}
	}
}