using Tools.WTools;
using UnityEngine;

namespace Game.CameraStateMachine
{
	public class LookAtBulletDataContainer : StateDataContainer<ICameraState>
	{
		public Transform BulletTransform;
	}
}