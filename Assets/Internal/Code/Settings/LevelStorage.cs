using System;
using Additional.Enums;
using UnityEngine;

namespace Settings
{
	[CreateAssetMenu(menuName = "Settings/LevelStorage")]
	public class LevelStorage : ScriptableObject
	{
		[Serializable]
		public class TargetSettings
		{
			[Serializable]
			public class TargetCube
			{
				public string PathToCube => _pathToCube.ToString();
				public int QuantityScoreForDestroyingCube => _quantityScoreForDestroyingCube;

				[SerializeField] private TargetCubeElementId _pathToCube;
				[SerializeField] private int _quantityScoreForDestroyingCube;
			}

			public TargetCube[] TargetCubes => _targetCubes;

			public int HeightCenterCube => _heightCenterCube;
			public int WeighCenterCube => _weighCenterCube;

			[SerializeField] private TargetCube[] _targetCubes;
		
			[SerializeField] private int _heightCenterCube = 1;
			[SerializeField] private int _weighCenterCube = 1;

		}
		
		[Serializable]
		public class Level
		{
			public TargetSettings TargetSettings => _targetSettings;
			public int SizeTargetElement => _sizeTargetElement;

			public float DistanceToTarget => _distanceToTarget;

			[Header("TargetSettings")]
			[SerializeField] private TargetSettings _targetSettings;
			[SerializeField] private int _sizeTargetElement = 1;

			[Header("OtherSettings")]
			[SerializeField] private float _distanceToTarget;
		}


		public Level[] Levels => _levels;

		[SerializeField] private Level[] _levels;
	}
}