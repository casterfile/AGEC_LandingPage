using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GrabDropScript : MonoBehaviour, InteractionListenerInterface
{
	int OIL = 1;
	public PuzzleManager PuzzleManager;
	public GameObject SelectedPiece;
    public bool movableObjectGrabbed = false;
	Ray moRay;
	public Transform moTransform;
    public LayerMask whatIsMovableObject;
    RaycastHit moHit;
	public LayerMask whatIsGround;
    public Transform ground;
    RaycastHit groundHit;
    RaycastHit mousePosHit;
    public float mousePosYOffsetFromGround = 0f;
    public Vector3 mouseRelToGround;
    [SerializeField]
    private Transform emptyRotator;
	[SerializeField]
	private Vector3 DefaultScale;
    private Vector3 PileScale;

	[SerializeField]
	private AttemptPlacementLocation lastAttemptLocation;

	
	public bool isPlaying;

	[Tooltip("List of the objects that may be dragged and dropped.")]
	public GameObject[] draggableObjects;
	
	[Tooltip("Drag speed of the selected object.")]
	public float dragSpeed = 3.0f;

	[Tooltip("Minimum Z-position of the dragged object, when moving forward and back.")]
	public float minZ = 0f;

	[Tooltip("Maximum Z-position of the dragged object, when moving forward and back.")]
	public float maxZ = 5f;

	// public options (used by the Options GUI)
	[Tooltip("Whether the objects obey gravity when released, or not. Used by the Options GUI-window.")]
	public bool useGravity = true;
	[Tooltip("Whether the objects should be put in their original positions. Used by the Options GUI-window.")]
	public bool resetObjects = false;

	[Tooltip("Camera used for screen ray-casting. This is usually the main camera.")]
	public Camera screenCamera;

	[Tooltip("UI-Text used to display information messages.")]
	public UnityEngine.UI.Text infoGuiText;

	[Tooltip("Interaction manager instance, used to detect hand interactions. If left empty, it will be the first interaction manager found in the scene.")]
	[SerializeField]
	private InteractionManager interactionManager;

    [Tooltip("Index of the player, tracked by the respective InteractionManager. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
    public int playerIndex = 0;

    [Tooltip("Whether the left hand interaction is allowed by the respective InteractionManager.")]
	public bool leftHandInteraction = true;

	[Tooltip("Whether the right hand interaction is allowed by the respective InteractionManager.")]
	public bool rightHandInteraction = true;


	// hand interaction variables
	//private bool isLeftHandDrag = false;
	private InteractionManager.HandEventType lastHandEvent = InteractionManager.HandEventType.None;

	// currently dragged object and its parameters
	private GameObject draggedObject;
	//private float draggedObjectDepth;
	// private Vector3 draggedObjectOffset;
	// private Material draggedObjectMaterial;
	private float draggedNormalZ;

	// initial objects' positions and rotations (used for resetting objects)
	private Vector3[] initialObjPos;
	private Quaternion[] initialObjRot;

	// normalized and pixel position of the cursor
	private Vector3 screenNormalPos = Vector3.zero;
	private Vector3 screenPixelPos = Vector3.zero;
	private Vector3 newObjectPos = Vector3.zero;


	// choose whether to use gravity or not
	public void SetUseGravity(bool bUseGravity)
	{
		this.useGravity = bUseGravity;
	}

	// request resetting of the draggable objects
	public void RequestObjectReset()
	{
		resetObjects = true;
	}

    public void Awake()
    {
		//PuzzleManager = FindObjectOfType<PuzzleManager>();
	}


    void Start()
	{
		// get the interaction manager instance
		if (interactionManager == null)
		{
			//interactionManager = InteractionManager.Instance;
			interactionManager = GetInteractionManager();
		}

		if (PuzzleManager == null)
		{
			PuzzleManager = FindObjectOfType<PuzzleManager>();
		}

        moHit = new RaycastHit();
        groundHit = new RaycastHit();

		isPlaying = true;
		
		// by default set the main-camera to be screen-camera
		if (screenCamera == null) 
		{
			screenCamera = Camera.main;
		}

		// save the initial positions and rotations of the objects
		initialObjPos = new Vector3[draggableObjects.Length];
		initialObjRot = new Quaternion[draggableObjects.Length];

		for(int i = 0; i < draggableObjects.Length; i++)
		{
			initialObjPos[i] = screenCamera ? screenCamera.transform.InverseTransformPoint(draggableObjects[i].transform.position) : draggableObjects[i].transform.position;
			initialObjRot[i] = screenCamera ? Quaternion.Inverse(screenCamera.transform.rotation) * draggableObjects[i].transform.rotation : draggableObjects[i].transform.rotation;
		}

		

		//Get the scale of the PuzzlePiece when in the Pile
		
	}


    // tries to locate a proper interaction manager in the scene
    private InteractionManager GetInteractionManager()
    {
        // find the proper interaction manager
        MonoBehaviour[] monoScripts = FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];

        foreach (MonoBehaviour monoScript in monoScripts)
        {
            if ((monoScript is InteractionManager) && monoScript.enabled)
            {
                InteractionManager manager = (InteractionManager)monoScript;

                if (manager.playerIndex == playerIndex && manager.leftHandInteraction == leftHandInteraction && manager.rightHandInteraction == rightHandInteraction)
                {
                    return manager;
                }
            }
        }

        // not found
        return null;
    }


    void Update() 
	{
		if (PuzzleManager == null)
        {
			PuzzleManager = FindObjectOfType<PuzzleManager>();
        }

		if (PuzzleManager != null)
		{
			if (PuzzleManager.PuzzleBoard != null)
			{
				if (PuzzleManager.PuzzleBoard.transform.childCount > 0)
				{
					if (DefaultScale == Vector3.zero)
					{
						DefaultScale = PuzzleManager.PuzzleBoard.transform.GetChild(0).localScale;
					}
				}
			}
		}
		// if(interactionManager != null && interactionManager.IsInteractionInited())
		// {
		// 	// if(resetObjects && draggedObject == null)
		// 	// {
		// 	// 	// reset the objects as needed
		// 	// 	resetObjects = false;
		// 	// 	ResetObjects ();
		// 	// }

		// 	if(draggedObject == null)
		// 	{
		// 		// no object is currently selected or dragged.
		// 		bool bHandIntAllowed = (leftHandInteraction && interactionManager.IsLeftHandPrimary()) || (rightHandInteraction && interactionManager.IsRightHandPrimary());

		// 		// check if there is an underlying object to be selected
		// 		if(lastHandEvent == InteractionManager.HandEventType.Grip && bHandIntAllowed)
		// 		{
		// 			// convert the normalized screen pos to pixel pos
		// screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

		// screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
		// screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));
		// Ray ray = screenCamera ? screenCamera.ScreenPointToRay(screenPixelPos) : new Ray();

		// moRay = screenCamera ? screenCamera.ScreenPointToRay(screenPixelPos) : new Ray();

		// 			// moRay = Camera.main.ScreenPointToRay(interactionManager.GetRightHandScreenPos());

		// 			// FindAndGrabMovableObject();

		// 			// check if there is an underlying objects
		// 			RaycastHit hit;
		// 			if(Physics.Raycast(ray, out hit))
		// 			{
		// 				foreach(GameObject obj in draggableObjects)
		// 				{
		// 					if(hit.collider.gameObject == obj)
		// 					{
		// 						if(hit.transform.CompareTag("Puzzle"))
		//     					{
		// 							if(!hit.transform.GetComponent<PieceScript>().InRightPosition)
		// 							{
		// 								SelectedPiece = hit.transform.gameObject;
		// 								SelectedPiece.GetComponent<PieceScript>().Selected = true;
		// 								SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
		// 								OIL++;

		// 								// an object was hit by the ray. select it and start drgging
		// 								draggedObject = obj;
		// 								// draggedObjectOffset = hit.point - draggedObject.transform.position;
		// 								// draggedObjectOffset.z = 0; // don't change z-pos

		// 								// draggedNormalZ = (minZ + screenNormalPos.z * (maxZ - minZ)) - 
		// 								// draggedObject.transform.position.z; // start from the initial hand-z

		// 								// stop using gravity while dragging object
		// 								draggedObject.GetComponent<Rigidbody>().useGravity = false;
		// 								break;
		// 							}
		// 						}

		// 					}
		// 				}
		// 			}
		// 		}

		// 	}
		// else
		// {
		// 	bool bHandIntAllowed = (leftHandInteraction && interactionManager.IsLeftHandPrimary()) || (rightHandInteraction && interactionManager.IsRightHandPrimary());

		// 	if (bHandIntAllowed) 
		// 	{
		// 		// continue dragging the object
		// 		screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

		// 		// convert the normalized screen pos to 3D-world pos
		// 		screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
		// 		screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));
		// 		//screenPixelPos.z = screenNormalPos.z + draggedObjectDepth;
		// 		screenPixelPos.z = (minZ + screenNormalPos.z * (maxZ - minZ)) - draggedNormalZ -
		// 			(screenCamera ? screenCamera.transform.position.z : 0f);

		// 		// newObjectPos = screenCamera.ScreenToWorldPoint(screenPixelPos) - draggedObjectOffset;
		// 		newObjectPos = screenCamera.ScreenToWorldPoint(screenPixelPos);
		// 		draggedObject.transform.position = Vector3.Lerp(draggedObject.transform.position, newObjectPos, dragSpeed * Time.deltaTime);

		// 		// check if the object (hand grip) was released
		// 		bool isReleased = lastHandEvent == InteractionManager.HandEventType.Release;

		// 		if(isReleased)
		// 		{
		// 			// restore the object's material and stop dragging the object
		// 			// draggedObject.GetComponent<Renderer>().material = draggedObjectMaterial;

		// 			if(useGravity)
		// 			{
		// 				// add gravity to the object
		// 				draggedObject.GetComponent<Rigidbody>().useGravity = true;
		// 			}

		// 			if(SelectedPiece != null)
		// 			{
		// 				SelectedPiece.GetComponent<PieceScript>().Selected = false;
		// 				SelectedPiece = null;
		// 			}

		// 			draggedObject = null;

		// 			// DropMovableObject();
		// 		}
		// 	}
		// }

		// }

		screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

		screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
		screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));
		moRay = screenCamera ? screenCamera.ScreenPointToRay(screenPixelPos) : new Ray();		

        if(lastHandEvent == InteractionManager.HandEventType.Grip && SelectedPiece == null)
        {
            FindAndGrabMovableObject();
        }

		//Added a verification for dropping to avoid infinite update : if there is a selected piece MARCK
        if(lastHandEvent == InteractionManager.HandEventType.Release && SelectedPiece != null)
        {
            DropMovableObject();
        }

		movableObjectGrabbed = moTransform != null;

        if(movableObjectGrabbed)
        {
            TraceMousePosRelativeToGround();
        }
	}


	void OnGUI()
	{
		if(infoGuiText != null && interactionManager != null && interactionManager.IsInteractionInited())
		{
			string sInfo = string.Empty;
			
			long userID = interactionManager.GetUserID();
			if(userID != 0)
			{
				if(draggedObject != null)
                {
                    sInfo = "Dragging the " + draggedObject.name + " around.";
                    CameraWebCam.cameraCheck = false;
                }
                else
                {
                    sInfo = "Please grab and drag an object around.";
                    CameraWebCam.cameraCheck = false;
                }
                    


            }
			else
			{
				KinectManager kinectManager = KinectManager.Instance;

				if(kinectManager && kinectManager.IsInitialized())
				{
					sInfo = "Waiting for Users...";
                    CameraWebCam.cameraCheck = true;
                    // print("camera check!");
				}
				else
				{
					sInfo = "Kinect is not initialized. Check the log for details.";
                    CameraWebCam.cameraCheck = true;
                    // print("camera check!");
                }
			}
			
			infoGuiText.text = sInfo;
		}
	}


	// reset positions and rotations of the objects
	private void ResetObjects()
	{
		for(int i = 0; i < draggableObjects.Length; i++)
		{
			draggableObjects[i].GetComponent<Rigidbody>().useGravity = false;
			draggableObjects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;

			draggableObjects[i].transform.position = screenCamera ? screenCamera.transform.TransformPoint(initialObjPos[i]) : initialObjPos[i];
			draggableObjects[i].transform.rotation = screenCamera ? screenCamera.transform.rotation * initialObjRot[i] : initialObjRot[i];
		}
	}


	public void HandGripDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
	{
		if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		lastHandEvent = InteractionManager.HandEventType.Grip;
		//isLeftHandDrag = !isRightHand;
		screenNormalPos = handScreenPos;
	}

	public void HandReleaseDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
	{
		if (!isHandInteracting || !interactionManager)
			return;
		if (userId != interactionManager.GetUserID())
			return;

		lastHandEvent = InteractionManager.HandEventType.Release;
		//isLeftHandDrag = !isRightHand;
		screenNormalPos = handScreenPos;
	}

	public bool HandClickDetected(long userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
	{
		return true;
	}
    private Vector3 velocity = Vector3.zero;

	void FindAndGrabMovableObject()
    {
        if(Physics.Raycast(
            moRay,
            out moHit,
            Mathf.Infinity,
            whatIsMovableObject))
        {
            if(moHit.transform.GetComponent<PieceScript>().enabled)
            {
                if(!moHit.transform.GetComponent<PieceScript>().InRightPosition)
                {
                    moTransform = moHit.transform;

                    FindGroundBelowMovableObject();

                    SelectedPiece = moHit.transform.gameObject;
                    //DefaultScale = SelectedPiece.transform.localScale;

                    SelectedPiece.transform.SetParent(SelectedPiece.GetComponent<PieceScript>().PuzzleManager.PuzzleBoard);
                    
					// Debug.Log(SelectedPiece.transform.parent);
					//Make the Puzzle Board the parent when a puzzle piece is selected
                    //moHit.transform.SetParent(moHit.transform.parent.parent);

                    SelectedPiece.GetComponent<PieceScript>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
            }
        }
    }

	void DropMovableObject()
    {
        if(moTransform != null)
            moTransform.GetComponent<Rigidbody>().isKinematic = false;
        moTransform = null;
        ground = null;

        if(SelectedPiece != null)
        {
			PieceScript piece = SelectedPiece.GetComponent<PieceScript>();
			SelectedPiece.GetComponent<PieceScript>().Selected = false;

			SelectedPiece.GetComponent<PieceScript>().CheckPuzzlePiece();

			SelectedPiece = null;

			PuzzleAttempt newAttempt = new PuzzleAttempt();
			PuzzleManager.PuzzleAttempts.Add(newAttempt.SetPuzzleAttempt(PuzzleManager.PuzzleAttempts.Count + 1, lastAttemptLocation, "Attemp Using Motion Tracking Camera", piece.GetLastDroppedPos(), PuzzleManager.GameTimer.TimeRemaining, piece.GetComponent<PieceScript>().InRightPosition));
		}
    }

	void TraceMousePosRelativeToGround()
    {
        if(Physics.Raycast(
            moRay,
            out mousePosHit,
            Mathf.Infinity,
            whatIsGround))
        {
			
            mouseRelToGround = mousePosHit.point;
            //SelectedPiece.transform.SetParent(SelectedPiece.transform.parent.parent);

            if (mousePosHit.transform.tag == "PuzzleBoard")
            {
                // SelectedPiece.transform.localScale = new Vector3(0.61f, 0.61f, 0.61f);

                SelectedPiece.transform.localScale = DefaultScale;
                //SelectedPiece.transform.localScale = PileScale;
                
                moTransform.position = new Vector3(
                    mouseRelToGround.x,
                    mouseRelToGround.y,
                    mouseRelToGround.z);

				lastAttemptLocation = AttemptPlacementLocation.Board;
				PuzzleManager.CurrentPlacementLocation = AttemptPlacementLocation.Board;
			}

            if(mousePosHit.transform.tag == "Table")
            {
                SelectedPiece.transform.localScale = new Vector3(1f, 1f, 1f);
                //SelectedPiece.transform.localScale = new Vector3 (PercentToDecimal(DefaultScale.x,PuzzleManager.PuzzleGameConfig.PpEnlargeFactorOnGrab), PercentToDecimal(DefaultScale.y,PuzzleManager.PuzzleGameConfig.PpEnlargeFactorOnGrab), PercentToDecimal(DefaultScale.z, PuzzleManager.PuzzleGameConfig.PpEnlargeFactorOnGrab));

                moTransform.position = new Vector3(
                    mouseRelToGround.x,
                    mouseRelToGround.y + 1f,
                    mouseRelToGround.z);

				lastAttemptLocation = AttemptPlacementLocation.Table;
				PuzzleManager.CurrentPlacementLocation = AttemptPlacementLocation.Table;
			}
          
		}
    }
	void FindGroundBelowMovableObject()
    {
        if(Physics.Raycast(
            moTransform.position,
            Vector3.down,
            out groundHit,
            Mathf.Infinity,
            whatIsGround))
        {
            ground = groundHit.transform;
        }
    }


	// public void ResetGame()
	// {
	// 	PieceScript[] pieceScripts = FindObjectsOfType<PieceScript>();

	// 	for(int i = 0; i < pieceScripts .Length; i++)
	// 	{
	// 		pieceScripts[i].reset();
	// 		pieceScripts[i].InRightPosition = false;
	// 	}
	// 	score = 0;
	// 	countdownTimer = FindObjectOfType<CountdownTimer>();

	// 	countdownTimer.currentTime = countdownTimer.startingTime;
	// 	countdownTimer.FinalScorePanel.SetActive(false);
	// 	isPlaying = true;
	// }
}
