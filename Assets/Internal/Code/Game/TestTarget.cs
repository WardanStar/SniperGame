using UnityEngine;

namespace Game
{
	public class TestTarget
	{
		public TestTarget(
			TargetInfoGenerator targetInfoGenerator
			)
		{
			var level = targetInfoGenerator.GetTargetInfo(0, out int heightTargetInfo, out int weightTargetInfo);

			for (int i = 0; i < heightTargetInfo; i++)
			{
				Debug.LogError($"{level[i, 0]} {level[i, 1]} {level[i, 2]} {level[i, 3]} {level[i, 4]} {level[i, 5]} {level[i, 6]} {level[i, 7]} {level[i, 8]} {level[i, 9]} {level[i, 10]}");
			}
		}
	}
}