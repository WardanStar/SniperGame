using Additional;
using Game.Data;
using Game.Entities;
using ProjectSystems;
using Settings;
using Signals;
using Tools.WTools;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game
{
    public class TargetGenerator : IInitializable
    {
        private readonly IArm _arm;
        private readonly ILoader _loader;
        private readonly SceneResourcesStorage _sceneResourcesStorage;
        private readonly TargetInfoGenerator _targetInfoGenerator;
        private readonly LevelsDataControlSystem _levelsDataControlSystem;
        private readonly SignalBus _signalBus;
        private CompositeDisposable _startGameSignalDisposable;

        public TargetGenerator(
            IArm arm,
            ILoader loader,
            SceneResourcesStorage sceneResourcesStorage,
            TargetInfoGenerator targetInfoGenerator,
            LevelsDataControlSystem levelsDataControlSystem,
            SignalBus signalBus
            )
        {
            _arm = arm;
            _loader = loader;
            _sceneResourcesStorage = sceneResourcesStorage;
            _targetInfoGenerator = targetInfoGenerator;
            _levelsDataControlSystem = levelsDataControlSystem;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.GetStream<PreparationGameSignal>().Subscribe(_ => StartGame());
        }

        private void StartGame()
        {
            LevelStorage.Level currentLevel = _levelsDataControlSystem.GetCurrentLevel();
            
            LevelStorage.TargetSettings targetSettings = currentLevel.TargetSettings;

            Vector3 targetSpawnPoint = _sceneResourcesStorage.TargetSpawnPoint.position;

            float targetCubesSizeX = (targetSettings.TargetCubes.Length - 1) * currentLevel.SizeTargetElement;
            float targetCenterSizeX = (targetSettings.WeighCenterCube * 0.5f) * currentLevel.SizeTargetElement;
            float correctionX = currentLevel.SizeTargetElement * 0.5f;

            Vector3 startPoint = new Vector3(
                (targetSpawnPoint.x - 
                targetCubesSizeX -
                targetCenterSizeX +
                correctionX),

                targetSpawnPoint.y + currentLevel.SizeTargetElement * 0.5f,

                targetSpawnPoint.z);

            int[] targetInfos = _targetInfoGenerator.GenerateTargetInfo(out int weightTargetInfo);

            Vector3 nextPosition = startPoint;
            int quantityElementInLine = 0;
            
            foreach (var targetInfo in targetInfos)
            {
                if (targetInfo == -1)
                    return;
                
                var cubeElement = 
                    _arm.PoolObjectGetter.GetComponentFromPoolObject<TargetCubeElementMono>
                        (ConstantKeys.TARGET_CUBE_ELEMENT_COLLECTION_ID, ConstantKeys.TARGET_CUBE_ELEMENT_ID, nextPosition, Quaternion.identity);

                LevelStorage.TargetSettings.TargetCube cubeSettings = targetSettings.TargetCubes[targetInfo];

                cubeElement.transform.localScale = currentLevel.SizeTargetElement * Vector3.one;
                
                cubeElement.SetMaterials(_loader.LoadObject<Material>(
                    ConstantKeys.TARGET_CUBE_ELEMENT_MATERIALS_COLLECTION_ID, cubeSettings.PathToCube));
                
                cubeElement.SetQuantityScore(cubeSettings.QuantityScoreForDefeatingCube);
                
                nextPosition = new Vector3(nextPosition.x + currentLevel.SizeTargetElement, nextPosition.y, nextPosition.z);
                quantityElementInLine++;

                if (quantityElementInLine == weightTargetInfo)
                {
                    quantityElementInLine = 0;
                    nextPosition = new Vector3(startPoint.x, nextPosition.y + currentLevel.SizeTargetElement, startPoint.z);
                }
            }
        }

        
    }
}