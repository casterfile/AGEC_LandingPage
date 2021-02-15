using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Put this script in the Scene
public class Global_GamePause : MonoBehaviour
{
    
    public GameObject CameraMain; //The main Camera here when implementing this script
    public static bool isPause = false;
    // Start is called before the first frame update
    void Awake()
    {
        

    }

    // Update is called once per frame


    void Update()
    {
        if (isPause == false)
        {
            //All process needs to be inside the Pause Arguments 
        }

        /*
        Implementation on another Script
        if (Global_GamePause.isPause == false)
        {
             //All process needs to be inside the Pause Arguments   
        }
        */

    }


    //Only Works on Android
    void OnApplicationPause(bool _bool)
    {
        if (_bool)
        {
            print("paused");
            CameraMain.GetComponent<Camera>().enabled = false;
            isPause = true;
        }
        else
        {
            print("resumed");
            CameraMain.GetComponent<Camera>().enabled = true;
            isPause = false;
        }
    }

    //Work on Windows
    void OnApplicationFocus(bool _bool)
    {
        if (_bool)
        {
            print("resumed");
            CameraMain.GetComponent<Camera>().enabled = true;
            isPause = false;
        }
        else
        {
            print("paused");
            CameraMain.GetComponent<Camera>().enabled = false;
            isPause = true;
        }
    }
}
