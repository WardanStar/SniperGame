using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.WTools
{
    [CreateAssetMenu(menuName = "Settings/InstatiateSettings/ResourcesCollection")]
    public class StorageOfPathsToObjectInResources : ScriptableObject
    {
        [Serializable]
        public class ObjectDataInResources
        {
            public string IDObject => _idObject;
            public string PathToResources => _pathToResources;


            [SerializeField] private string _idObject;
            [SerializeField] private string _pathToResources;
        }

        [SerializeField] private List<ObjectDataInResources> _storageObjectDataInResources = new List<ObjectDataInResources>();

        public string GetPathObjectToID(string id)
        {
            foreach (var objectDataInResources in _storageObjectDataInResources)
            {
                if (objectDataInResources.IDObject == id)
                    return objectDataInResources.PathToResources;
            }

            throw new NullReferenceException($"Common id {id} is not used");
        }
    }
}