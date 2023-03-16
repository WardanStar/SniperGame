using Save;
using Save.Levels;
using Settings;
using Signals;
using UniRx;
using Zenject;

namespace ProjectSystems
{
	public class LevelsDataControlSystem : IInitializable
	{
		private readonly SignalBus _signalBus;
		private readonly LevelStorage _levelStorage;
		private int _indexLevel;
		private readonly LevelsSaveDataSystem _levelsSaveDataSystem;

		public LevelsDataControlSystem(
			ISaveDataControlSystem saveDataControlSystem,
			SignalBus signalBus,
			LevelStorage levelStorage
			)
		{
			_signalBus = signalBus;
			_levelStorage = levelStorage;
			_levelsSaveDataSystem = saveDataControlSystem.LevelsSaveDataSystem;
			_indexLevel = _levelsSaveDataSystem.GetIndexCurrentLevel();
		}
		
		public void Initialize()
		{
			_signalBus.GetStream<NextLevelSignal>().Subscribe(_ => NextLevel());
		}
		
		public LevelStorage.Level GetCurrentLevel() =>
			GetLevel(_indexLevel);

		public int GetIndexCurrentLevel() => _indexLevel;

		public int GetMaxTargetSize(bool height)
		{
			int maxSize = 0;
			
			foreach (LevelStorage.Level level in _levelStorage.Levels)
			{
				LevelStorage.TargetSettings targetSettings = level.TargetSettings;
				
				int currentSize = (height ? targetSettings.HeightCenterCube : targetSettings.WeighCenterCube) +
				                  ((targetSettings.TargetCubes.Length - 1) * 2);
				
				if (maxSize < currentSize)
					maxSize = currentSize;
			}

			return maxSize;
		}
		
		private void NextLevel()
		{
			_indexLevel++;
			_levelsSaveDataSystem.SetIndexCurrentLevel(_indexLevel);
		}
		
		private LevelStorage.Level GetLevel(int indexLevel)
		{
			var levels = _levelStorage.Levels;
			
			return indexLevel > levels.Length - 1
				? levels[^1]
				: levels[indexLevel];
		}
	}
}