using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleGestureManager : MonoBehaviour
{
	public TMP_Text LastGestureInfoText;
    public bool slideChangeWithGestures = true;
    public bool slideChangeWithKeys = true;

	[SerializeField]
	private PuzzleManager PuzzleManager;
	[SerializeField]
	private GrabDropScript gDScript;
	[SerializeField]
	private SwipeGestureListener gestureListener;

	void Start()
	{
		PuzzleManager = FindObjectOfType<PuzzleManager>();
		gDScript = FindObjectOfType<GrabDropScript>();
		gestureListener = SwipeGestureListener.Instance;
	}

    public void Update()
    {
		if (slideChangeWithKeys)
		{
			if (Input.GetKeyDown(KeyCode.A) && gDScript.SelectedPiece != null)
			{
				RotateCounterClockwise(gDScript.SelectedPiece.transform);
			}
			else if (Input.GetKeyDown(KeyCode.D) && gDScript.SelectedPiece != null)
			{
				RotateClockwise(gDScript.SelectedPiece.transform);
			}
		}

		if (slideChangeWithGestures && gestureListener)
		{
			if (gestureListener.IsSwipeLeft() && gDScript.SelectedPiece != null)
			//if (gestureListener.IsSwipeLeft())
			{
				LastGestureInfoText.text = "SWIPED LEFT";
				RotateCounterClockwise(gDScript.SelectedPiece.transform);
			}
			else if (gestureListener.IsSwipeRight() && gDScript.SelectedPiece != null)
			// if (gestureListener.IsSwipeRight())
			{
				LastGestureInfoText.text = "SWIPED RIGHT";
				RotateClockwise(gDScript.SelectedPiece.transform);
			}
		}
	}

    void RotateCounterClockwise(Transform gameObj)
    {
        gameObj.transform.Rotate(0, 0, 90);

    }

    void RotateClockwise(Transform gameObj)
    {
        gameObj.transform.Rotate(0, 0, -90);
    }






}
