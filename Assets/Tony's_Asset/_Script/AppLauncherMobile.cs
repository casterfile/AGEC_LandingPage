using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppLauncherPlugin;

public class AppLauncherMobile : MonoBehaviour
{
    #if UNITY_ANDROID
    public string AppID;//com.mobilemobility.agec sample name
    public void MobileAppLauncher()
    {
        AppLauncher.LaunchApp(""+ AppID, gameObject.name);
        print("Launch: " + AppID);
    }

    #endif
}
