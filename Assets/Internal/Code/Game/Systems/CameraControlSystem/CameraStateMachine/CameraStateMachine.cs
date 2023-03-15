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
			WeaponControlSystem weaponControlSystem,
			UIFormControlSystem uiFormControlSystem
			)
		{
			LookAtBulletDataContainer lookAtBulletContainer = new LookAtBulletDataContainer();
			
			SetDataContainers(new Dictionary<Type, object>()
			{
				{typeof(LookAtBulletDataContainer), lookAtBulletContainer}
			});
			
			SetStates(new Dictionary<Type, State<ICameraState>>()
			{
				{typeof(IdleCameraState), new IdleCameraState(this)},
				
				{typeof(AimingCameraState), new AimingCameraState(this,
					joystick, weaponControlSystem,
					uiFormControlSystem,
					sceneResourcesStorage.Camera.transform, 
					gameSettings.SpeedCameraRotation)},
				
				{typeof(LookAtBulletCameraState), new LookAtBulletCameraState(this,
					lookAtBulletContainer,
					sceneResourcesStorage.Camera.transform,
					gameSettings.IndentCameraWithBullet,
					gameSettings.CameraMovementTimeToTheTarget,
					gameSettings.MAXCameraSpeed)},
				
				{typeof(LookAtResultCameraState), new LookAtResultCameraState(this)},
			});
			
			SetState<IdleCameraState>();
		}
	}
}