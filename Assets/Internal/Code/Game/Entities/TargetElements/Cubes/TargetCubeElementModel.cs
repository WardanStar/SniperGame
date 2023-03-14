using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Entities
{
	public class TargetCubeElementModel : IInitializable
	{
		private readonly SignalBus _signalBus;
		private readonly MeshRenderer _meshRenderer;
		private readonly TMP_Text _scoreText;

		private int _quantityScoreByDestroy;

		public TargetCubeElementModel(
			SignalBus signalBus,
			TargetCubeElementMono targetCubeElementMono,
			MeshRenderer meshRenderer,
			TMP_Text scoreText
			)
		{
			_signalBus = signalBus;
			_meshRenderer = meshRenderer;
			_scoreText = scoreText;

			targetCubeElementMono.OnDamage += OnDamage;
			targetCubeElementMono.OnChangeMaterial += ChangeMaterial;
			targetCubeElementMono.OnChangeQuantityScoreByDestroy += ChangeQuantityScoreBuDestroy;
		}

		public void Initialize()
		{
			
		}
		
		private void ChangeQuantityScoreBuDestroy(int score)
		{
			_quantityScoreByDestroy = score;
			
			_scoreText.text = score.ToString();
		}

		private void ChangeMaterial(Material material) =>
			_meshRenderer.material = material;

		private void OnDamage(float damage)
		{
			
		}

	}
}