using System;
using Game.Systems;
using TMPro;
using Tools.WTools;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Forms
{
	public class GameForm : UIForm
	{
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private RectTransform _ammunitionRoot;
		[SerializeField] private GridLayoutCorrector _gridLayoutCorrector;

		[Inject]
		public void Construct(
			ScoreCounter scoreCounter
		)
		{
			scoreCounter.Score.Subscribe(value => _scoreText.text = $"Score : {Math.Round(value, 1)}").AddTo(this);
		}
	}
}