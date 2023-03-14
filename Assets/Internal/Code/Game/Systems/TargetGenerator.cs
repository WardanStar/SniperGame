using Game.Data;
using ProjectSystems;
using Settings;
using Signals;
using Tools.WTools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
	public class TargetGenerator : IInitializable
	{
		private readonly IArm _arm;
		private readonly SceneResourcesStorage _sceneResourcesStorage;
		private readonly TargetInfoGenerator _targetInfoGenerator;
		private readonly LevelsDataControlSystem _levelsDataControlSystem;
		private readonly SignalBus _signalBus;
		private CompositeDisposable _startGameSignalDisposable;

		public TargetGenerator(
			IArm arm,
			SceneResourcesStorage sceneResourcesStorage,
			TargetInfoGenerator targetInfoGenerator,
			LevelsDataControlSystem levelsDataControlSystem,
			SignalBus signalBus
			)
		{
			_arm = arm;
			_sceneResourcesStorage = sceneResourcesStorage;
			_targetInfoGenerator = targetInfoGenerator;
			_levelsDataControlSystem = levelsDataControlSystem;
			_signalBus = signalBus;
		}
		
		public void Initialize()
		{
			_signalBus.GetStream<PreparationGameSignal>().Subscribe(_ => StartGame());
		}

		private void StartGame()
		{
			LevelStorage.Level currentLevel = _levelsDataControlSystem.GetCurrentLevel();
			
			TargetSettings targetSettings = currentLevel.TargetSettings;

			Vector3 targetSpawnPoint = _sceneResourcesStorage.TargetSpawnPoint.position;

			Vector3 startPoint = new Vector3(
				targetSpawnPoint.x - (targetSettings.TargetCubes.Length - 1) +
				(targetSettings.WeighCenterCube * 0.5f) -
				(Mathf.Approximately(targetSettings.WeighCenterCube % 2, 0)
					? 0 : 0.5f),

				targetSpawnPoint.y + 0.5f,

				targetSpawnPoint.z);

			int[] targetInfos = _targetInfoGenerator.GenerateTargetInfo(out int weightTargetInfo);

			Vector3 nextPosition = startPoint;
			int quantityElementInLine = 0;
			
			foreach (var targetInfo in targetInfos)
			{
				if (targetInfo == -1)
					return;
				
				_arm.PoolObjectGetter.GetPoolObject($"Cubes", targetSettings.TargetCubes[targetInfo].PathToCube, nextPosition, Quaternion.identity);
				nextPosition = new Vector3(nextPosition.x + 1, nextPosition.y, nextPosition.z);
				quantityElementInLine++;

				if (quantityElementInLine == weightTargetInfo)
				{
					quantityElementInLine = 0;
					nextPosition = new Vector3(startPoint.x, nextPosition.y + 1, startPoint.z);
				}
			}
		}

		
	}
}