using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace Game
{
	public class TargetInfoGenerator
	{
		private readonly LevelSettings _levelSettings;

		private TargetSettings _targetSettings;
		
		private readonly int[,] _targetInfo;

		public TargetInfoGenerator(
			LevelSettings levelSettings
			)
		{
			_levelSettings = levelSettings;

			_targetInfo = new int[levelSettings.GetMaxTargetSize(true), levelSettings.GetMaxTargetSize(false)];
		}

		public int[,] GetTargetInfo(int level, out int heightTargetInfo, out int weightTargetInfo)
		{
			_targetSettings = _levelSettings.GetLevel(level);

			int heightCenterCube = _targetSettings.HeightCenterCube;
			int weightCenterCube = _targetSettings.WeighCenterCube;
			int quantityCubesInLevel = _targetSettings.TargetCubes.Length - 1;
			
			heightTargetInfo = quantityCubesInLevel * 2 + heightCenterCube;
			weightTargetInfo = quantityCubesInLevel * 2 + weightCenterCube;
			
			int quantityCubesInLine = 1;
			
			for (int i = 0; i < quantityCubesInLevel; i++)
			{
				FillLine(i, quantityCubesInLine, quantityCubesInLevel, weightCenterCube);

				quantityCubesInLine++;
			}

			for (int i = quantityCubesInLevel; 
				i < quantityCubesInLevel + heightCenterCube; i++)
			{
				FillLine(i, quantityCubesInLevel + 1, quantityCubesInLevel, weightCenterCube);
			}
			
			for (int i = quantityCubesInLevel + heightCenterCube;
				i < (quantityCubesInLevel) * 2 + heightCenterCube; i++)
			{
				quantityCubesInLine--;
				
				FillLine(i, quantityCubesInLine, quantityCubesInLevel, weightCenterCube);
			}
			
			return _targetInfo;
		}

		private void FillLine(int indexLine, int quantityCubeInLine, int totalQuantityCube, int weightCenterCube)
		{
			for (int i = 0; i < quantityCubeInLine; i++)
			{
				var indexCurrentElement = totalQuantityCube - i;
				
				_targetInfo[indexLine, i] = indexCurrentElement;
				_targetInfo[indexLine, (totalQuantityCube * 2) - i + weightCenterCube - 1] = indexCurrentElement;
			}

			for (int i = quantityCubeInLine; i < (totalQuantityCube * 2) - quantityCubeInLine + weightCenterCube; i++)
			{
				_targetInfo[indexLine, i] = totalQuantityCube - quantityCubeInLine + 1;
			}
		}
	}
}