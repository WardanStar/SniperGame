using ProjectSystems;
using Settings;

namespace Game
{
    public class TargetInfoGenerator
    {
        private readonly LevelsDataControlSystem _levelsDataControlSystem;
        private LevelStorage.TargetSettings _targetSettings;
        
        private readonly int[] _targetInfos;

        public TargetInfoGenerator(
            LevelsDataControlSystem levelsDataControlSystem
            )
        {
            _levelsDataControlSystem = levelsDataControlSystem;
            
            _targetInfos = new int[levelsDataControlSystem.GetMaxTargetSize(true) * levelsDataControlSystem.GetMaxTargetSize(false)];
        }
        
/// <summary>
/// Returns an int[] that contains information on building a target,
/// each digit indicates an index in the array of data on building a target,
/// which is configured in the level settings.
/// </summary>
/// <param name="weightTargetInfo">Returns the number of elements on one horizontal construction line.</param>
/// <returns></returns>
        public int[] GenerateTargetInfo(out int weightTargetInfo)
        {
            ClearTargetInfo();
            
            var targetSettings = _levelsDataControlSystem.GetCurrentLevel().TargetSettings;
            
            int heightCenterCube = targetSettings.HeightCenterCube;
            int weightCenterCube = targetSettings.WeighCenterCube;
            int quantityCubesInLevel = targetSettings.TargetCubes.Length - 1;
            
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
            
            return _targetInfos;
        }

        private void FillLine(int indexLine, int quantityCubeInLine, int totalQuantityCube, int weightCenterCube)
        {
            int indexStart = ((totalQuantityCube * 2) + weightCenterCube) * indexLine;
            
            for (int i = 0; i < quantityCubeInLine; i++)
            {
                int indexCurrentElement = totalQuantityCube - i;
                
                _targetInfos[indexStart + i] = indexCurrentElement;
                _targetInfos[indexStart + (totalQuantityCube * 2) - i + weightCenterCube - 1] = indexCurrentElement;
            }

            for (int i = quantityCubeInLine; i < (totalQuantityCube * 2) - quantityCubeInLine + weightCenterCube; i++)
            {
                _targetInfos[indexStart + i] = totalQuantityCube - quantityCubeInLine + 1;
            }
        }

        private void ClearTargetInfo()
        {
            for (var index = 0; index < _targetInfos.Length; index++)
                _targetInfos[index] = -1;
        }
    }
}