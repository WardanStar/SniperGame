using UnityEngine;

namespace Tools.WTools
{
	public class UIPoolObjectGetter
	{
		private readonly Arm _arm;
		public UIPoolObjectGetter(Arm arm)
		{
			_arm = arm;
		}
		
		#region GetUIPoolObject
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Transform parent) =>
			_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Transform parent, Vector3 position) =>
			_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent, position);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Transform parent, Vector3 position, Quaternion rotation) =>
			_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent, position, rotation);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Transform parent, bool isInject, bool isRepeatedInject) =>
			_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent, default, default, isInject, isRepeatedInject);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Transform parent, Vector3 position, bool isInject, bool isRepeatedInject) =>
			_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent, position, default, isInject, isRepeatedInject);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Transform parent, Vector3 position, Quaternion rotation, bool isInject, bool isRepeatedInject) =>
			_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent, position, rotation, isInject, isRepeatedInject);

		#endregion

		#region GetComponentFromUIPoolObject

		public T GetComponentFromUIPoolObject<T>(string idCollection, string idObject, Transform parent) where T : Object =>
			_arm.GetComponentFromUIPoolObjectBase<T>(idCollection, idObject, parent);
		
		public T GetComponentFromUIPoolObject<T>(string idCollection, string idObject, Transform parent, Vector3 position) where T : Object =>
			_arm.GetComponentFromUIPoolObjectBase<T>(idCollection, idObject, parent, position);
		
		public T GetComponentFromUIPoolObject<T>(string idCollection, string idObject, Transform parent, Vector3 position, Quaternion rotation) where T : Object =>
			_arm.GetComponentFromUIPoolObjectBase<T>(idCollection, idObject, parent, position, rotation, parent);
		
		public T GetComponentFromUIPoolObject<T>(string idCollection, string idObject, Transform parent, bool isInject, bool isRepeatedInject) where T : Object =>
			_arm.GetComponentFromUIPoolObjectBase<T>(idCollection, idObject, parent, default, default, isInject, isRepeatedInject);
		
		public T GetComponentFromUIPoolObject<T>(string idCollection, string idObject, Transform parent, Vector3 position, bool isInject, bool isRepeatedInject) where T : Object =>
			_arm.GetComponentFromUIPoolObjectBase<T>(idCollection, idObject, parent, position, default, isInject, isRepeatedInject);
		
		public T GetComponentFromUIPoolObject<T>(string idCollection, string idObject, Transform parent, Vector3 position, Quaternion rotation, bool isInject, bool isRepeatedInject) where T : Object =>
			_arm.GetComponentFromUIPoolObjectBase<T>(idCollection, idObject, parent, position, rotation, isInject, isRepeatedInject);

		#endregion

		#region WarmUp

		public void WarmUp(string idCollection, string pathToObject, Transform parent, int quantityObjects)
		{
			for (int i = 0; i <= quantityObjects; i++)
				_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent, default, default, true, false, false, true);
		}
		
		public void WarmUp(string idCollection, string pathToObject, Transform parent, int quantityObjects, bool isInject, bool isRepeatedInject)
		{
			for (int i = 0; i <= quantityObjects; i++)
				_arm.GetUIPoolObjectBase(idCollection, pathToObject, parent, default, default, isInject, isRepeatedInject, false, true);
		}

		#endregion
	}
}