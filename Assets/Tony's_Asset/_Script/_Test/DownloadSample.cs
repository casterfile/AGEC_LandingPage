using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using UnityEngine.UI;

public class DownloadSample : MonoBehaviour
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
        DownloadBG.SetActive(false);
        isFileAvailable = true;
        isDownloadAlreadyStarted = false;
        client = new WebClient();
        DNameFile = "installer";
        DNameType = "zip";

        if (System.IO.File.Exists("big_buck_bunny_720p_30mb" + "." + "mp4"))
        {
            print("File Exist YES");
        }
        else
        {
            print("File Exist NO");
        }


        

        //print("Finish download: "+ client);

        //DownLoadFileInBackground4("http://localhost/download/");

    }



    private IEnumerator CheckIfFinish()
    {
        
        //
        //client.DownloadFile("http://localhost/download/" + DNameFile + "." + DNameType, @"" + DNameFile + ".zip");

        while (isFileAvailable)
        {
            yield return new WaitForSeconds(1.5f);
            print("Coroutine ended: " + Time.time + " seconds");

            if(isDownloadAlreadyStarted == false)
            {
                client.DownloadFile("https://www.sample-videos.com/video123/mp4/720/big_buck_bunny_720p_30mb.mp4", @"" + "big_buck_bunny_720p_30mb" + ".mp4");
                isDownloadAlreadyStarted = true;
            }

            if (System.IO.File.Exists("big_buck_bunny_720p_30mb" + "." + "mp4"))
            {
                isFileAvailable = false;
                print("Download End now");
                yield return null;
            }
        }
        DownloadBG.SetActive(false);

        print("Start Download");

        
        
    }

    public void StartDownload()
    {
        DownloadBG.SetActive(true);
        StartCoroutine(CheckIfFinish());
    }

    public void SampleButton()
    {
        print("Data 1234");
    }


    /*
    // Sample call : DownLoadFileInBackground4 ("http://www.contoso.com/logs/January.txt");
    public static void DownLoadFileInBackground4(string address)
    {
        WebClient client = new WebClient();
        Uri uri = new Uri(address);

        // Specify a DownloadFileCompleted handler here...

        // Specify a progress notification handler.
        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);

        client.DownloadFileAsync(uri, "Sample.zip");
    }

    private static void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
    {
        // Displays the operation identifier, and the transfer progress.
        Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
            (string)e.UserState,
            e.BytesReceived,
            e.TotalBytesToReceive,
            e.ProgressPercentage);
    }
    */

}
