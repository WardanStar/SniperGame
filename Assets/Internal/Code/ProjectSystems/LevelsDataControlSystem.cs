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
			_levelStorage.GetLevel(_indexLevel);

		private void NextLevel()
		{
			_indexLevel++;
		}
	}
}