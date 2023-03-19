using ProjectSystems;
using Signals;
using Tools.WTools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Forms
{
    public class LostForm : UIForm
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitToMenuButton;
        
        private SignalBus _signalBus;
        private ProjectStateMachine _projectStateMachine;

        [Inject]
        public void Construct(
            SignalBus signalBus,
            ProjectStateMachine projectStateMachine
        )
        {
            _signalBus = signalBus;
            _projectStateMachine = projectStateMachine;
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(() =>
            {
                Hide<LostForm>(false);
                _signalBus.Fire<PlayGameSignal>();
            });
            _exitToMenuButton.onClick.AddListener(() =>
            {
                Hide<LostForm>(false);
                _projectStateMachine.SetState<MenuProjectState>();
            });
        }
    }
}