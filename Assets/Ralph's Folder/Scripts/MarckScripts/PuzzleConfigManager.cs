using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class PuzzleConfigManager : MonoBehaviour
{
    public PrimaryHandManager PHandManager;
    public PuzzleGameConfig ConfigContainer;
    public bool isConfigStatic;
    public TextScroller TSDifficulty;
    public TextScroller TSTimeLimit;
    public TextScroller TSMissingPieces;
    public TextScroller TSBoardPos;
    public TextScroller TSFocusArea;
    public TextScroller TSRotation;

    public List<PuzzleGameConfig> GameConfigTemplates = new List<PuzzleGameConfig>();

    //HARD CODED FOR NOW
    public Vector2 SelectedBoardRange;
    public List<Button> BoardChoices = new List<Button>();
    public Color SelectedBoardColor = new Color(255,255,255,255);
    public Color DefaultBoardColor = new Color(255,255,255,255);

    public Transform THandConfigWindow;
    public Transform TPuzzleManager;
    public Transform TPuzzleSelection;
    public Transform TConfigWindow;
    public Transform TGameUI;

    [Header("Hand Settings")]
    public Image HandIcon;
    public Sprite HandSprite;

    private bool isDoneCalibrating;


    public void Awake()
    {
        PopulateGameConfigTS();
    }

    public void Start()
    {
        LoadGameConfig(GameConfigTemplates[TSDifficulty.GetCurrentIndex()]);
        LoadCustomConfigChecker();
    }
    public void PopulateGameConfigTS()
    {
        TSDifficulty.Entrees.Clear();
        if (GameConfigTemplates.Count > 0)
        {
            foreach(PuzzleGameConfig config in GameConfigTemplates.ToList())
            {
                TSDifficulty.Entrees.Add(config.ConfigName);
            }
        }

        //POPULATE THE SCROLL ENTREES OF BOARD POSITION AND FOCUS AREA
        ScreenPosition[] ScreenPosArray = (ScreenPosition[])Enum.GetValues(typeof(ScreenPosition));
        if (ScreenPosArray.Length > 0)
        {
            for(int i = 0; i < ScreenPosArray.Length; i++)
            {
                TSBoardPos.Entrees.Add(ScreenPosArray[i].ToString());
            }
            
            for(int i = 0; i < ScreenPosArray.Length; i++)
            {
                TSFocusArea.Entrees.Add(ScreenPosArray[i].ToString());
            }
        }


        //ADD ON AND OFF TO ROTATION
        if(TSRotation.Entrees.Count != 2) 
        {
            TSRotation.Entrees.Clear();
            TSRotation.Entrees.Add("OFF");
            TSRotation.Entrees.Add("ON");
        }
    }

    public void LoadCustomConfigChecker()
    {
        foreach(Button b in TSTimeLimit.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }
        
        foreach(Button b in TSMissingPieces.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }
        
        foreach(Button b in TSBoardPos.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }  
        
        foreach(Button b in TSFocusArea.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }
    }

    public void LoadGameConfig(PuzzleGameConfig puzzleGameConfig)
    {
        TSTimeLimit.SetIntegralValues(0, 1000, Mathf.RoundToInt((int)puzzleGameConfig.TimeLimit));
        TSMissingPieces.SetIntegralValues(1, Mathf.RoundToInt(puzzleGameConfig.BoardRange.x * puzzleGameConfig.BoardRange.y), Mathf.RoundToInt((int)puzzleGameConfig.MissingPieces));
        TSBoardPos.UpdateScrollLabel((int)puzzleGameConfig.BoardPosition);
        TSFocusArea.UpdateScrollLabel((int)puzzleGameConfig.MissingPieceFocus);
        if (!puzzleGameConfig.IsPpRotatable) 
        {
            TSRotation.UpdateScrollLabel(0);
        }
        else if (puzzleGameConfig.IsPpRotatable) 
        {
            TSRotation.UpdateScrollLabel(1);
        }

        if (isConfigStatic)
        {
            TSTimeLimit.ToggleButtons(puzzleGameConfig.isFreeConfig);
            TSMissingPieces.ToggleButtons(puzzleGameConfig.isFreeConfig);
        }

        //HARD CODED FOR PUZZLE SIZE
        LoadSelectedBoard(Mathf.RoundToInt(puzzleGameConfig.BoardRange.x), Mathf.RoundToInt(puzzleGameConfig.BoardRange.y));
    }

    public void CheckIfCustomConfig()
    {
        IsCustomConfig(GameConfigTemplates[TSDifficulty.GetCurrentIndex()]);
    }

    public bool IsCustomConfig(PuzzleGameConfig pGameConfig)
    {
        if(TSTimeLimit.GetCurrentIntegralValue() != Mathf.RoundToInt((int)pGameConfig.TimeLimit) 
            || TSMissingPieces.GetCurrentIntegralValue() != Mathf.RoundToInt((int)pGameConfig.MissingPieces)
            || SelectedBoardRange != pGameConfig.BoardRange)
        {
            TSDifficulty.UpdateScrollLabel(3);
            TSMissingPieces.MaxValue = Mathf.RoundToInt(SelectedBoardRange.x * SelectedBoardRange.y);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadSelectedBoard(int x, int y)
    {
        int totalPieces = x * y;
        if (BoardChoices.Count < 3) return;

        foreach (Button b in BoardChoices)
        {
            b.image.color = DefaultBoardColor;
        }

        if (totalPieces <= 12)
        {
            BoardChoices[0].image.color = SelectedBoardColor;
        }
        else if(totalPieces == 20)
        {
            BoardChoices[1].image.color = SelectedBoardColor;
        }
        else if(totalPieces >= 30)
        {
            BoardChoices[2].image.color = SelectedBoardColor;
        }

        SelectedBoardRange = new Vector2(x, y);
    }

    public void SelectBoard(int index)
    {
        foreach (Button b in BoardChoices)
        {
            b.image.color = DefaultBoardColor;
        }

        if (index == 0)
        {
            SelectedBoardRange = new Vector2(4, 3);
        }
        else if (index == 1)
        {
            SelectedBoardRange = new Vector2(5, 4);
        }
        else if (index == 2)
        {
            SelectedBoardRange = new Vector2(6, 5);
        }

        BoardChoices[index].image.color = SelectedBoardColor;
    }
    
    public void UpdateGameConfig()
    {
        if (GameConfigTemplates.Count <= 0) return;
        LoadGameConfig(GameConfigTemplates[TSDifficulty.GetCurrentIndex()]);
    }

    public void OnClickStartGame()
    {
        StartCoroutine(IOnClickStartGame());
    }

    public IEnumerator IOnClickStartGame()
    {
        LoadConfigToContainer();

        TConfigWindow.gameObject.SetActive(false);
        TPuzzleManager.GetComponent<PuzzleManager>().PuzzleGameConfig = ConfigContainer;
        TGameUI.gameObject.SetActive(true);
        TPuzzleManager.gameObject.SetActive(true);
        yield break;
    }

    public void LoadConfigToContainer()
    {
        ConfigContainer.TimeLimit = int.Parse(TSTimeLimit.Label.text);
        ConfigContainer.MissingPieces = int.Parse(TSMissingPieces.Label.text);
        ConfigContainer.BoardRange = SelectedBoardRange;
        if (ConfigContainer.MissingPieces > (ConfigContainer.BoardRange.x * ConfigContainer.BoardRange.y))
        {
            ConfigContainer.MissingPieces = Mathf.RoundToInt(ConfigContainer.BoardRange.x * ConfigContainer.BoardRange.y);
        }
        ConfigContainer.MissingPieceFocus = (ScreenPosition)TSMissingPieces.GetCurrentIndex();
        ConfigContainer.BoardPosition = (ScreenPosition)TSBoardPos.GetCurrentIndex();
        if (TSRotation.GetCurrentIndex() == 0)
        {
            ConfigContainer.IsPpRotatable = false;
        }
        else if (TSRotation.GetCurrentIndex() == 1)
        {
            ConfigContainer.IsPpRotatable = true;
        }
    }

    public void Update()
    {
        if (PHandManager.IsDoneCalibration && !isDoneCalibrating)
        {
            StartCoroutine(WaitForPrimaryHandCalib());
        }
    }

    public IEnumerator WaitForPrimaryHandCalib()
    {
        isDoneCalibrating = true;
        yield return new WaitForSeconds(1f);
        TPuzzleSelection.gameObject.SetActive(true);
        THandConfigWindow.gameObject.SetActive(false);
        Debug.Log("Primary Hand Calibration Completed");
        yield break;
    }

}
