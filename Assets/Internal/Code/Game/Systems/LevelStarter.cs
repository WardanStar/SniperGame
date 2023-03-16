using Game.Data;
using InputSystem;
using Signals;
using Tools.DTools;
using Tools.WTools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
	public class LevelStarter : IInitializable
	{
		private readonly IArm _arm;
		private readonly IJoystick _joystick;
		private readonly SignalBus _signalBus;
		private readonly ContextDisposable _contextDisposable;
		private readonly SceneResourcesStorage _sceneResourcesStorage;

		public LevelStarter(
			IArm arm,
			IJoystick joystick,
			SignalBus signalBus,
			SceneResourcesStorage sceneResourcesStorage,
			ContextDisposable contextDisposable
			)
		{
			_arm = arm;
			_joystick = joystick;
			_signalBus = signalBus;
			_sceneResourcesStorage = sceneResourcesStorage;
			_contextDisposable = contextDisposable;
		}

		public void Initialize()
		{
			_signalBus.GetStream<PlayGameSignal>().Subscribe(signal => StartGame()).AddTo(_contextDisposable);
		}
		
		private void StartGame()
		{
			_arm.InitializeRoot();
			
			_arm.ReturnToPoolAllObject();
			
			_signalBus.Fire<PreparationGameSignal>();

			Cursor.visible = false;
			
			_signalBus.Fire<StartGameSignal>();
			
			_joystick.ChangeActive(true);
		}

		
	}
}