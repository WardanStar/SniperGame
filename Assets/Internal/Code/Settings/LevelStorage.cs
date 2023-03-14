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


		public Level[] Levels => _levels;

		[SerializeField] private Level[] _levels;
	}
}