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
            WeaponInfo weaponInfo,
            UIFormControlSystem uiFormControlSystem
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
                    joystick, weaponInfo,
                    uiFormControlSystem,
                    sceneResourcesStorage,
                    gameSettings)
                },
                
                {typeof(LookAtBulletCameraState), new LookAtBulletCameraState(this,
                    lookAtBulletContainer,
                    sceneResourcesStorage,
                    gameSettings)
                },
                
                {typeof(LookAtResultCameraState), new LookAtResultCameraState(this,
                        gameSettings)
                },
                
                {typeof(ReturnToStartPositionCameraState), new ReturnToStartPositionCameraState(this,
                        signalBus,
                        returnToStartPositionDataContainer,
                        sceneResourcesStorage,
                        gameSettings)
                }
            });
        }
    }
}