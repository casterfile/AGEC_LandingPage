using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEngine.UI;
using System.IO;

public class GameLuncher : MonoBehaviour
{
    private void Start()
    {
        print("SystemInfo.deviceUniqueIdentifier :" + SystemInfo.deviceUniqueIdentifier);
        Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
        goOutput.text = "SystemInfo.deviceUniqueIdentifier :" + SystemInfo.deviceUniqueIdentifier;

        /*
         deviceUniqueIdentifier will be use to make sure game won't be lunch on other device
         for example we lunch the Game outside the Store App the game wont lunch since it has no command from the server
         how will the checking works
         -Store: before it lunch the game it will send the deviceUniqueIdentifier and the gameID that i want to lunch
         Then it will lunch the Game
         -Game: the game will check for the deviceUniqueIdentifier and it gameID if it match the server info if it match
         then the game will lunch and the user can play the game

        what problem do we fix here we make sure that game can't be play outside from the machine that it is install
        and we make sure that game cant be lunch without the command from the StoreAPP 
         */
    }
    public void LunchNow(string appNameExecute)
    {
        try
        {
            Process process = new Process();
            
            //IF you want to call the app gain just execute the app gain
            //process.StartInfo.FileName = "Notepad.exe"; //
            //process.StartInfo.FileName = "C:/Program Files (x86)/Notepad++/notepad++.exe";
            //process.StartInfo.Arguments = path;


            //This will execute the GAME
            string m_Path = Application.dataPath;
            //string strFinalLocation = m_Path + "/../Library/AGEC_GAME01/AGEC_GAME01.exe";

            string strFinalLocation = m_Path + "/../Library/"+ appNameExecute + "/"+ appNameExecute + ".exe";
            process.StartInfo.FileName = strFinalLocation;
            process.Start();
            print("LunchNow: "+ m_Path);
            Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
            goOutput.text = strFinalLocation;
            //print(ExitCode);

            //Implementation on the Game after the exit call the store app .exe again to Make call it to the front
        }
        catch (Exception e)
        {
            print(e);
        }
    }

    public void DeleteGame()
    {
        //AGEC_GAME01_Deleted
        string m_Path = Application.dataPath;
        string strFinalLocation = m_Path + "/../Library/AGEC_GAME01_Deleted";
        if (Directory.Exists(strFinalLocation)) {
            Directory.Delete(strFinalLocation, true);
        }
        Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
        goOutput.text = "Delete Directory: "+ strFinalLocation;
    }

    public void CreateDirectory()
    {
        string m_Path = Application.dataPath;
        string strFinalLocation = m_Path + "/../Library/AGEC_GAME01_Deleted";
        Directory.CreateDirectory(strFinalLocation);

        Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
        goOutput.text = "Create Directory: " + strFinalLocation;
    }


    public void LunchAppFromLibrary(string appNameExecute)
    {
#if UNITY_STANDALONE_WIN


        try
        {
            Process process = new Process();
            string m_Path = Application.dataPath;
            string strFinalLocation = m_Path + "/../../" + appNameExecute + "/" + appNameExecute + ".exe";
            process.StartInfo.FileName = strFinalLocation;
            process.Start();
            print("LunchNow: " + m_Path);
            Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
            goOutput.text = strFinalLocation;
        }
        catch (Exception e)
        {
            print(e);
        }
#endif
    }

    public void LunchGameLibrayFromLibrary()
    {
#if UNITY_STANDALONE_WIN


        try
        {
            Process process = new Process();
            string m_Path = Application.dataPath;
            string strFinalLocation = m_Path + "/../../../AgeC.exe";
            process.StartInfo.FileName = strFinalLocation;
            process.Start();
            print("LunchNow: " + m_Path);
            Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
            goOutput.text = strFinalLocation;
        }
        catch (Exception e)
        {
            print(e);
        }
#endif
    }

}
