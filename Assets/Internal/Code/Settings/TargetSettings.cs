using System;
using Additional.Enums;
using UnityEngine;

namespace Settings
{
	[Serializable]
	public class TargetSettings
	{
		[Serializable]
		public class TargetCube
		{
			public string PathToCube => _pathToCube.ToString();
			//public int QuantityScoreForDestroyingCube => _quantityScoreForDestroyingCube;

			[SerializeField] private TargetCubeElementId _pathToCube;
			//[SerializeField] private int _quantityScoreForDestroyingCube;
		}

		public TargetCube[] TargetCubes => _targetCubes;

		public int HeightCenterCube => _heightCenterCube;
		public int WeighCenterCube => _weighCenterCube;

		[SerializeField] private TargetCube[] _targetCubes;
		
		[SerializeField] private int _heightCenterCube = 1;
		[SerializeField] private int _weighCenterCube = 1;

	}
}