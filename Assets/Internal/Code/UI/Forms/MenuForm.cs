using Signals;
using Tools.WTools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Forms
{
	public class MenuForm : UIForm
	{
		[SerializeField] private Button _playGame;
		private SignalBus _signalBus;

		[Inject]
		public void Construct(SignalBus signalBus)
		{
			_signalBus = signalBus;
		}

		private void Start()
		{
			_playGame.onClick.AddListener(() =>
			{
				_signalBus.Fire<PlayGameSignal>();
				Hide<MenuForm>(false);
			});
		}
	}
}