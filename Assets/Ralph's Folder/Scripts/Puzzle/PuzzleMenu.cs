using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMenu : MonoBehaviour
{
    public Sprite[] PuzzleBackgrounds;
    public Sprite SelectedPuzzleBackground;
    public GameObject UI;
    public GameObject ConfigPanel;
    public GameObject ChoosePanel;
    public GameObject GameOverPanel;
    public GameObject PuzzleManager;
    public PuzzleGameConfig[] PuzzleGameConfig;

    public int btnSelected = 0;
    public Dropdown boardSizeDrop;
    public Dropdown timeLimitDrop;
    public Dropdown missingPieceDrop;
    public Dropdown missingPiecePositionDrop;
    public Dropdown boardPositionDrop;
    public Toggle RotationTgl;

    public Text scoreText;
    public Text wrongText;

    public void Puzzle1Btn()
    {
        SelectedPuzzleBackground = PuzzleBackgrounds[0];
        NextPanel();
    }

    public void Puzzle2Btn()
    {
        SelectedPuzzleBackground = PuzzleBackgrounds[1];
        NextPanel();
    }

    public void Puzzle3Btn()
    {
        SelectedPuzzleBackground = PuzzleBackgrounds[2];
        NextPanel();
    }

    public void EasyBtn()
    {
        btnSelected = 0;

        boardSizeDrop.value = 0;
        timeLimitDrop.value = 0;
        missingPieceDrop.value = 0;
        missingPiecePositionDrop.value = 0;
        boardPositionDrop.value = 3;
        RotationTgl.isOn = false;
        onDificultyChange();
    }


    public void MediumBtn()
    {
        btnSelected = 1;

        boardSizeDrop.value = 1;
        timeLimitDrop.value = 1;
        missingPieceDrop.value = 1;
        missingPiecePositionDrop.value = 0;
        boardPositionDrop.value = 3;
        RotationTgl.isOn = false;
        onDificultyChange();
    }

    public void HardBtn()
    {
        btnSelected = 2;

        boardSizeDrop.value = 2;
        timeLimitDrop.value = 2;
        missingPieceDrop.value = 2;
        missingPiecePositionDrop.value = 0;
        boardPositionDrop.value = 3;
        RotationTgl.isOn = false;
        onDificultyChange();
    }

    public void NextPanel()
    {
        ChoosePanel.SetActive(false);
        ConfigPanel.SetActive(true);
    }

    public void StartBtn()
    {
        PuzzleManager.GetComponent<PuzzleManager>().PuzzleGameConfig = PuzzleGameConfig[btnSelected];

        ConfigPanel.SetActive(false);
        PuzzleManager.SetActive(true);
        UI.SetActive(true);
    }

    public void onDificultyChange()
    {
        onRotationTglChanged();
        onBoardSizeDropChange();
        ontimeLimitDropChange();
        onMissingPieceDropChange();
        onBoardPositionDropChange();
        onMissingPiecePositionDropChange();
    }

    public void onRotationTglChanged()
    {
        PuzzleGameConfig[btnSelected].IsPpRotatable = RotationTgl.isOn;
    }

    public void onBoardSizeDropChange()
    {
        switch(boardSizeDrop.value)
        {
            case 0:
                PuzzleGameConfig[btnSelected].BoardSize = BoardSize.Small;
                break;
            case 1:
                PuzzleGameConfig[btnSelected].BoardSize = BoardSize.Medium;
                break;
            case 2:
                PuzzleGameConfig[btnSelected].BoardSize = BoardSize.Large;
                break;
        }
    }

    public void ontimeLimitDropChange()
    {
        switch(timeLimitDrop.value)
        {
            case 0:
                PuzzleGameConfig[btnSelected].TimeLimit = 60;
                break;
            case 1:
                PuzzleGameConfig[btnSelected].TimeLimit = 100;
                break;
            case 2:
                PuzzleGameConfig[btnSelected].TimeLimit = 120;
                break;
        }
    }

    public void onMissingPieceDropChange()
    {
        switch(missingPieceDrop.value)
        {
            case 0:
                PuzzleGameConfig[btnSelected].MissingPieces = 6;
                break;
            case 1:
                PuzzleGameConfig[btnSelected].MissingPieces = 10;
                break;
            case 2:
                PuzzleGameConfig[btnSelected].MissingPieces = 15;
                break;
        }
    }

    public void onBoardPositionDropChange()
    {
        switch(boardPositionDrop.value)
        {
            case 0:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.Center;
                break;
            case 1:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.TopLeft;
                break;
            case 2:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.TopRight;
                break;
            case 3:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.MiddleLeft;
                break;
            case 4:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.MiddleRight;
                break;
            case 5:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.BottomLeft;
                break;
            case 6:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.BottomRight;
                break;
            case 7:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.TopMiddle;
                break;
            case 8:
                PuzzleGameConfig[btnSelected].BoardPosition = ScreenPosition.BottomMiddle;
                break;
        }
    }

    public void onMissingPiecePositionDropChange()
    {
        switch(missingPiecePositionDrop.value)
        {
            case 0:
                PuzzleGameConfig[btnSelected].MissingPieceFocus = ScreenPosition.Random;
                break;
            case 1:
                PuzzleGameConfig[btnSelected].MissingPieceFocus = ScreenPosition.Center;
                break;
            case 2:
                PuzzleGameConfig[btnSelected].MissingPieceFocus = ScreenPosition.TopMiddle;
                break;
            case 3:
                PuzzleGameConfig[btnSelected].MissingPieceFocus = ScreenPosition.MiddleRight;
                break;
            case 4:
                PuzzleGameConfig[btnSelected].MissingPieceFocus = ScreenPosition.MiddleLeft;
                break;
            case 5:
                PuzzleGameConfig[btnSelected].MissingPieceFocus = ScreenPosition.BottomMiddle;
                break;
        }
    }
}
