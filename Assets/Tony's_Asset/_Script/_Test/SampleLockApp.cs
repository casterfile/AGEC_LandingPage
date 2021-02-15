using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppLauncherPlugin;

using System.Diagnostics;
using System;
using UnityEngine.UI;
using System.IO;


public class SampleLockApp : MonoBehaviour
{
    /* Note
     * This App is inside the library
     * Name of App for Game library and FaceDetect
     * Game Library WindowAppID: AgeC and MobileAppID: com.agec.app
     * faceDetect WindowAppID: faceDetect and MobileAppID: com.agec.facedetect
     * SampleLockApp WindowAppID: SampleLockApp and MobileAppID: com.agec.samplelockapp
     */

    public string MobileAppID;//com.mobilemobility.agec sample name
    public string WindowAppID;//Name of the app
    public void LaunchAppExit()
    {
#if UNITY_ANDROID
        AppLauncher.LaunchApp(""+ MobileAppID, gameObject.name);
        print("Launch: " + MobileAppID);
#endif

        LunchLibrary(WindowAppID);
    }

    public void LaunchAppLockScreen()
    {
#if UNITY_ANDROID
        AppLauncher.LaunchApp(""+ MobileAppID, gameObject.name);
        print("Launch: " + MobileAppID);
#endif

        LunchLifaceDetect(WindowAppID);
    }


    private void LunchLibrary(string appNameExecute)
    {
#if UNITY_STANDALONE_WIN
       

        try
        {
            Process process = new Process();
            string m_Path = Application.dataPath;
            string strFinalLocation = m_Path + "/../../../" + appNameExecute + ".exe";
            process.StartInfo.FileName = strFinalLocation;
            process.Start();
            print("LunchNow: " + m_Path);
            Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
            goOutput.text = strFinalLocation;
            Application.Quit();
        }
        catch (Exception e)
        {
            print(e);
        }
#endif
    }

    private void LunchLifaceDetect(string appNameExecute)
    {
#if UNITY_STANDALONE_WIN


        try
        {
            HandleTextFile fileRead = new HandleTextFile();
            fileRead.SetFileFormLibrary("FaceDetectSetting", "1");

            Process process = new Process();
            string m_Path = Application.dataPath;
            string strFinalLocation = m_Path + "/../../" + appNameExecute + "/" + appNameExecute + ".exe";
            process.StartInfo.FileName = strFinalLocation;
            process.Start();
            print("LunchNow: " + m_Path);
            Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
            goOutput.text = strFinalLocation;

            
            
        }
        catch (Exception e)
        {
            print(e);
        }
#endif
    }
}
