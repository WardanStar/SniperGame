using UnityEngine;

namespace Tools.WTools
{
    public interface ILoader
    {
        /// <summary>
        /// Returns an object of the specified type, from resources, at the specified path.
        /// </summary>
        /// <param name="idCollection"></param>
        /// <param name="idObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T LoadObject<T>(string idCollection, string idObject) where T : Object;
    }
}