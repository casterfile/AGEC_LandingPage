using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardPositionManager : MonoBehaviour
{
    public Transform Table;
    public Transform Board;
    public PuzzleManager PuzzleManager;
    public PositionLogs BoardPositionLogs;
    public PositionLogs PilePositionLogs;

    public void SetBoardPosition(ScreenPosition sPos)
    {
        if (BoardPositionLogs.BoardPositions.Count <= 0) return;

        if (PuzzleManager.PuzzleBoard != null)
        {
            Board = PuzzleManager.PuzzleBoard;
        }
        foreach (PositionLogs.BoardPosition bP in BoardPositionLogs.BoardPositions.ToList())
        {
            if(bP.ScreenBoardPosition == sPos)
            {
                Board.transform.position = bP.Position;
                Board.transform.rotation = bP.Rotation;
                Board.transform.localScale = bP.Scale;
            }
        }
    }

    public void SetPilePosition(ScreenPosition sPos)
    {
        if (PilePositionLogs.BoardPositions.Count <= 0) return;

        if (PuzzleManager.PuzzleBoard != null)
        {
            Board = PuzzleManager.PuzzleBoard;
        }
        foreach (PositionLogs.BoardPosition bP in PilePositionLogs.BoardPositions.ToList())
        {
            if (bP.ScreenBoardPosition == sPos)
            {
                if (bP.ScreenBoardPosition == ScreenPosition.TopMiddle || bP.ScreenBoardPosition == ScreenPosition.BottomMiddle || bP.ScreenBoardPosition == ScreenPosition.Center)
                {

                    foreach (Transform t in Board.GetComponent<PuzzleBoard>().PuzzlePieces.ToList())
                    {
                        t.GetComponent<PositionRandomizer>().IsRestrictedToParentPos = false;
                        t.GetComponent<PositionRandomizer>().XRange = new Vector2(-4, 4);
                        t.GetComponent<PositionRandomizer>().YRange = new Vector2(-0.1f, 0.2980002f);
                    }
                    //Set the pile to the alternate
                    Board.GetComponent<PuzzleBoard>().PileContainer = Board.GetComponent<PuzzleBoard>().AltPileContainer;
                }
                else if (bP.ScreenBoardPosition == ScreenPosition.Center)
                {

                    foreach (Transform t in Board.GetComponent<PuzzleBoard>().PuzzlePieces.ToList())
                    {
                        t.GetComponent<PositionRandomizer>().IsRestrictedToParentPos = false;
                        t.GetComponent<PositionRandomizer>().XRange = new Vector2(-4, 4);
                        t.GetComponent<PositionRandomizer>().YRange = new Vector2(-0.3770002f, 0.2980002f);
                    }
                    //Set the pile to the alternate
                    Board.GetComponent<PuzzleBoard>().PileContainer = Board.GetComponent<PuzzleBoard>().AltPileContainer;
                }
                else
                {

                    foreach (Transform t in Board.GetComponent<PuzzleBoard>().PuzzlePieces.ToList())
                    {
                        t.GetComponent<PositionRandomizer>().IsRestrictedToParentPos = true;
                        t.GetComponent<PositionRandomizer>().XRange = new Vector2(-0.5f, 0.5f);
                        t.GetComponent<PositionRandomizer>().YRange = new Vector2(-0.5f, 0.5f);
                    }

                    //Set the pile to default
                    Board.GetComponent<PuzzleBoard>().PileContainer = Board.GetComponent<PuzzleBoard>().DefPileContainer;
                }

                Board.GetComponent<PuzzleBoard>().PileContainer.transform.position = bP.Position;
                Board.GetComponent<PuzzleBoard>().PileContainer.transform.rotation = bP.Rotation;
                Board.GetComponent<PuzzleBoard>().PileContainer.localScale = bP.Scale;

              
            }
        }
    }


    public void Update()
    {
        if(PuzzleManager.PuzzleBoard != null)
        {
            if (Board == null)
            {
                Board = PuzzleManager.PuzzleBoard;
            }
        }
    }

}
