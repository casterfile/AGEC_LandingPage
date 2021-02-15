using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttemptPlacementLocation
{
    Board,
    Table,
    OutOfBounds
}

[System.Serializable]
public class PuzzleAttempt
{
    public int AttemptId;
    public AttemptPlacementLocation AttemptPlacementLocation;
    public string Remarks;
    public Vector3 AttemptEndPosition;
    public float AttemptTime;
    public bool IsAttemptSuccessful;

    public PuzzleAttempt SetPuzzleAttempt(int id, AttemptPlacementLocation loc, string remarks, Vector3 endPos, float time, bool isSuccessful)
    {
        PuzzleAttempt newPA = new PuzzleAttempt();
        newPA.AttemptId = id;
        newPA.AttemptPlacementLocation = loc;
        newPA.Remarks = remarks;
        newPA.AttemptEndPosition = endPos;
        newPA.AttemptTime = time;
        newPA.IsAttemptSuccessful = isSuccessful;

        return newPA;
    }
}
