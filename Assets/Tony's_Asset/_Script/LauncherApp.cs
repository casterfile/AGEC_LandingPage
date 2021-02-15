using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppLauncherPlugin;

public class LauncherApp : MonoBehaviour
{

    public string MobileAppID;//com.mobilemobility.agec sample name
    public string WindowAppID;//Name of the app

    public bool isFaceDetectSetting;

    public void OnMouseUp()
    {
        AppDownloader appDownloader = GameObject.Find("EventController").GetComponent<AppDownloader>();
        

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
        

        MobileAppLauncher();
    }

    public void MobileAppLauncher()
    {
#if UNITY_ANDROID
        AppLauncher.LaunchApp(""+ MobileAppID, gameObject.name);
        if (AppLauncher.LaunchApp("" + MobileAppID, gameObject.name) == false)
        {
            Application.OpenURL("http://192.168.1.8/download/DownloadSample.apk");
        }
        
        print("Launch: " + MobileAppID);
#endif
    }

    


}
