using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    
    public AudioSource[] soundFx;
    public GameObject[] Puzzles;
    public PuzzleGameConfig PuzzleGameConfig;
    public Transform PuzzleBoard;
    //public Text ScoreText;
    public TMP_Text ScoreText;
    public int score;
    public int wrongAction;
    public int InRightPositionPieces = 0;
    public AttemptPlacementLocation CurrentPlacementLocation;
    private PuzzleMenu PuzzleMenu;
    private List<int> uniqueNumbers;
    private List<int> finishedList;
    private List<PieceScript> Pieces;
    private List<PieceScript> ChosenPieces;
    private int NumOfPieces = 0;

    [SerializeField]
    private BoardPositionManager boardPosManager;
    [SerializeField]
    public GenericTimer GameTimer;

    public ScreenVisualFeedback sVisualFeedback;

    public List<PuzzleAttempt> PuzzleAttempts = new List<PuzzleAttempt>();


    private void Awake()
    {
        GameTimer.SetTimer(Timer.Countdown, PuzzleGameConfig.TimeLimit);
    }

    void Start()
    {
        //GetBoardSize();
        GetBoardSizeWithTotalPieces(Mathf.RoundToInt(PuzzleGameConfig.BoardRange.x), Mathf.RoundToInt(PuzzleGameConfig.BoardRange.y));

        uniqueNumbers = new List<int>();
        finishedList = new List<int>();

        PuzzleMenu = FindObjectOfType<PuzzleMenu>();

        Pieces = new List<PieceScript>();
        ChosenPieces = new List<PieceScript>();

        Pieces.AddRange(PieceScript.FindObjectsOfType<PieceScript>());

        for(int i = 0; i < Pieces.Count; i++)
        {
            Pieces[i].enabled = false;
        }

        GetPuzzlePieces();
        GenerateRandomList();
        EnableScripts();
        boardPosManager.SetBoardPosition(PuzzleGameConfig.BoardPosition);
        boardPosManager.SetPilePosition(PuzzleGameConfig.BoardPosition);

        if (GameTimer != null)
        {
            StartCoroutine(IStartTimer(0.5f));
        }
    }

    void Update()
    {
        if(InRightPositionPieces == PuzzleGameConfig.MissingPieces || GameTimer.GetTimerStatus())
        {
            OnPuzzleComplete();
        }
    }

    public void GetBoardSize()
    {
        switch(PuzzleGameConfig.BoardSize)
        {
            case BoardSize.Small:
                Puzzles[0].SetActive(true);
                PuzzleBoard = Puzzles[0].transform;
                break;
            
            case BoardSize.Medium:
                Puzzles[1].SetActive(true);
                PuzzleBoard = Puzzles[1].transform;
                break;

            case BoardSize.Large:
                Puzzles[2].SetActive(true);
                PuzzleBoard = Puzzles[2].transform;
                break;
        }
    }
    
    public void GetBoardSizeWithTotalPieces(int x, int y)
    {
        int prod = Mathf.RoundToInt(x * y);
        switch(prod)
        {
            case 12:
                Puzzles[0].SetActive(true);
                PuzzleBoard = Puzzles[0].transform;
                break;
            
            case 20:
                Puzzles[1].SetActive(true);
                PuzzleBoard = Puzzles[1].transform;
                break;

            case 30:
                Puzzles[2].SetActive(true);
                PuzzleBoard = Puzzles[2].transform;
                break;
            default:
                Puzzles[0].SetActive(true);
                PuzzleBoard = Puzzles[0].transform;
                break;
        }
    }

    public void GetPuzzlePieces()
    {
        

        switch(PuzzleGameConfig.MissingPieceFocus)
        {
            case ScreenPosition.Random:
                for(int i = 0; i < Pieces.Count; i++){
                    ChosenPieces.Add(Pieces[i]);
                }
                break;

            case ScreenPosition.TopMiddle:
                for(int i = 0; i < Pieces.Count; i++){
                    if(Pieces[i].Top)
                    {
                        ChosenPieces.Add(Pieces[i]);
                    }
                }
                break;

            case ScreenPosition.BottomMiddle:
                for(int i = 0; i < Pieces.Count; i++){
                    if(Pieces[i].Bottom)
                    {
                        ChosenPieces.Add(Pieces[i]);
                    }
                }
                break;

            case ScreenPosition.MiddleLeft:
                for(int i = 0; i < Pieces.Count; i++){
                    if(Pieces[i].Left)
                    {
                        ChosenPieces.Add(Pieces[i]);
                    }
                }
                break;

            case ScreenPosition.MiddleRight:
                for(int i = 0; i < Pieces.Count; i++){
                    if(Pieces[i].Right)
                    {
                        ChosenPieces.Add(Pieces[i]);
                    }
                }
                break;

            case ScreenPosition.Center:
                for(int i = 0; i < Pieces.Count; i++){
                    if(Pieces[i].Center)
                    {
                        ChosenPieces.Add(Pieces[i]);
                    }
                }
                break;
        }
    }

    public void GenerateRandomList(){

        for(int i = 0; i < ChosenPieces.Count; i++){
            uniqueNumbers.Add(i);
        }

        for(int i = 0; i< ChosenPieces.Count; i ++)
        {
            int ranNum = uniqueNumbers[Random.Range(0,uniqueNumbers.Count)];
            finishedList.Add(ranNum);
            uniqueNumbers.Remove(ranNum);
        }
    }

    public void EnableScripts()
    {
        for(int i = 0; i < PuzzleGameConfig.MissingPieces; i++){
            ChosenPieces[finishedList[i]].enabled = true;
        }
    }

    public IEnumerator IStartTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameTimer.SetTimer(Timer.Countdown, PuzzleGameConfig.TimeLimit);
        GameTimer.RestartTimer();
        yield break;
    }

    //Do here whatever happens when the Puzzle is already Completed
    public void OnPuzzleComplete()
    {
        GameTimer.StopTimer();
        PuzzleMenu.GameOverPanel.SetActive(true);
        PuzzleMenu.scoreText.text = ("Score : " + score.ToString());
        PuzzleMenu.wrongText.text = ("Wrong Actions: " + wrongAction.ToString());

        PuzzleMenu.UI.SetActive(false);
    }

}
