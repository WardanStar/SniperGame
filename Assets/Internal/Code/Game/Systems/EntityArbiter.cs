using ProjectSystems;
using Signals;
using Tools.DTools;
using UniRx;
using Zenject;

namespace Game.Systems
{
    public class EntityArbiter : IInitializable
    {
        private readonly SignalBus _signalBus;
        private readonly WeaponInfo _weaponInfo;
        private readonly ContextDisposable _contextDisposable;

        public EntityArbiter(
            SignalBus signalBus,
            WeaponInfo weaponInfo,
            ContextDisposable contextDisposable
            )
        {
            _signalBus = signalBus;
            _weaponInfo = weaponInfo;
            _contextDisposable = contextDisposable;
        }
        
        public void Initialize()
        {
            _signalBus.GetStream<KillTargetElementSignal>().Subscribe(signal =>
            {
                _signalBus.Fire(new IncreaseScoreSignal(){QuantityScore = signal.QuantityScoreOnDestroy * _weaponInfo.GetCurrentWeapon().ScoringRatio});
            }).AddTo(_contextDisposable);
        }
    }
}