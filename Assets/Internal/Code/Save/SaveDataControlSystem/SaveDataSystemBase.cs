using Tools.WTools;

namespace Save
{
	public abstract class SaveDataSystemBase
	{
		protected readonly KeysInfo KeysInfo;
		protected readonly KeysAdmin KeysAdmin;

		protected SaveDataSystemBase(
			IKeysSystem keysSystem
			)
		{
			KeysAdmin = keysSystem.KeysAdmin;
			KeysInfo = keysSystem.KeysInfo;
		}
	}
}