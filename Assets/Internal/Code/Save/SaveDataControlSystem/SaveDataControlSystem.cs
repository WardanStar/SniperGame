using Additional;
using Save.Levels;
using Save.Weapon;
using Settings;
using Tools.WTools;

namespace Save
{
	public class SaveDataControlSystem : ISaveDataControlSystem
	{
		public LevelsSaveDataSystem LevelsSaveDataSystem => _levelSaveDataSystem;
		public WeaponSaveDataSystem WeaponSaveDataSystem => _weaponSaveSystem;
		
		private readonly IKeysSystem _keysSystem;
		private readonly GameSettings _gameSettings;
		private readonly LevelsSaveDataSystem _levelSaveDataSystem;
		private readonly WeaponSaveDataSystem _weaponSaveSystem;

		public SaveDataControlSystem(
			IKeysSystem keysSystem,
			GameSettings gameSettings
			)
		{
			_keysSystem = keysSystem;
			_gameSettings = gameSettings;
			_levelSaveDataSystem = new LevelsSaveDataSystem(keysSystem);
			_weaponSaveSystem = new WeaponSaveDataSystem(keysSystem);
		}

		public void Initialize()
		{
			_keysSystem.KeysAdmin.SetKey(ConstantKeys.SELECT_WEAPON_ID, _gameSettings.DefaultWeaponID, true);
		}

		public void DeleteAllKeys()
		{
			_keysSystem.KeysAdmin.DeleteAllKeys();
		}
	}
}