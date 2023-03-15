using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Tools.DTools
{
	public class ContextDisposable : IDisposable
	{
		private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();

		public void AddDisposable(IDisposable disposable)
		{
			_compositeDisposable.Add(disposable);
		}
		
		void IDisposable.Dispose() {
			_compositeDisposable.Dispose();
		}
	}

	public static class DisposableExtensions
	{
		public static IDisposable AddTo(this IDisposable disposable, ContextDisposable contextDisposable)
		{
			contextDisposable.AddDisposable(disposable);
			return disposable;
		}
	}
}