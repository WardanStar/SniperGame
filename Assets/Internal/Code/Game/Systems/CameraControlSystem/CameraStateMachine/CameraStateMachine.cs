using System;
using System.Collections.Generic;
using Game.Data;
using InputSystem;
using ProjectSystems;
using Settings;
using Tools.DTools;
using Tools.WTools;
using Zenject;

namespace Game.CameraStateMachine
{
	public interface ICameraState{}
	
	public class CameraStateMachine : StateMachine<ICameraState>
	{
		public CameraStateMachine(
			IJoystick joystick,
			SignalBus signalBus,
			SceneResourcesStorage sceneResourcesStorage,
			GameSettings gameSettings,
			WeaponControlSystem weaponControlSystem,
			UIFormControlSystem uiFormControlSystem,
			ContextDisposable contextDisposable
		)
		{
			LookAtBulletDataContainer lookAtBulletContainer = new LookAtBulletDataContainer();
			ReturnToStartPositionDataContainer returnToStartPositionDataContainer = new ReturnToStartPositionDataContainer();
			
			SetDataContainers(new Dictionary<Type, object>()
			{
				{typeof(LookAtBulletDataContainer), lookAtBulletContainer},
				{typeof(ReturnToStartPositionDataContainer), returnToStartPositionDataContainer}
			});
			
			SetStates(new Dictionary<Type, State<ICameraState>>()
			{
				{typeof(IdleCameraState), new IdleCameraState(this,
						uiFormControlSystem)
				},
				
				{typeof(AimingCameraState), new AimingCameraState(this,
					joystick, weaponControlSystem,
					uiFormControlSystem,
					sceneResourcesStorage.Camera.transform, 
					gameSettings.SpeedCameraRotation)
				},
				
				{typeof(LookAtBulletCameraState), new LookAtBulletCameraState(this,
					lookAtBulletContainer,
					sceneResourcesStorage.Camera.transform,
					gameSettings.IndentCameraWithBullet,
					gameSettings.CameraMovementTimeToTheTarget,
					gameSettings.MAXCameraSpeed,
					gameSettings.DistanceToTheCameraTransitionToTheResultMode)
				},
				
				{typeof(LookAtResultCameraState), new LookAtResultCameraState(this,
						gameSettings.TimeToCameraLookResult)
				},
				
				{typeof(ReturnToStartPositionCameraState), new ReturnToStartPositionCameraState(this,
						signalBus,
						returnToStartPositionDataContainer,
						sceneResourcesStorage.Camera.transform,
						gameSettings.SpeedMoveReturnCameraOnStartPosition,
						gameSettings.SpeedRotateCameraOnStartRotation)
				}
			});
		}
	}
}