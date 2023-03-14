using ProjectSystems;
using UnityEngine;

namespace Game
{
	public class TestTarget
	{
		public TestTarget(
			LevelsDataControlSystem levelsDataControlSystem,
			TargetInfoGenerator targetInfoGenerator
			)
		{
			var targetInfo = targetInfoGenerator.GenerateTargetInfo(levelsDataControlSystem.GetCurrentLevel().TargetSettings, out int heightTargetInfo, out int weightTargetInfo);

			for (int i = 0; i < heightTargetInfo; i++)
			{
				Debug.LogError($"{targetInfo[i, 0]} {targetInfo[i, 1]} {targetInfo[i, 2]} {targetInfo[i, 3]} {targetInfo[i, 4]} {targetInfo[i, 5]} {targetInfo[i, 6]} {targetInfo[i, 7]} {targetInfo[i, 8]} {targetInfo[i, 9]} {targetInfo[i, 10]}");
			}
		}
	}
}