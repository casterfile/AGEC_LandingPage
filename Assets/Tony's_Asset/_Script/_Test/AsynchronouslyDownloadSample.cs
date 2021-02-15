using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.ComponentModel;

public class AsynchronouslyDownloadSample : MonoBehaviour
{
    float progressBar1 = 0;
    string DNameFile = "Sample.zip";
    string URLFile = "http://localhost/download/";
    // Start is called before the first frame update
    void Start()
    {
        /*
        WebClient client = new WebClient();
        
        client.DownloadFile("http://localhost/download/"+ DNameFile + ".zip", @""+ DNameFile + ".zip");
        print("Finish download: "+ client);
        */
        //DownLoadFileInBackground4("http://localhost/download/");
        downloadFile();
    }


    private void downloadFile()
    {
        //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        // This will download a large image from the web, you can change the value
        // i.e a textbox : textBox1.Text
        string url = URLFile + DNameFile;
        

        using (WebClient wc = new WebClient())
        {
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            wc.DownloadFileAsync(new Uri(url), DNameFile);
        }
    }
    
    private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        // In case you don't have a progressBar Log the value instead 
        // Console.WriteLine(e.ProgressPercentage);
        progressBar1 = e.ProgressPercentage;
    }

    private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        progressBar1 = 0;

        if (e.Cancelled)
        {
            print("The download has been cancelled");
            return;
        }

        if (e.Error != null) // We have an error! Retry a few times, then abort.
        {
            print("An error ocurred while trying to download file");

            return;
        }

        print("File succesfully downloaded");
    }


}
