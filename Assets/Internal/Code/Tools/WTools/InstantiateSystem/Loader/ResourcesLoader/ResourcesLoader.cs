using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tools.WTools
{
	public class ResourcesLoader : ILoader
	{
		private readonly StorageOfResourcesCollection _storageOfResourcesCollection;

		public ResourcesLoader(
			StorageOfResourcesCollection storageOfResourcesCollection
			)
		{
			_storageOfResourcesCollection = storageOfResourcesCollection;
		}
		
		
		public T LoadObject<T>(string idCollection, string idObject) where T : Object
		{
			var pathToObject = _storageOfResourcesCollection.GetPathObjectToID(idCollection, idObject);
			
			var obj = Resources.Load<T>(pathToObject);
			
			if (ReferenceEquals(obj, null))
				throw new NullReferenceException();
			
			return obj;
		}
	}
}