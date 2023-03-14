using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Storages/LevelStorage")]
	public class LevelSettings : ScriptableObject
	{
		[SerializeField] private TargetSettings[] _targetSettings;

		public TargetSettings GetLevel(int indexLevel)
		{
			if (indexLevel > _targetSettings.Length - 1)
				return null;

			return _targetSettings[indexLevel];
		}

		public int GetMaxTargetSize(bool height)
		{
			int maxSize = 0;
			
			foreach (var targetSettings in _targetSettings)
			{
				int currentSize = (height ? targetSettings.HeightCenterCube : targetSettings.WeighCenterCube) + ((targetSettings.TargetCubes.Length - 1) * 2);
				
				if (maxSize < currentSize)
					maxSize = currentSize;
			}

			return maxSize;
		}
	}
}