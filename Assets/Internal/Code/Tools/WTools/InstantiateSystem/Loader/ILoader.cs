using UnityEngine;

namespace Tools.WTools
{
	public interface ILoader
	{
		public abstract T LoadObject<T>(string idCollection, string idObject) where T : Object;
	}
}