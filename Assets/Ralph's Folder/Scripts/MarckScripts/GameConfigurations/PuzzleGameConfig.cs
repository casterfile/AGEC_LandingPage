using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TENTATIVE ENUM [STRING] SIZES
public enum BoardSize
{
    Small,
    Medium,
    Large
}
public enum ScreenPosition
{
    Random,
    Left, //Dont use
    Right, //Dont use
    Center,
    TopLeft,
    TopRight,
    MiddleLeft,
    MiddleRight,
    BottomLeft,
    BottomRight,
    TopMiddle,
    BottomMiddle,
}

[CreateAssetMenu(fileName = "NewPuzzleConfig", menuName = "GameConfiguration/JigsawPuzzle")]
[System.Serializable]
public class PuzzleGameConfig : GameConfig
{
    [Space(7)]
    [Header("Information")]
    public string ConfigName;
    public string ConfigDescription;
    public bool isFreeConfig;
    [Space(7)]
    [Header("Board")]
    public BoardSize BoardSize; //Size of the Board in Enum
    public Vector2 BoardRange; //Size of the Board in Rows / Columns
    [Space(5)]
    public bool IsBoardPositionDynamic; //Is the Board Position Dynamic [Pile will always be opposite the board]
    public ScreenPosition BoardPosition; //Position of the board in Enum
    public ScreenPosition MissingPieceFocus; //Position where the majority of the puzzle pieces will be taken from the board
    public int MissingPieces; //Number of Puzzle Pieces that are taken out of the board

    [Space(7)]
    [Header("Puzzle Piece")]
    public bool IsPpHighlightOnGrab; //Does the puzzle piece highlight when selected
    public bool IsPpRotatable; //Can the puzzle piece be rotated
    public float PpEnlargeFactorOnGrab; //How many percent does the puzzle piece enlarge when selected

}

