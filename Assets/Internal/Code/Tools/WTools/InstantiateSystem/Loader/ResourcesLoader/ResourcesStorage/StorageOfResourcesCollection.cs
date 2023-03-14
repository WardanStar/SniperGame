using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.WTools
{
	[CreateAssetMenu(menuName = "Settings/InstatiateSettings/StorageOfResourcesCollection")]
	public class StorageOfResourcesCollection : ScriptableObject
	{
		[Serializable]
		public class ResourcesCollection
		{
			public string IDCollection => idCollection;
			public StorageOfPathsToObjectInResources ResourcesList => _resourcesList;

			[SerializeField] private string idCollection;
			[SerializeField] private StorageOfPathsToObjectInResources _resourcesList;
		}
		
		[SerializeField] private List<ResourcesCollection> _storageObjectDataInResources = new();
		
		public string GetPathObjectToID(string idCollection, string id) =>
			GetCollection(idCollection).ResourcesList.GetPathObjectToID(id);

		private ResourcesCollection GetCollection(string idCollection)
		{
			foreach (var objectDataInResources in _storageObjectDataInResources)
			{
				if (objectDataInResources.IDCollection == idCollection)
				{
					return objectDataInResources;
				}
			}
			
			throw new NullReferenceException($"Common collection id {idCollection} is not used");
		}
	}
}