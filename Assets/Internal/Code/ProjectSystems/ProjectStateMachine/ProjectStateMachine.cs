using System;
using System.Collections.Generic;
using InputSystem;
using Tools.WTools;
using Zenject;

namespace ProjectSystems
{
    public interface IProjectState{}
    
    public class ProjectStateMachine : StateMachine<IProjectState>
    {
        public ProjectStateMachine(
            IJoystick joystick,
            SignalBus signalBus,
            UIFormControlSystem uiFormControlSystem
            )
        {
            SetStates(new Dictionary<Type, State<IProjectState>>()
            {
                {typeof(MenuProjectState), new MenuProjectState(this,
                    joystick, signalBus,
                    uiFormControlSystem)
                },
                
                {typeof(GameProjectState), new GameProjectState(this,
                    signalBus,
                    uiFormControlSystem)
                }
            });
            
        }
    }
}