﻿using System;
using Game.Misc;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Tools.WTools
{
	public class Arm : IArm
	{
		public IReadOnlyReactiveProperty<bool> OnReady => _onReady;
		public PoolObjectGetter PoolObjectGetter => _poolObjectGetter;
		public UIPoolObjectGetter UIPoolObjectGetter => _uiPoolObjectGetter;
		public ObjectGetter ObjectGetter => _objectGetter;

		private readonly ObjectInjector _objectInjector;
		private readonly PoolStorage _poolStorage;
		private readonly ILoader _loader;
		private readonly DiContainer _container;
		private readonly PoolObjectGetter _poolObjectGetter;
		private readonly UIPoolObjectGetter _uiPoolObjectGetter;
		private readonly ObjectGetter _objectGetter;
		
		private readonly ReactiveProperty<bool> _onReady = new();
		
		private Transform _poolRoot;

		public Arm(
			ObjectInjector objectInjector,
			PoolStorage poolStorage,
			ILoader loader,
			DiContainer container
			)
		{
			_objectInjector = objectInjector;
			_poolStorage = poolStorage;
			_loader = loader;
			_container = container;

			_poolObjectGetter = new PoolObjectGetter(this);
			_uiPoolObjectGetter = new UIPoolObjectGetter(this);
			_objectGetter = new ObjectGetter();
		}
		
		public void CreateNewRootFromScene()
		{
			_poolRoot = new GameObject("Pool").transform;
		}

		public void InitializeRoot()
		{
			if (_poolRoot != null)
				return;
				
			CreateNewRootFromScene();
			_onReady.Value = true;
		}

		public void ReturnToPoolAllObjectOnID(string idObject)
		{
			Pool pool = _poolStorage.GetPool(idObject);
			
			pool.ReturnToPoolAllObject();
		}

		public void ReturnToPoolAllObject()
		{
			foreach (Pool pool in _poolStorage.Pools.Values)
				pool.ReturnToPoolAllObject();
		}

		public IPoolObject GetPoolObjectBase(string idCollection, string idObject, Vector3 position = default, Quaternion rotation = default,
			Transform parent = null, bool isInject = true, bool isRepeatedInject = false, bool isActive = true, bool warmUpObject = false)
		{
			Pool pool = _poolStorage.GetPool(idObject);

			PoolObjectBase poolObject = pool.GetPoolObject();

			bool isNewObject = false;

			if (poolObject == null)
			{
				isNewObject = true;
				
				var loadObj = _loader.LoadObject<PoolObjectBase>(idCollection, idObject);

				if (ReferenceEquals(loadObj, null))
					throw new NullReferenceException();

				poolObject = isInject ?
					_container.InstantiatePrefabForComponent<PoolObjectBase>(loadObj) :
					Object.Instantiate(loadObj, position, rotation, parent ?? _poolRoot);
				
				pool.AddPoolObject(poolObject);
			}

			if (!warmUpObject)
				poolObject.Using();
			
			PreparationPoolObject(idObject, poolObject, position, rotation, parent, isNewObject, isRepeatedInject, isActive);
			
			return poolObject;
		}
		
		public T GetComponentFromPoolObjectBase<T>(string idCollection, string idObject, Vector3 position = default, Quaternion rotation = default,
			Transform parent = null, bool isInject = true, bool isRepeatedInject = false, bool isActive = true) where T : Object
		{
			IPoolObject poolObject = GetPoolObjectBase(idCollection, idObject, position, rotation, parent, isInject, isRepeatedInject, isActive);

			return poolObject.GetComponentFromGameObject<T>();
		}
		
		public IPoolObject GetUIPoolObjectBase(string idCollection, string idObject, Transform parent, Vector2 position = default, Quaternion rotation = default,
			bool isInject = true, bool isRepeatedInject = false, bool isActive = true, bool warmUpObject = false)
		{
			Pool pool = _poolStorage.GetPool(idObject);

			PoolObjectBase poolObject = pool.GetPoolObject();

			bool isNewObject = false;

			if (poolObject == null)
			{
				isNewObject = true;
				
				var loadObj = _loader.LoadObject<PoolObjectBase>(idCollection, idObject);

				if (ReferenceEquals(loadObj, null))
					throw new NullReferenceException();

				poolObject = isInject ?
					_container.InstantiatePrefabForComponent<PoolObjectBase>(loadObj, parent) :
					Object.Instantiate(loadObj, parent);
				
				pool.AddPoolObject(poolObject);
			}
			
			if (!warmUpObject)
				poolObject.Using();
			
			PreparationUILocked(idObject, poolObject.GetComponentFromGameObject<UIPoolObject>(), position, rotation, isNewObject, isRepeatedInject, isActive);
			
			return poolObject;
		}
		
		public T GetComponentFromUIPoolObjectBase<T>(string idCollection, string idObject, Transform parent, Vector2 position = default, Quaternion rotation = default,
			bool isInject = true, bool isRepeatedInject = false, bool isActive = true) where T : Object
		{
			IPoolObject poolObject = GetUIPoolObjectBase(idCollection, idObject, parent, position, rotation, isInject, isRepeatedInject, isActive);

			return poolObject.GetComponentFromGameObject<T>();
		}

		public ILockedMonoBehaviour GetLockedMonoBehaviourBase(string idCollection, string idObject, Vector3 position = default, Quaternion rotation = default,
			Transform parent = null, bool isInject = true, bool isRepeatedInject = false, bool isActive = true)
		{
			var loadObj = _loader.LoadObject<LockedMonoBehaviour>(idCollection, idObject);

			if (ReferenceEquals(loadObj, null))
				throw new NullReferenceException();

			ILockedMonoBehaviour lockedMonoBehaviour = isInject
				? _container.InstantiatePrefabForComponent<ILockedMonoBehaviour>(loadObj)
				: Object.Instantiate(loadObj, position, rotation, parent ?? _poolRoot);
			
			PreparationLockedMonoBehaviour(lockedMonoBehaviour, position, rotation, parent, true, isRepeatedInject, isActive);

			return lockedMonoBehaviour;
		}

		public T GetComponentFromLockedMonoBehaviourBase<T>(string idCollection, string idObject, Vector3 position = default, Quaternion rotation = default,
			Transform parent = null, bool isInject = true, bool isRepeatedInject = false, bool isActive = true) where T : Object
		{
			ILockedMonoBehaviour lockedMonoBehaviour = GetLockedMonoBehaviourBase(idCollection, idObject, position, rotation, parent,
				isInject, isRepeatedInject, isActive);

			return lockedMonoBehaviour.GetComponentFromGameObject<T>();
		}

		private void PreparationPoolObject(string idObject, PoolObjectBase poolObjectBase,
			Vector3 position = default, Quaternion rotation = default,
			Transform parent = null, bool isNewObject = true, bool isRepeatedInject = false, bool isActive = true)
		{
			PreparationLockedMonoBehaviour(poolObjectBase, position, rotation, parent, isNewObject, isRepeatedInject, isActive);
			poolObjectBase.SetID(idObject);
		}
		
		private void PreparationLockedMonoBehaviour(ILockedMonoBehaviour lockedMonoBehaviour, Vector3 position = default, Quaternion rotation = default,
			Transform parent = null, bool isNewObject = true, bool isRepeatedInject = false, bool isActive = true)
		{
			lockedMonoBehaviour.ChangeActive(isActive);

			if (isRepeatedInject && !isNewObject)
				_objectInjector.InjectGO(lockedMonoBehaviour.GetGameObject);
			
			if (!ReferenceEquals(parent, null))
				lockedMonoBehaviour.SetParent(parent);
			
			if (position != default)
				lockedMonoBehaviour.SetPosition(position);
			
			if (rotation != default)
				lockedMonoBehaviour.SetRotation(rotation);
		}

		private void PreparationUILocked(string idObject, UIPoolObject uiLocked, Vector2 position = default, Quaternion rotation = default, 
			bool isNewObject = true, bool isRepeatedInject = false, bool isActive = true)
		{
			PreparationPoolObject(idObject, uiLocked, default, rotation, null, isNewObject, isRepeatedInject, isActive);
			
			uiLocked.SetPosition(position);
		}
	}
}