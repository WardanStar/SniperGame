using System;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Storages/LevelStorage")]
	public class LevelStorage : ScriptableObject
	{
		[Serializable]
		public class Level
		{
			public TargetSettings TargetSettings => _targetSettings;

			[SerializeField] private TargetSettings _targetSettings;
		}
		
		
		[SerializeField] private Level[] _levels;

		public Level GetLevel(int indexLevel) =>
			indexLevel > _levels.Length - 1 ? _levels[^1] : _levels[indexLevel];

		public int GetMaxTargetSize(bool height)
		{
			int maxSize = 0;
			
			foreach (Level level in _levels)
			{
				TargetSettings targetSettings = level.TargetSettings;
				
				int currentSize = (height ? targetSettings.HeightCenterCube : targetSettings.WeighCenterCube) +
				                  ((targetSettings.TargetCubes.Length - 1) * 2);
				
				if (maxSize < currentSize)
					maxSize = currentSize;
			}

			return maxSize;
		}
	}
}