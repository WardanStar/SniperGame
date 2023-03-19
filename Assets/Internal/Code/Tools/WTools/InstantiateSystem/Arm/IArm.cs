using UniRx;

namespace Tools.WTools
{
    public interface IArm
    {
        /// <summary>
        /// Returns a reactive property whose value indicates the readiness of the arm to work.
        /// </summary>
        public IReadOnlyReactiveProperty<bool> OnReady { get; }
        
        public PoolObjectGetter PoolObjectGetter { get; }
        public UIPoolObjectGetter UIPoolObjectGetter { get; }
        public ObjectGetter ObjectGetter { get; }
        
        /// <summary>
        /// Creating a new storage for the arm on the currently active scene.
        /// </summary>
        public void CreateNewRootFromScene();
        
        /// <summary>
        /// If there is no storage, then creates a new storage for the hand on the currently active scene.
        /// </summary>
        public void InitializeRoot();
        
        /// <summary>
        /// Returns all objects with the specified id in the storage.
        /// </summary>
        /// <param name="idObjects"></param>
        public void ReturnToPoolAllObjectsOnID(string idObjects);
        
        /// <summary>
        /// Returns to the storage all objects that are currently active.
        /// </summary>
        public void ReturnToPoolAllObjects();
    }
}