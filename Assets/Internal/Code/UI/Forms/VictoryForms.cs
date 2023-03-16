using ProjectSystems;
using Signals;
using Tools.WTools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Forms
{
	public class VictoryForms : UIForm
	{
		[SerializeField] private Button _continueButton;
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
			_continueButton.onClick.AddListener(() => _signalBus.Fire<PlayGameSignal>());
			_exitToMenuButton.onClick.AddListener(() =>
			{
				Hide<VictoryForms>(false);
				_projectStateMachine.SetState<MenuProjectState>();
			});
		}
	}
}