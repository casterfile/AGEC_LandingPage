using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AndroidInstallTest : MonoBehaviour
{
    private string savePath;
    private string url;
    string namePDF = "test";
    string typeData = "pdf";

    void Start()
    {
        StartCoroutine(downLoadFromServer());
        
    }


    void openPDF()
    {
        
        TextAsset pdfTem = Resources.Load("PDFs/" + namePDF, typeof(TextAsset)) as TextAsset;
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + namePDF + ".pdf", pdfTem.bytes);
        Application.OpenURL(Application.persistentDataPath + "/" + namePDF + ".pdf");
    }


    IEnumerator downLoadFromServer()
    {
        

        //string url = "https://download.apkpure.com/b/APK/Y29tLmZhY2Vib29rLmxpdGVfMjcyMDgyNzA4XzNkMGYxZjkx?_fn=RmFjZWJvb2sgTGl0ZV92MjM3LjAuMC43LjExOF9hcGtwdXJlLmNvbS5hcGs&as=277f7c3638b4f1c4a594337496cbe8e360236eff&ai=714353206&at=1612934791&_sa=ai%2Cat&k=e01e7829cc8bda069c9901e8c225337360261187&_p=Y29tLmZhY2Vib29rLmxpdGU&c=1%7CSOCIAL%7CZGV2PUZhY2Vib29rJnQ9YXBrJnM9MTYxNzA0OCZ2bj0yMzcuMC4wLjcuMTE4JnZjPTI3MjA4MjcwOA";
        string url = "http://192.168.1.8/download/Philippines_1987."+ typeData;
        savePath = Path.Combine(Application.persistentDataPath, "data");
        savePath = Path.Combine(savePath, namePDF+"." + typeData);


        //savePath = Path.Combine(Application.dataPath, "AntiOvr.apk");

        Dictionary<string, string> header = new Dictionary<string, string>();
        string userAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
        header.Add("User-Agent", userAgent);
        WWW www = new WWW(url, null, header);


        while (!www.isDone)
        {
            //Must yield below/wait for a frame
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Stat: " + www.progress;
            yield return null;
        }

        byte[] yourBytes = www.bytes;

        GameObject.Find("TextDebug").GetComponent<Text>().text = "Done downloading. Size: " + yourBytes.Length;


        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(savePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Created Dir";
        }

        try
        {
            //Now Save it
            System.IO.File.WriteAllBytes(savePath, yourBytes);
            Debug.Log("Saved Data to: " + savePath.Replace("/", "\\"));
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Saved Data: "+ savePath;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + savePath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Error Saving Data";
        }

        //Install APK
        //installApp(savePath);

        
    }
    public void DownloadStart()
    {
        //string url = "https://m.apkpure.com/facebook-lite/com.facebook.lite/download?from=details";
        //Application.OpenURL(url);
        StartCoroutine(downLoadFromServer());
    }

    public void openfile()
    {
        GameObject.Find("TextDebug").GetComponent<Text>().text = "savePath: " + savePath;
        Application.OpenURL(savePath);
    }


    public bool installApp(string apkPath)
    {
        try
        {
            AndroidJavaClass intentObj = new AndroidJavaClass("android.content.Intent");
            string ACTION_VIEW = intentObj.GetStatic<string>("ACTION_VIEW");
            int FLAG_ACTIVITY_NEW_TASK = intentObj.GetStatic<int>("FLAG_ACTIVITY_NEW_TASK");
            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", ACTION_VIEW);

            AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", apkPath);
            AndroidJavaClass uriObj = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uri = uriObj.CallStatic<AndroidJavaObject>("fromFile", fileObj);

            intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/vnd.android.package-archive");
            intent.Call<AndroidJavaObject>("addFlags", FLAG_ACTIVITY_NEW_TASK);
            intent.Call<AndroidJavaObject>("setClassName", "com.android.packageinstaller", "com.android.packageinstaller.PackageInstallerActivity");

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("startActivity", intent);

            GameObject.Find("TextDebug").GetComponent<Text>().text = "Success";
            return true;
        }
        catch (System.Exception e)
        {
            GameObject.Find("TextDebug").GetComponent<Text>().text = "Error: " + e.Message;
            return false;
        }
    }

}
