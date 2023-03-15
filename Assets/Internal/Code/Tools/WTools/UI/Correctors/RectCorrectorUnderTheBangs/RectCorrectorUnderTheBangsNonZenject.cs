using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools.WTools
{
	public class RectCorrectorUnderTheBangsNonZenject : MonoBehaviour
	{
		[SerializeField] private Canvas _canvas;
		
		[Header("Size")]
		[SerializeField] private bool _changeSizeUp;
		[SerializeField] private bool _changeSizeDown;
		[Header("Position")]
		[SerializeField] private bool _changePositionUp;
		[SerializeField] private bool _changePositionDown;

		[SerializeField] private bool delay;

		private void Start()
		{
			if (delay) {
				Delay().Forget();
				return;
			}
			
			Corrected();
		}
		
		private async UniTaskVoid Delay()
		{
			await UniTask.Yield();
			await UniTask.Yield();
			Corrected();
		}
		
		private void Corrected() {
			
			RectTransform rectTransform = (RectTransform)transform;
			
			if (_canvas == null || rectTransform == null) {
				Debug.LogError($"Canvas in corrector is NULL {this}");
			}
			
			float safeZoneHeight = 0f;
			
#if UNITY_ANDROID
			safeZoneHeight = (_canvas.pixelRect.height - Screen.safeArea.height) / 2;
			_changeSizeDown = false;
			_changePositionDown = false;
#endif
			
#if UNITY_IOS
			safeZoneHeight = (_canvas.pixelRect.height - Screen.safeArea.height - Screen.safeArea.y) / 1.5f;
#endif
			if (_changePositionUp)
				rectTransform.anchoredPosition -= safeZoneHeight * Vector2.up;
			if (_changePositionDown)
				rectTransform.anchoredPosition += safeZoneHeight * Vector2.up;
			if (_changeSizeUp)
				rectTransform.offsetMax -= safeZoneHeight * Vector2.up;
			if (_changeSizeDown)
				rectTransform.offsetMin += safeZoneHeight * Vector2.up;
		}
	}
}