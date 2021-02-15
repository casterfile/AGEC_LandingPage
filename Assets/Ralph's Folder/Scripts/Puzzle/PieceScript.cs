using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(PositionRandomizer))]
public class PieceScript : MonoBehaviour
{
    public PuzzleManager PuzzleManager;
    private Vector3 PileDefaultScale;
    private Vector3 DefaultScale;
    private Vector3 RightPosition;
    [SerializeField]
    private Vector3 RightRotation;
    private Quaternion PileRotation;
    private Vector3 RandomPosition;
    private Vector3 PilePosition;
    public bool InRightPosition;
    public bool Selected;
    public bool Left = false, Right = false, Top = false, Bottom = false, Center = false;

    private PositionRandomizer posRandomizer;
    private Vector3 LastDroppedPos;

    void Start()
    {
        // KinnectGrab = FindObjectOfType<GrabDropScript>();
        posRandomizer = GetComponent<PositionRandomizer>();

        PuzzleManager = FindObjectOfType<PuzzleManager>();

        DefaultScale = transform.localScale;
        RightPosition = transform.position;
        RightRotation = transform.localEulerAngles;

        reset();
    }


    public void CheckPuzzlePiece()
    {
        if(Vector3.Distance(transform.position, RightPosition) < 0.65f && RightRotation == transform.localEulerAngles)
        {
            if(!Selected)
            {
                if(!InRightPosition)
                {
                    LastDroppedPos = transform.position;
                    transform.position = RightPosition;
                    InRightPosition = true;

                    GetComponent<SortingGroup>().sortingOrder = 0;

                    PuzzleManager.score += 100;
                    PuzzleManager.ScoreText.text = ("SCORE : " + PuzzleManager.score.ToString());
                    PuzzleManager.sVisualFeedback.ShowPositiveFeedback();
                    PuzzleManager.soundFx[0].Play();

                    PuzzleManager.InRightPositionPieces++;
                }
            }
        } 
        else {
            if(!Selected)
            {
                if(!InRightPosition){

                    LastDroppedPos = transform.position;
                    //Return the puzzle piece to the pile and set it as its parent
                    transform.SetParent(PuzzleManager.PuzzleBoard.GetComponent<PuzzleBoard>().PileContainer);

                    transform.localPosition = PilePosition;
                    
                    //Set to the default scale of the pile
                    transform.localScale = PileDefaultScale;

                    if (PuzzleManager.CurrentPlacementLocation == AttemptPlacementLocation.Board)
                    {

                        if (PuzzleManager.score >= 100)
                        {
                            PuzzleManager.score -= 100;
                        }

                        PuzzleManager.wrongAction++;

                        PuzzleManager.sVisualFeedback.ShowNegativeFeedback();
                        PuzzleManager.soundFx[1].Play();
                    }

                    PuzzleManager.ScoreText.text = ("SCORE : " + PuzzleManager.score.ToString());
                }
            }
        }
    }

    public Vector3 GetLastDroppedPos()
    {
        Vector3 pos = LastDroppedPos;
        return pos;
    }

    public void reset()
    {
        //RandomPosition = new Vector3(Random.Range(0.9f, 4.25f), 2.8f, Random.Range(-2.2f, -4.96f));

        if(PuzzleManager.PuzzleGameConfig.IsPpRotatable)
        {
            int ranRotateNum = Random.Range(1,3);

            switch(ranRotateNum)
            {
                case 1:
                    transform.Rotate(0, 0, 90);
                    break;
                case 2:
                    transform.Rotate(0, 0, -90);
                    break;
                case 3:
                    transform.Rotate(0, 0, -180);
                    break;
            }
        }

        transform.SetParent(PuzzleManager.PuzzleBoard.GetComponent<PuzzleBoard>().PileContainer);
        RandomPosition = posRandomizer.RandomizePosition(transform);
        PilePosition = transform.localPosition;
        PileRotation = transform.localRotation;
        PileDefaultScale = transform.localScale;
    }
}
