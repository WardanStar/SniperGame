using Save;
using Save.Levels;
using Settings;
using Signals;
using UniRx;
using Zenject;

namespace ProjectSystems
{
    public class LevelsDataControlSystem : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly LevelStorage _levelStorage;
        private int _indexLevel;
        private readonly LevelsSaveDataSystem _levelsSaveDataSystem;

        public LevelsDataControlSystem(
            ISaveDataControlSystem saveDataControlSystem,
            SignalBus signalBus,
            LevelStorage levelStorage
            )
        {
            _signalBus = signalBus;
            _levelStorage = levelStorage;
            _levelsSaveDataSystem = saveDataControlSystem.LevelsSaveDataSystem;
            _indexLevel = _levelsSaveDataSystem.GetIndexCurrentLevel();
        }
        
        public void Initialize()
        {
            _signalBus.GetStream<NextLevelSignal>().Subscribe(_ => NextLevel());
        }
        
        /// <summary>
        /// Returns the currently selected level settings
        /// </summary>
        /// <returns></returns>
        public LevelStorage.Level GetCurrentLevel() =>
            GetLevel(_indexLevel);

        /// <summary>
        /// Returns the index of the level currently selected
        /// </summary>
        /// <returns></returns>
        public int GetIndexCurrentLevel() => _indexLevel;

        /// <summary>
        /// Returns the largest target size vertically or horizontally at all levels
        /// </summary>
        /// <param name="isVerticalSize">true : vertiacl size, false : horizontal size</param>
        /// <returns></returns>
        public int GetMaxTargetSize(bool isVerticalSize)
        {
            int maxSize = 0;
            
            foreach (LevelStorage.Level level in _levelStorage.Levels)
            {
                LevelStorage.TargetSettings targetSettings = level.TargetSettings;
                
                int currentSize = (isVerticalSize ? targetSettings.HeightCenterCube : targetSettings.WeighCenterCube) +
                                  ((targetSettings.TargetCubes.Length - 1) * 2);
                
                if (maxSize < currentSize)
                    maxSize = currentSize;
            }

            return maxSize;
        }
        
        private void NextLevel()
        {
            if (_indexLevel > _levelStorage.Levels.Length - 1)
                return;
            
            _indexLevel++;
            _levelsSaveDataSystem.SetIndexCurrentLevel(_indexLevel);
        }
        
        private LevelStorage.Level GetLevel(int indexLevel)
        {
            LevelStorage.Level[] levels = _levelStorage.Levels;
            
            return indexLevel > levels.Length - 1
                ? levels[^1]
                : levels[indexLevel];
        }
    }
}