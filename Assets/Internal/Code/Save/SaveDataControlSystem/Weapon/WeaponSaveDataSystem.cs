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
            KeysInfo.GetKeyValue<string>(ConstantKeys.SELECT_WEAPON_ID);

        public void SetWeapon(string idWeapon) =>
            KeysAdmin.SetKeyValue(ConstantKeys.SELECT_WEAPON_ID, idWeapon);
    }
}