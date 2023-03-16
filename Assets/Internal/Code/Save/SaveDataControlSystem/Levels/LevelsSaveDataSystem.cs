using Additional;
using Tools.WTools;

namespace Save.Levels
{
	public class LevelsSaveDataSystem : SaveDataSystemBase
	{
		public LevelsSaveDataSystem(IKeysSystem keysSystem) : base(keysSystem)
		{
		}

		public int GetIndexCurrentLevel() =>
			KeysInfo.GetKey<int>(ConstantKeys.CURRENT_LEVEL_ID);

		public void SetIndexCurrentLevel(int indexLevel) =>
			KeysAdmin.SetKey(ConstantKeys.CURRENT_LEVEL_ID, indexLevel);
	}
}