using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class MopConfigManager : MonoBehaviour
{
    public MopGameConfig ConfigContainer;
    public bool isConfigStatic;
    public TextScroller TSDifficulty;
    public TextScroller TSTimeLimit;
    public TextScroller TSDirtPermanence;
    public TextScroller TSDirtTriggerSize;
    public TextScroller TSSpawnAreaSize;

    public List<MopGameConfig> GameConfigTemplates = new List<MopGameConfig>();

    //HARD CODED FOR NOW
    public Vector2 SelectedBoardRange;
    public Color SelectedBoardColor = new Color(255,255,255,255);
    public Color DefaultBoardColor = new Color(255,255,255,255);

    public Transform TMopManager;
    public Transform TRightFootSpot;
    public Transform TLeftFootSpot;
    public Transform TConfigWindow;
    public Transform TGameUI;


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
            foreach(MopGameConfig config in GameConfigTemplates.ToList())
            {
                TSDifficulty.Entrees.Add(config.ConfigName);
            }
        }

        //POPULATE THE SCROLL ENTREES OF BOARD POSITION AND FOCUS AREA
        AreaSize[] AreaSizeArray = (AreaSize[])Enum.GetValues(typeof(AreaSize));
        if (AreaSizeArray.Length > 0)
        {
            for(int i = 0; i < AreaSizeArray.Length; i++)
            {
                TSDirtTriggerSize.Entrees.Add(AreaSizeArray[i].ToString());
            }
            
            for(int i = 0; i < AreaSizeArray.Length; i++)
            {
                TSSpawnAreaSize.Entrees.Add(AreaSizeArray[i].ToString());
            }
        }
    }

    public void LoadCustomConfigChecker()
    {
        foreach(Button b in TSTimeLimit.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }
        
        foreach(Button b in TSDirtPermanence.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }
        
        foreach(Button b in TSDirtTriggerSize.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }  
        
        foreach(Button b in TSSpawnAreaSize.Buttons)
        {
            b.onClick.AddListener(() => CheckIfCustomConfig());
        }
    }

    public void LoadGameConfig(MopGameConfig mopGameConfig)
    {
        TSTimeLimit.SetIntegralValues(0, 1000, Mathf.RoundToInt((int)mopGameConfig.TimeLimit));
        TSDirtPermanence.SetIntegralValues(1, 3, Mathf.RoundToInt((int)mopGameConfig.DirtPermanence));
        TSDirtTriggerSize.UpdateScrollLabel((int)mopGameConfig.DirTriggerSize);
        TSSpawnAreaSize.UpdateScrollLabel((int)mopGameConfig.SpawnAreaSize);

        if (isConfigStatic)
        {
            TSTimeLimit.ToggleButtons(mopGameConfig.isFreeConfig);
            TSDirtPermanence.ToggleButtons(mopGameConfig.isFreeConfig);
        }
    }

    public void CheckIfCustomConfig()
    {
        IsCustomConfig(GameConfigTemplates[TSDifficulty.GetCurrentIndex()]);
    }

    public bool IsCustomConfig(MopGameConfig pGameConfig)
    {
        if(TSTimeLimit.GetCurrentIntegralValue() != Mathf.RoundToInt((int)pGameConfig.TimeLimit) 
            || TSDirtPermanence.GetCurrentIntegralValue() != Mathf.RoundToInt((int)pGameConfig.DirtPermanence))
        {
            TSDifficulty.UpdateScrollLabel(3);
            return true;
        }
        else
        {
            return false;
        }
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
        TMopManager.GetComponent<MopManager>().MopGameConfig = ConfigContainer;
        TGameUI.gameObject.SetActive(true);
        TMopManager.gameObject.SetActive(true);
        TRightFootSpot.gameObject.SetActive(true);
        TLeftFootSpot.gameObject.SetActive(true);
        yield break;
    }

    public void LoadConfigToContainer()
    {
        ConfigContainer.TimeLimit = int.Parse(TSTimeLimit.Label.text);
        ConfigContainer.DirtPermanence = int.Parse(TSDirtPermanence.Label.text);
        ConfigContainer.DirTriggerSize = (AreaSize)TSDirtTriggerSize.GetCurrentIndex();
        ConfigContainer.SpawnAreaSize = (AreaSize)TSSpawnAreaSize.GetCurrentIndex();
    }

}
