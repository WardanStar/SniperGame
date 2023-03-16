using Save.Levels;
using Save.Weapon;

namespace Save
{
	public interface ISaveDataControlSystem
	{
		public LevelsSaveDataSystem LevelsSaveDataSystem { get; }
		public WeaponSaveDataSystem WeaponSaveDataSystem { get; }
		public void Initialize();
		public void DeleteAllKeys();
	}
}