using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GamePauseScriptTest : MonoBehaviour
{
    float timer = 0.0f;
    public Text outputText,OutputResume, CountTestOutput;
    public static bool isPause = false;
    public GameObject CameraMain;
    // Start is called before the first frame update
    void Awake()
    {
        //CameraMain = GameObject.Find("MainCamera");
        
    }

    // Update is called once per frame
    

    void Update()
    {
        if(isPause == false)
        {
            timer += Time.deltaTime;
            float seconds = timer % 60f;

            //print("seconds: "+ seconds);
            double current = Math.Round(seconds, 2);
            outputText.text = "Time:" + current;

            int counting = GameObject.FindGameObjectsWithTag("testProcess").Length;
            CountTestOutput.text = " Number of object: "+ counting;
        }
        
    }


    //Only Works on Android
    void OnApplicationPause(bool _bool)
    {
        if (_bool)
        {
            print("paused");
            OutputResume.text = "paused";
            CameraMain.GetComponent<Camera>().enabled = false;
            isPause = true;
        }
        else
        {
            print("resumed");
            OutputResume.text = "resumed";
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
            OutputResume.text = "resumed";
            CameraMain.GetComponent<Camera>().enabled = true;
            isPause = false;
        }
        else
        {
            print("paused");
            OutputResume.text = "paused";
            CameraMain.GetComponent<Camera>().enabled = false;
            isPause = true;
        }
    }
}
