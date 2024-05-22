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
        string dataString = JsonUtility.ToJson(data);
        string dataPath = GetPath(slotID);
        Debug.Log("Saving in path: " + dataPath);
        SaveFile(dataString, dataPath);
    }

    private static string GetPath(int slotID)
    {
        switch(slotID)
        {
            case (1):
                return Application.persistentDataPath + "/ss1.json";
            case (2):
                return Application.persistentDataPath + "/ss2.json";
            case (3):
                return Application.persistentDataPath + "/ss3.json";
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
        return JsonUtility.FromJson<Data>(dataString);
    }
}
