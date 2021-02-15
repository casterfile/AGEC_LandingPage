using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public enum PrimaryHand
{
	Undefined,
	Left,
	Right
}

public class PrimaryHandManager : MonoBehaviour 
{
	[Tooltip("Camera used for screen-to-world calculations. This is usually the main camera.")]
	public Camera screenCamera;

	public PrimaryHand PrimaryHand;
	public Image HandIcon;
	public Sprite HandSprite;
	public TMP_Text HandInfo;
	public bool IsDoneCalibration = false;

	// reference to the gesture listener
	private RaisehandGestureListener gestureListener;
	private InteractionManager interactionManager;


	void Start() 
	{
		// hide mouse cursor
		//Cursor.visible = false;
		
		// by default set the main-camera to be screen-camera
		if (screenCamera == null) 
		{
			screenCamera = Camera.main;
		}

		// get the gestures listener
		gestureListener = RaisehandGestureListener.Instance;
		if(interactionManager == null)
        {
			interactionManager = FindObjectOfType<InteractionManager>();
        }

	}
	
	void Update() 
	{
		// dont run Update() if there is no gesture listener
		if(!gestureListener || !interactionManager)
			return;

		if (PrimaryHand == PrimaryHand.Undefined)
		{
			if (gestureListener.IsRaiseLeftHand())
			{
				PrimaryHand = PrimaryHand.Left;
				interactionManager.leftHandInteraction = true;
				interactionManager.rightHandInteraction = false;
				interactionManager.isLeftHandSelectedAsPrimary = true;
				interactionManager.isRightHandSelectedAsPrimary = false;
				interactionManager.SetLeftHandPrimary();
				if (HandIcon != null)
				{
					HandIcon.gameObject.SetActive(true);
					HandIcon.sprite = HandSprite;
					HandIcon.transform.Rotate(0, 180, 0);
				}

				if(HandInfo != null)
                {
					HandInfo.gameObject.SetActive(true);
					HandInfo.text = "LEFT HAND DETECTED";
                }
				IsDoneCalibration = true;
			}
			else if (gestureListener.IsRaiseRightHand())
			{
				PrimaryHand = PrimaryHand.Right;
				interactionManager.leftHandInteraction = false;
				interactionManager.rightHandInteraction = true;
				interactionManager.isLeftHandSelectedAsPrimary = false;
				interactionManager.isRightHandSelectedAsPrimary = true;
				interactionManager.SetRightHandPrimary();
				if (HandIcon != null)
				{
					HandIcon.gameObject.SetActive(true);
					HandIcon.sprite = HandSprite;
					HandIcon.transform.Rotate(0, 0, 0);
				}

				if(HandInfo != null)
                {
					HandInfo.gameObject.SetActive(true);
					HandInfo.text = "RIGHT HAND DETECTED";
				}
				IsDoneCalibration = true;
			}
		}

		/*
		if (PrimaryHand == PrimaryHand.Left)
		{
			interactionManager.SetLeftHandPrimary();
		}
		else if (PrimaryHand == PrimaryHand.Right)
		{
			interactionManager.SetRightHandPrimary();
		}
		*/

		
	}
	public void EndCalibration()
	{
		IsDoneCalibration = true;
	}
}
