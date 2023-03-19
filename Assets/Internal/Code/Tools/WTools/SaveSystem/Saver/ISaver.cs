using System;

namespace Tools.WTools.Saver
{
    public interface ISaver
    {
        /// <summary>
        /// The event fires before saving the keys.
        /// </summary>
        public event Action OnSave;
        
        /// <summary>
        /// Saving keys.
        /// </summary>
        public void SaveData();
    }
}