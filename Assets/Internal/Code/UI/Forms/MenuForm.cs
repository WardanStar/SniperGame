using ProjectSystems;
using TMPro;
using Tools.WTools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Forms
{
	public class MenuForm : UIForm
	{
		[SerializeField] private Button _playGame;
		[SerializeField] private Button _changeWeaponButton;
		[SerializeField] private TMP_Text _levelNumberText;
		private LevelsDataControlSystem _levelsDataControlSystem;

		[Inject]
		public void Construct(
			ProjectStateMachine projectStateMachine,
			LevelsDataControlSystem levelsDataControlSystem
			)
		{
			_levelsDataControlSystem = levelsDataControlSystem;
			_playGame.onClick.AddListener(projectStateMachine.SetState<GameProjectState>);
		}

		public override void ActionBeforeShow()
		{
			_levelNumberText.text = $"Level {_levelsDataControlSystem.GetIndexCurrentLevel() + 1}";
		}
	}
}