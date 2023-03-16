using Signals;
using Tools.DTools;
using UniRx;
using Zenject;

namespace Game.Systems
{
	public class ScoreCounter : IInitializable
	{
		private readonly SignalBus _signalBus;
		private readonly ContextDisposable _contextDisposable;
		public IReadOnlyReactiveProperty<float> Score => _score;

		private readonly ReactiveProperty<float> _score = new();

		public ScoreCounter(
			SignalBus signalBus,
			ContextDisposable contextDisposable
			)
		{
			_signalBus = signalBus;
			_contextDisposable = contextDisposable;
		}

		public void Initialize()
		{
			_signalBus.GetStream<StartGameSignal>().Subscribe(signal => _score.Value = 0)
				.AddTo(_contextDisposable);
			
			_signalBus.GetStream<IncreaseScoreSignal>().Subscribe(signal => _score.Value += signal.QuantityScore)
				.AddTo(_contextDisposable);
		}
	}
}