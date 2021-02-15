using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ClickMoveDropScript : MonoBehaviour
{
    public PuzzleManager PuzzleManager;
    private Vector3 DefaultScale;
    private Vector3 PileScale;

    public GameObject SelectedPiece;
    int OIL = 1;
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
    private AttemptPlacementLocation lastAttemptLocation;

    // Start is called before the first frame update
    void Start()
    {
        

        moHit = new RaycastHit();
        groundHit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        if (PuzzleManager == null)
        {
            PuzzleManager = FindObjectOfType<PuzzleManager>();
        }

        moRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            FindAndGrabMovableObject();
        }
        if(Input.GetMouseButtonUp(0))
        {
            DropMovableObject();
        }

        movableObjectGrabbed = moTransform != null;

        if(movableObjectGrabbed)
        {
            TraceMousePosRelativeToGround();
        }

        if(PuzzleManager.PuzzleGameConfig.IsPpRotatable)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                RotateCounterClockwise();
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                RotateClockwise();
            }
        }
    }

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
                    SelectedPiece.transform.SetParent(SelectedPiece.transform.parent.parent);
                    //Make the Puzzle Board the parent when a puzzle piece is selected
                    //moHit.transform.SetParent(moHit.transform.parent.parent);

                    //Get the scale of the PuzzlePiece when in the Pile
                    DefaultScale = SelectedPiece.transform.localScale;

                    SelectedPiece.GetComponent<PieceScript>().Selected = true;
                    SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                    OIL++;
                }
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

    private Vector3 scaleBeforeEnlargement;
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

    public float PercentToDecimal(float value, float percentageToGet)
    {
        float result = 0;

        float decPerc = percentageToGet / 100;
        float dec = decPerc * value;
        result = value - dec;

        return value;
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
            PuzzleManager.PuzzleAttempts.Add(newAttempt.SetPuzzleAttempt(PuzzleManager.PuzzleAttempts.Count + 1, lastAttemptLocation, "Attemp Using Mouse / Keyboard", piece.GetLastDroppedPos(), PuzzleManager.GameTimer.TimeRemaining, piece.GetComponent<PieceScript>().InRightPosition));
        }
    }

    Coroutine rotation;
    void RotateCounterClockwise()
    {
        /*
        if(SelectedPiece != null)
        {
            if(SelectedPiece.transform.localEulerAngles.x >= 0 && SelectedPiece.transform.localEulerAngles.y >= 0)
            {
                SelectedPiece.transform.Rotate(0, 0, 90);
            }
            else if (SelectedPiece.transform.localEulerAngles.x < 0 && SelectedPiece.transform.localEulerAngles.y >= 0)
            {
                SelectedPiece.transform.Rotate(0, 0, -90);
            }
            else if (SelectedPiece.transform.localEulerAngles.x < 0 && SelectedPiece.transform.localEulerAngles.y < 0)
            {
                SelectedPiece.transform.Rotate(0, 0, -90);
            }
        }
        */
        SelectedPiece.transform.Rotate(0, 0, 90);
        //rotation = StartCoroutine(IRotateCounterClockwise());

    }

    void RotateClockwise()
    {
        /*
        if (SelectedPiece != null)
        {
            if (SelectedPiece.transform.localEulerAngles.x >= 0 && SelectedPiece.transform.localEulerAngles.y >= 0)
            {
                SelectedPiece.transform.Rotate(0, 0, -90);
            }
            else if (SelectedPiece.transform.localEulerAngles.x < 0 && SelectedPiece.transform.localEulerAngles.y >= 0)
            {
                SelectedPiece.transform.Rotate(0, 0, 90);
            }
            else if (SelectedPiece.transform.localEulerAngles.x < 0 && SelectedPiece.transform.localEulerAngles.y < 0)
            {
                SelectedPiece.transform.Rotate(0, 0, 90);
            }
        }
        */

        SelectedPiece.transform.Rotate(0, 0, -90);
        //rotation = StartCoroutine(IRotateClockwise());
    }

    /*
    IEnumerator IRotateCounterClockwise()
    {
        if(rotation != null) { StopCoroutine(rotation); }
        if (SelectedPiece != null)
        {
            SelectedPiece.transform.SetParent(emptyRotator);
            Debug.Log("Current Parent : " + transform.parent);
            SelectedPiece.transform.Rotate(0, 0, 90);
            SelectedPiece.transform.SetParent(SelectedPiece.GetComponent<PieceScript>().PuzzleManager.PuzzleBoard.GetComponent<PuzzleBoard>().PileContainer);
        }
        rotation = null;
        yield break;
    }

    

    IEnumerator IRotateClockwise()
    {
        if (rotation != null) { StopCoroutine(rotation); }
        if (SelectedPiece != null)
        {
            SelectedPiece.transform.SetParent(emptyRotator);
            SelectedPiece.transform.Rotate(0, 0, -90);
            SelectedPiece.transform.SetParent(SelectedPiece.GetComponent<PieceScript>().PuzzleManager.PuzzleBoard.GetComponent<PuzzleBoard>().PileContainer);
        }
        rotation = null;
        yield break;
    }
    */
}
