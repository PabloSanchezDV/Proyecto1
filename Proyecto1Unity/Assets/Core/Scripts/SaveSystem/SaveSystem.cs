using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    /*
     * Save File format
     * 
     *  
     *      1\n                 //the integer means in which level is the player
     *      000000\n            //each character (0/1) means true or false for the collectibles
     *      000000\n            //each character (0/1) means true or false for the bananas
     *      01/01/2000 - 00:00  //time stamp
     */

    public static void SaveFile(Data data, int slotID)
    {
        string dataString = ConvertDataToFile(data);
        string dataPath = GetPath(slotID);
        Debug.Log("Saving in path: " + dataPath);
        SaveFile(dataString, dataPath);
    }

    private static string ConvertDataToFile(Data data)
    {
        string dataString;

        //Level
        dataString = data.level.ToString() + '\n';

        //Big collectibles
        for (int i = 0; i < data.bigCollectibles.Length; i++)
        {
            if (data.bigCollectibles[i])
                dataString += 1;
            else
                dataString += 0;
        }
        dataString += "\n";

        //Bananas
        for (int i = 0; i < data.bananas.Length; i++)
        {
            if (data.bananas[i])
                dataString += 1;
            else
                dataString += 0;
        }
        dataString += "\n";

        //Time stamp
        dataString += data.timeStamp;

        return dataString;
    }

    private static string GetPath(int slotID)
    {
        switch(slotID)
        {
            case (1):
                return Application.persistentDataPath + "/ss1.file";
            case (2):
                return Application.persistentDataPath + "/ss2.file";
            case (3):
                return Application.persistentDataPath + "/ss3.file";
            default:
                throw new UnityException("Saving path does not exist");
        }
    }

    private static void SaveFile(string dataString, string dataPath)
    {
        StreamWriter streamWriter = new StreamWriter(dataPath);
        streamWriter.Write(dataString);
        streamWriter.Close();
    }

    public static Data LoadFile(int slotID, bool applyChangesToScene)
    {
        string dataPath = GetPath(slotID);

        if(File.Exists(dataPath))
        {
            string dataString = LoadFile(dataPath);
            Data data = ConvertFileToData(dataString);
            if(applyChangesToScene)
            {
                SaveDatabase.instance.ApplyLoadedData(data);
                SceneLoader.instance.LoadScene(data);
                GameManager.instance.UpdateCollectibles(data);
                UIManager.instance.UpdateHUD();
            }
            return data;
        }
        else
        {
            throw new UnityException("File does not exist");
        }
    }

    private static string LoadFile(string dataPath)
    {
        StreamReader streamReader = new StreamReader(dataPath);

        return streamReader.ReadToEnd();
    }

    private static Data ConvertFileToData(string dataString)
    {
        string[] strings = dataString.Split('\n');
        Data data = new Data();
        data.level = Convert.ToInt32(strings[0]);
        data.bigCollectibles = ConvertBinaryStringToBoolArray(strings[1]);
        data.bananas = ConvertBinaryStringToBoolArray(strings[2]);
        data.timeStamp = strings[3];

        return data;
    }

    private static bool[] ConvertBinaryStringToBoolArray(string dataString)
    {
        bool[] boolArray = new bool[dataString.Length];
        for(int i = 0; i < dataString.Length; i++)
        {
            if (dataString[i] == '0')
                boolArray[i] = false;
            else if (dataString[i] == '1')
                boolArray[i] = true;
            else
                throw new Exception("Trying to load corrupted data.");
        }
        return boolArray;
    }
}
