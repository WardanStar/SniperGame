using Tools.WTools;
using UnityEngine;

namespace Game.CameraStateMachine
{
	public class ReturnToStartPositionDataContainer : StateDataContainer<ICameraState>
	{
		public Vector3 StartPosition;
		public Vector3 StartRotation;
	}
}