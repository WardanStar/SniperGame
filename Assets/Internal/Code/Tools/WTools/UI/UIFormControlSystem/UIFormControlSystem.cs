using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Tools.WTools
{
	public class UIFormControlSystem : IInitializable
	{
		public enum TypeFormAnimation
		{
			ChangeAlpha
		}

		public IReadOnlyReactiveProperty<bool> OnReady => _onReady;

		private readonly UIStorage _uiStorage;
		private readonly UIFormAnimator _formAnimator;

		private ReactiveProperty<bool> _onReady = new();

		private List<UIForm> _activeForms = new(20);

		public UIFormControlSystem(
			UIStorage uiStorage,
			UIFormAnimator formAnimator
			)
		{
			_uiStorage = uiStorage;
			_formAnimator = formAnimator;
		}
		
		public void Initialize()
		{
			_onReady.Value = true;
		}

		public async UniTask<T> ShowForm<T>(
			bool isAnimationShow = false,
			float animationDuration = 2f,
			bool waitToTheAnimationTheEnd = true,
			TypeFormAnimation typeFormAnimation = TypeFormAnimation.ChangeAlpha)
			where T : UIForm
		{
			T currentForm = _uiStorage.GetForm<T>();

			currentForm.gameObject.SetActive(true);
			
			_activeForms.Add(currentForm);
			
			currentForm.ActionBeforeShow();
			
			if (isAnimationShow)
			{
				if (waitToTheAnimationTheEnd)
					await AnimationForm(currentForm, typeFormAnimation, true, animationDuration);
				else
					AnimationForm(currentForm, typeFormAnimation, true, animationDuration).Forget();
			}
			
			currentForm.ActionAfterShow();

			return currentForm;
		}
		
		public async UniTask<T> HideForm<T>(
			bool isAnimationShow = false,
			float animationDuration = 2f,
			bool waitToTheAnimationTheEnd = true,
			TypeFormAnimation typeFormAnimation = TypeFormAnimation.ChangeAlpha
			) 
			where T : UIForm
		{
			T currentForm = _uiStorage.GetForm<T>();

			_activeForms.Remove(currentForm);

			currentForm.ActionBeforeHide();
			
			if (isAnimationShow)
			{
				if (waitToTheAnimationTheEnd)
					await AnimationForm(currentForm, typeFormAnimation, false, animationDuration);
				else
					AnimationForm(currentForm, typeFormAnimation, false, animationDuration).Forget();
			}
			
			currentForm.ActionAfterHide();
			
			currentForm.gameObject.SetActive(false);

			return currentForm;
		}

		public void HideAllActiveForm(bool isAnimationShow = false,
			float animationDuration = 2f,
			TypeFormAnimation typeFormAnimation = TypeFormAnimation.ChangeAlpha)
		{
			foreach (var uiForm in _activeForms)
			{
				if (isAnimationShow)
					AnimationForm(uiForm, typeFormAnimation, false, animationDuration).Forget();
				
				uiForm.gameObject.SetActive(false);
			}
		}
		
		protected virtual void ActionBeforeShow(){}
		protected virtual void ActionAfterShow(){}

		private async UniTask AnimationForm(UIForm uiForm, TypeFormAnimation typeFormAnimation, bool isEmergence, float animationDuration)
		{
			foreach (var button in uiForm.Buttons)
				button.interactable = false;
			
			switch (typeFormAnimation)
			{
				case TypeFormAnimation.ChangeAlpha:
					_formAnimator.ChangeAlphaAnimation(uiForm, animationDuration, isEmergence);
					await UniTask.Delay(TimeSpan.FromSeconds(animationDuration));
					break;
			}
			
			foreach (var button in uiForm.Buttons)
				button.interactable = true;
		}
	}
}