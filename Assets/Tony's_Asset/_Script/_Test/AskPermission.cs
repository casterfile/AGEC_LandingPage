using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class AskPermission : MonoBehaviour
{
    GameObject dialog = null;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            
        }
        /*
        Permission.RequestUserPermission(Permission.Microphone);
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        Permission.RequestUserPermission(Permission.ExternalStorageRead);
        Permission.RequestUserPermission(Permission.Camera);
        Permission.RequestUserPermission(Permission.FineLocation);
        */
#endif
    }

    private void Update()
    {
#if UNITY_ANDROID

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }

 
#endif
    }
}
