using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class StoreApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            //Process myProcess = new Process();
            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //myProcess.StartInfo.CreateNoWindow = true;
            //myProcess.StartInfo.UseShellExecute = false;
            //myProcess.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            //string path = "C:\\Users\\Anthony Castor\\Desktop\\testFile.bat"; ///C:\Users\Anthony Castor\Desktop
            //myProcess.StartInfo.Arguments = "/c" + path;
            //myProcess.EnableRaisingEvents = true;
            //myProcess.Start();
            //myProcess.WaitForExit();
            //int ExitCode = myProcess.ExitCode;


            //String path = @"f:\temp\data.txt";
            //Process foo = new Process();
            //foo.StartInfo.FileName = "Notepad.exe";
            ////foo.StartInfo.Arguments = path;
            //foo.Start();
            //print("Exit finish");
            //print(ExitCode);
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
