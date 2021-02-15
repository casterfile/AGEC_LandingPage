using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Diagnostics;
using System;

public class UnzipSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Open Notedpad
        /*
        string strCmdText;
        strCmdText = "/C notepad";   //This command to open a new notepad
        System.Diagnostics.Process.Start("CMD.exe", strCmdText); //Start cmd process
        */
        //string strCmdText = "/C tar.exe";   //This command to open a new notepad
        //string strCmdText = "/C tar.exe -a -c -f C:\\Users\\Anthony Castor\\Desktop\\output.zip C:\\Users\\Anthony Castor\\Desktop\\sample";
        //string strCmdText = "/C notepad";
        
        try
        {
            Process process = new Process();

            string strFinalLocation = "Unzip.exe";//"/C C:/Users/Anthony Castor/Desktop/sample/Unzip.exe";

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
