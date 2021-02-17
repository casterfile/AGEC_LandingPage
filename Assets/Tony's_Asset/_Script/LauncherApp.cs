using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppLauncherPlugin;
using UnityEngine.UI;

public class LauncherApp : MonoBehaviour
{

    public string MobileAppID;//com.mobilemobility.agec sample name
    public string WindowAppID;//Name of the app

    public bool isFaceDetectSetting;
    public LauncherApp_DownloadInstall downloadInstall;


    public void TestOnMouseUp()
    {
        AppDownloader appDownloader = GameObject.Find("EventController").GetComponent<AppDownloader>();


#if UNITY_STANDALONE_WIN
        if(appDownloader.StartDownloadWindows(WindowAppID) == true)
        {
            if (isFaceDetectSetting == true)
            {
                HandleTextFile fileRead2 = new HandleTextFile();
                fileRead2.SetFileVariable("FaceDetectSetting", "0");
            }
            GameLuncher gl = new GameLuncher();
            gl.LunchNow(WindowAppID);

            HandleTextFile fileRead = new HandleTextFile();
            fileRead.SetFileVariable("GameName", WindowAppID);
        }
#endif

        MobileAppLauncher();

    }

    public void MobileAppLauncher()
    {
#if UNITY_ANDROID

        GameObject.Find("TextDebug").GetComponent<Text>().text = "Lunch App";
        AppLauncher.LaunchApp("" + MobileAppID, gameObject.name);

        if (AppLauncher.LaunchApp("" + MobileAppID, gameObject.name) == false)
        {
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Download Start";
            downloadInstall.StartDownload();
        }

#endif
    }

    


}
