using System;
using System.Collections.Generic;
using Game.Data;
using InputSystem;
using ProjectSystems;
using Settings;
using Tools.WTools;

namespace Game.CameraStateMachine
{
	public interface ICameraState{}
	
	public class CameraStateMachine : StateMachine<ICameraState>
	{
		public CameraStateMachine(
			IJoystick joystick,
			SceneResourcesStorage sceneResourcesStorage,
			GameSettings gameSettings,
			WeaponControlSystem weaponControlSystem
			)
		{
			SetStates(new Dictionary<Type, State<ICameraState>>()
			{
				{typeof(IdleCameraState), new IdleCameraState(this)},
				
				{typeof(AimingCameraState), new AimingCameraState(this,
					joystick, sceneResourcesStorage.Camera,
					gameSettings.SpeedCameraRotation * weaponControlSystem.GetSpeedAiming())},
				
				{typeof(LookAtBulletCameraState), new LookAtBulletCameraState(this)},
				
				{typeof(LookAtResultCameraState), new LookAtResultCameraState(this)},
			});
		}
	}
}