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
		private readonly WeaponControlSystem _weaponControlSystem;
		private readonly ContextDisposable _contextDisposable;

		public EntityArbiter(
			SignalBus signalBus,
			WeaponControlSystem weaponControlSystem,
			ContextDisposable contextDisposable
			)
		{
			_signalBus = signalBus;
			_weaponControlSystem = weaponControlSystem;
			_contextDisposable = contextDisposable;
		}
		
		
		public void Initialize()
		{
			_signalBus.GetStream<KillTargetElementSignal>().Subscribe(signal =>
			{
				_signalBus.Fire(new IncreaseScoreSignal(){QuantityScore = signal.QuantityScoreOnDestroy * _weaponControlSystem.GetCurrentWeapon().ScoringRatio});
			}).AddTo(_contextDisposable);
		}
	}
}