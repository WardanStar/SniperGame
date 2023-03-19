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
            KeysInfo.GetKeyValue<int>(ConstantKeys.CURRENT_LEVEL_ID);

        public void SetIndexCurrentLevel(int indexLevel) =>
            KeysAdmin.SetKeyValue(ConstantKeys.CURRENT_LEVEL_ID, indexLevel);
    }
}