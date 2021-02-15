using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileDownloader : MonoBehaviour
{
    public Text Output;
    void Start()
    {
        StartCoroutine(DownloadFile());
    }

    IEnumerator DownloadFile()
    {
        var uwr = new UnityWebRequest("http://localhost/download/", UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(Application.dataPath+ "/../", "installer.zip");
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.LogError(uwr.error);
            Output.text = uwr.error;
        }
        else
        {
            Debug.Log("File successfully downloaded and saved to " + path);
            Output.text = "File successfully downloaded and saved to " + path;
        }
    }
}