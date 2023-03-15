using Save.Weapon;

namespace Save
{
	public interface ISaveDataControlSystem
	{
		public WeaponSaveDataSystem WeaponSaveDataSystem { get; }
		public void Initialize();
		public void DeleteAllKeys();
	}
}