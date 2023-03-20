using Cysharp.Threading.Tasks;
using InputSystem;
using ProjectSystems;
using Settings;
using Signals;
using Tools.DTools;
using Tools.WTools;
using UI.Forms;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public class LevelEndedSystem : IInitializable
    {
        private readonly IJoystick _joystick;
        private readonly SignalBus _signalBus;
        private readonly WeaponInfo _weaponInfo;
        private readonly ScoreCounter _scoreCounter;
        private readonly GameSettings _gameSettings;
        private readonly UIFormControlSystem _uiFormControlSystem;
        private readonly ContextDisposable _contextDisposable;

        public LevelEndedSystem(
            IJoystick joystick,
            SignalBus signalBus,
            WeaponInfo weaponInfo,
            ScoreCounter scoreCounter,
            GameSettings gameSettings,
            UIFormControlSystem uiFormControlSystem,
            ContextDisposable contextDisposable
        )
        {
            _joystick = joystick;
            _signalBus = signalBus;
            _weaponInfo = weaponInfo;
            _scoreCounter = scoreCounter;
            _gameSettings = gameSettings;
            _uiFormControlSystem = uiFormControlSystem;
            _contextDisposable = contextDisposable;
        }
        
        public void Initialize()
        {
            _signalBus.GetStream<CameraReturnToStartPositionSignal>().Subscribe(_ =>
            {
                if (_scoreCounter.Score.Value < _gameSettings.QuantityScoreOnVictory)
                {
                    if (_weaponInfo.Ammunition.Value == 0)
                    {
                        EndedLevel(false);
                        return;
                    }
                    
                    _joystick.ChangeActive(true);
                    return;
                }
                
                EndedLevel(true);
                _signalBus.Fire<NextLevelSignal>();

            }).AddTo(_contextDisposable);
        }

        private void EndedLevel(bool isWin)
        {
            Cursor.visible = true;
            _uiFormControlSystem.HideForm<InscriptionBeforeAimingForm>().Forget();

            if (isWin)
                _uiFormControlSystem.ShowForm<VictoryForms>(true, 1f).Forget();
            else
                _uiFormControlSystem.ShowForm<LostForm>(true, 1f).Forget();
        }
    }
}