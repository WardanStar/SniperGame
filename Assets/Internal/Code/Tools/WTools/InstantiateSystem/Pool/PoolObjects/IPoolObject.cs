namespace Tools.WTools
{
	public interface IPoolObject : ILockedMonoBehaviour
	{
		public string ID { get; }
		
		public bool IsUsed { get; }

		public void Using();
		public void ReturnToPool();

		public void SetID(string id);
	}
}