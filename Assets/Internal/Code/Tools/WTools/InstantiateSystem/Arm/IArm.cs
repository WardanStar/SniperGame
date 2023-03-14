using UniRx;

namespace Tools.WTools
{
	public interface IArm
	{
		public IReadOnlyReactiveProperty<bool> OnReady { get; }
		public PoolObjectGetter PoolObjectGetter { get; }
		public UIPoolObjectGetter UIPoolObjectGetter { get; }
		public ObjectGetter ObjectGetter { get; }
		public void CreateNewRootFromScene();
		public void InitializeRoot();
		public void ReturnToPoolAllObjectOnID(string idObject);
		public void ReturnToPoolAllObject();
	}
}