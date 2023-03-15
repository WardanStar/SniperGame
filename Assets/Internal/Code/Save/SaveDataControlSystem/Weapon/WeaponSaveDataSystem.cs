using Additional;
using Tools.WTools;

namespace Save.Weapon
{
	public class WeaponSaveDataSystem : SaveDataSystemBase
	{
		public WeaponSaveDataSystem(IKeysSystem keysSystem) : base(keysSystem)
		{
		}

		public string GetIDCurrentWeapon() =>
			KeysInfo.GetKey<string>(ConstantKeys.SELECT_WEAPON_ID);

		public void SetWeapon(string idWeapon) =>
			KeysAdmin.SetKey(ConstantKeys.SELECT_WEAPON_ID, idWeapon);
	}
}