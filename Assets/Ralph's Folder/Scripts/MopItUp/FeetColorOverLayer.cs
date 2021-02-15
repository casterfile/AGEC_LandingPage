using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using Windows.Kinect;


public class FeetColorOverLayer : MonoBehaviour 
{
	public int playerIndex = 0;
	public GameObject leftFootOverlay;
	public GameObject rightFootOverlay;
	public bool mirroredMovement = false;
	private KinectManager manager;
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	

	void Start()
	{
		leftFootOverlay.SetActive(true);
		rightFootOverlay.SetActive(true);

		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

	void Update () 
	{
		if(manager == null)
		{
			manager = KinectManager.Instance;
		}

		if(manager && manager.IsInitialized())
		{

			// overlay the joints
			if(manager.IsUserDetected(playerIndex))
			{
				long userId = manager.GetUserIdByIndex(playerIndex);

				OverlayJoint(userId, (int)KinectInterop.JointType.FootLeft, leftFootOverlay.transform);
				OverlayJoint(userId, (int)KinectInterop.JointType.FootRight, rightFootOverlay.transform);
			}
			
		}
	}


	// overlays the given object over the given user joint
	private void OverlayJoint(long userId, int jointIndex, Transform overlayObj)
	{
		if(manager.IsJointTracked(userId, jointIndex))
		{
			Vector3 posJoint = manager.GetJointPosition(userId, jointIndex);
			posJoint.z = !mirroredMovement ? -posJoint.z : posJoint.z;
		

			if(mirroredMovement)
			{
				posJoint.x = -posJoint.x;
				posJoint.z = -posJoint.z;
			}

			posJoint = new Vector3(posJoint.x, overlayObj.transform.position.y, posJoint.z);

			overlayObj.transform.localPosition = posJoint;
			
		}
	}
}
