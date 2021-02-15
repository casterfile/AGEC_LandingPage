using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TENTATIVE ENUM [STRING] SIZES
public enum AreaSize
{
    Small,
    Medium,
    Large
}

[CreateAssetMenu(fileName = "NewMopConfig", menuName = "GameConfiguration/MopItUp")]
[System.Serializable]
public class MopGameConfig : GameConfig
{
    [Space(7)]
    [Header("Information")]
    public string ConfigName;
    public string ConfigDescription;
    public bool isFreeConfig;
    
    [Space(7)]
    [Header("Play Area")]
    public AreaSize SpawnAreaSize; //Size of the dirt spawn area
    public AreaSize DirTriggerSize; //Size of the trigger to wipe the dirt
    public int DirtPermanence; //Number of wipes to clean the Dirt
}

