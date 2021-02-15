using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HandleTextFile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*Game Tracking using text file
         * Name of the Game
         * State of the Game Library, FaceDetect, App
         * 
         */
        string path = Application.dataPath + "/gameState.txt";
        System.IO.File.WriteAllText(path, "Joomla is good!!!");
        print(path);
        ReadFile(path);
    }

    public void ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    public string ReturnRead(string FileName)
    {
        string DataPath = Application.dataPath + "/" + FileName + ".txt";
        //StreamReader reader = new StreamReader(DataPath);

        string content;
        using (StreamReader reader = new StreamReader(DataPath))
        {
            content = reader.ReadToEnd();
            reader.Close();
        }
        
        //string TextData = ""+reader.ReadToEnd();
        
        print("End Return: "+ content);
        return content;
    }
    

    public void SetFileVariable(string fileName, string State)
    {
        string path = Application.dataPath + "/"+fileName + ".txt";
        System.IO.File.WriteAllText(path, ""+ State);
        print(path);
        ReadFile(path);
    }


    public string ReturnReadFormLibrary(string FileName)
    {
        string DataPath = Application.dataPath + "/../../../../AgeC_App/AgeC_Data/" + FileName + ".txt";
        //StreamReader reader = new StreamReader(DataPath);

        string content;
        using (StreamReader reader = new StreamReader(DataPath))
        {
            content = reader.ReadToEnd();
            reader.Close();
        }

        //string TextData = ""+reader.ReadToEnd();

        print("End Return: " + content);
        return content;
    }

    public void SetFileFormLibrary(string fileName, string State)
    {
        //string DataPath = Application.dataPath + "/../../../../AgeC_App/AgeC_Data/" + FileName + ".txt";
        //StreamReader reader = new StreamReader(DataPath);

        string path = Application.dataPath + "/../../../../AgeC_App/AgeC_Data/" + "/" + fileName + ".txt";
        System.IO.File.WriteAllText(path, "" + State);
        print(path);
        ReadFile(path);
    }
}
