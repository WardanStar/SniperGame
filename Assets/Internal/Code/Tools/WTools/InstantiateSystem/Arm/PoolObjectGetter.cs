using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.WTools
{
	public class PoolObjectGetter
	{
		private readonly Arm _arm;

		public PoolObjectGetter(
			Arm arm
			)
		{
			_arm = arm;
		}

		#region GetPoolObject
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject) =>
			_arm.GetPoolObjectBase(idCollection, pathToObject);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Vector3 position, Quaternion rotation) =>
			_arm.GetPoolObjectBase(idCollection, pathToObject, position, rotation);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Vector3 position, Quaternion rotation, Transform parent) =>
			_arm.GetPoolObjectBase(idCollection, pathToObject, position, rotation, parent);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, bool isInject, bool isRepeatedInject) =>
			_arm.GetPoolObjectBase(idCollection, pathToObject, default, default, null, isInject, isRepeatedInject);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Vector3 position, Quaternion rotation, bool isInject, bool isRepeatedInject) =>
			_arm.GetPoolObjectBase(idCollection, pathToObject, position, rotation, null, isInject, isRepeatedInject);
		
		public IPoolObject GetPoolObject(string idCollection, string pathToObject, Vector3 position, Quaternion rotation, Transform parent , bool isInject, bool isRepeatedInject) =>
			_arm.GetPoolObjectBase(idCollection, pathToObject, position, rotation, parent, isInject, isRepeatedInject);
		

		#endregion

		#region GetComponentFromPoolObject

		public T GetComponentFromPoolObject<T>(string idCollection, string idObject) where T : Object =>
			_arm.GetComponentFromPoolObjectBase<T>(idCollection, idObject);
		
		public T GetComponentFromPoolObject<T>(string idCollection, string idObject, Vector3 position, Quaternion rotation) where T : Object =>
			_arm.GetComponentFromPoolObjectBase<T>(idCollection, idObject, position, rotation);
		
		public T GetComponentFromPoolObject<T>(string idCollection, string idObject, Vector3 position, Quaternion rotation, Transform parent) where T : Object =>
			_arm.GetComponentFromPoolObjectBase<T>(idCollection, idObject, position, rotation, parent);
		
		public T GetComponentFromPoolObject<T>(string idCollection, string idObject, bool isInject, bool isRepeatedInject) where T : Object =>
			_arm.GetComponentFromPoolObjectBase<T>(idCollection, idObject, default, default, null, isInject, isRepeatedInject);
		
		public T GetComponentFromPoolObject<T>(string idCollection, string idObject, Vector3 position, Quaternion rotation, bool isInject, bool isRepeatedInject) where T : Object =>
			_arm.GetComponentFromPoolObjectBase<T>(idCollection, idObject, position, rotation, null, isInject, isRepeatedInject);
		
		public T GetComponentFromPoolObject<T>(string idCollection, string idObject, Vector3 position, Quaternion rotation, Transform parent , bool isInject, bool isRepeatedInject) where T : Object =>
			_arm.GetComponentFromPoolObjectBase<T>(idCollection, idObject, position, rotation, parent, isInject, isRepeatedInject);

		#endregion

		#region WarmUp

		public void WarmUp(string idCollection, string pathToObject, int quantityObjects)
		{
			for (int i = 0; i <= quantityObjects; i++)
				_arm.GetPoolObjectBase(idCollection, pathToObject, default, default, null, true, false, false, true);
		}
		
		public void WarmUp(string idCollection, string pathToObject, int quantityObjects, bool isInject, bool isRepeatedInject)
		{
			for (int i = 0; i <= quantityObjects; i++)
				_arm.GetPoolObjectBase(idCollection, pathToObject, default, default, null, isInject, isRepeatedInject, false, true);
		}
		
		public void WarmUp(string idCollection, string pathToObject, int quantityObjects, Transform parent)
		{
			for (int i = 0; i <= quantityObjects; i++)
				_arm.GetPoolObjectBase(idCollection, pathToObject, default, default, parent, true, false, false, true);
		}
		
		public void WarmUp(string idCollection, string pathToObject, int quantityObjects, Transform parent, bool isInject, bool isRepeatedInject)
		{
			for (int i = 0; i <= quantityObjects; i++)
				_arm.GetPoolObjectBase(idCollection, pathToObject, default, default, parent, isInject, isRepeatedInject, false, true);
		}

		#endregion
	}
}