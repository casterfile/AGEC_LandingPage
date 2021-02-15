using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector3 SpawnPoint;

    public MopManager MopManager;
    private int randomNum;

    public void GetDifficulty()
    {
        switch(MopManager.MopGameConfig.SpawnAreaSize)
        {
            case AreaSize.Small:
                randomNum = 1;
                break;
            case AreaSize.Medium:
                randomNum = Random.Range(1,3);
                break;
            case AreaSize.Large:
                randomNum = Random.Range(1,4);
                break;
        }
    }

    public void RightLargeSpawnPoint()
    {
        GetDifficulty();
        switch(randomNum)
        {
            case 1:
                SpawnPoint = RightLowerSpawnPoint();
                break;
            case 2:
                SpawnPoint = RightMiddleSpawnPoint();
                break;
            case 3:
                SpawnPoint = RightUpperSpawnPoint();
                break;
        }
    }

    public void LeftLargeSpawnPoint()
    {
        GetDifficulty();
        switch(randomNum)
        {
            case 1:
                SpawnPoint = LeftLowerSpawnPoint();
                break;
            case 2:
                SpawnPoint = LeftMiddleSpawnPoint();
                break;
            case 3:
                SpawnPoint = LeftUpperSpawnPoint();
                break;
        }
    }

    public Vector3 RightUpperSpawnPoint()
    {
        return new Vector3(
            Random.Range(0.084f, 0.476f),
            0.003520771f,
            Random.Range(-1.665f, -1.486f)
            
        );
    }
    public Vector3 RightMiddleSpawnPoint()
    {
        return new Vector3(
            Random.Range(0.084f, 0.643f),
            0.003520771f,
            Random.Range(-1.796f, -1.665f)
        );
    }

    public Vector3 RightLowerSpawnPoint()
    {
        return new Vector3(
            Random.Range(0.084f, 0.681f),
            0.003520771f,
            Random.Range(-1.914f, -1.796f)
        );
    }

    public Vector3 LeftUpperSpawnPoint()
    {
        return new Vector3(
            Random.Range(-0.551f, -0.096f),
            0.003520771f,
            Random.Range(-1.665f, -1.486f)
            
        );
    }
    public Vector3 LeftMiddleSpawnPoint()
    {
        return new Vector3(
            Random.Range(-0.67f, -0.096f),
            0.003520771f,
            Random.Range(-1.796f, -1.665f)
        );
    }

    public Vector3 LeftLowerSpawnPoint()
    {
        return new Vector3(
            Random.Range(-0.843f, -0.096f),
            0.003520771f,
            Random.Range(-1.914f, -1.796f)
        );
    }
}
