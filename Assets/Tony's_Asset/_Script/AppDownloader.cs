using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using UnityEngine.UI;
using System.Diagnostics;

public class AppDownloader : MonoBehaviour
{
    private bool isFileAvailable;
    private string DNameFile;
    private string DNameType;
    private WebClient client;
    private bool isDownloadAlreadyStarted;
    public GameObject DownloadBG;

    // Start is called before the first frame update
    void Start()
    {
        DownloadBG = GameObject.Find("DownloadBG");
        DownloadBG.SetActive(false);
        isFileAvailable = true;
        isDownloadAlreadyStarted = false;
        client = new WebClient();
        DNameFile = "installer";
        DNameType = "zip";
    }
    public bool StartDownloadWindows(string WindowAppID)
    {
#if UNITY_STANDALONE_WIN
        DNameFile = WindowAppID;
        if (System.IO.File.Exists(WindowAppID + "." + "zip"))
        {
            print("File Exist YES");
            return true;
        }
        else
        {
            print("File Exist NO");
            DownloadBG.SetActive(true);
            StartCoroutine(CheckIfFinish());
            return false;
        }
#endif
#if UNITY_ANDROID
        return false;
#endif
    }



    public IEnumerator CheckIfFinish()
    {
        while (isFileAvailable)
        {
            yield return new WaitForSeconds(1.5f);
            print("Coroutine ended: " + Time.time + " seconds");

            if (isDownloadAlreadyStarted == false)
            {
                //client.DownloadFile("https://www.sample-videos.com/video123/mp4/720/big_buck_bunny_720p_30mb.mp4", @"" + "big_buck_bunny_720p_30mb" + ".mp4");
                client.DownloadFile("http://localhost/download/" + DNameFile + "." + DNameType, @"" + DNameFile + ".zip");

                isDownloadAlreadyStarted = true;
            }

            if (System.IO.File.Exists(DNameFile + "." + DNameType))
            {
                isFileAvailable = false;
                DownloadBG.SetActive(false);
                Unzip();
                print("Download End now");
                yield return null;
            }
        }

        

    }

    public void Unzip()
    {
        try
        {
            Process process = new Process();

            string strFinalLocation = "Unzip.exe";

            process.StartInfo.FileName = strFinalLocation;
            process.Start();
            print("" + process.StartInfo.FileName);
            
        }
        catch (Exception e)
        {
            print(e);
        }

    }
}
