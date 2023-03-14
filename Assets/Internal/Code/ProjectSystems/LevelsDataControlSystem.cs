using Settings;

namespace ProjectSystems
{
	public class LevelsDataControlSystem
	{
		private readonly LevelStorage _levelStorage;
		private int _indexLevel;

		public LevelsDataControlSystem(
			LevelStorage levelStorage
			)
		{
			_levelStorage = levelStorage;
		}
		
		public LevelStorage.Level GetCurrentLevel() =>
			GetLevel(_indexLevel);

		public int GetMaxTargetSize(bool height)
		{
			int maxSize = 0;
			
			foreach (LevelStorage.Level level in _levelStorage.Levels)
			{
				TargetSettings targetSettings = level.TargetSettings;
				
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