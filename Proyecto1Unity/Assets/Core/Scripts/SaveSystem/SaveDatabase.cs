using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDatabase : MonoBehaviour
{
    public static SaveDatabase instance;

    public Data data;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CreateNewData()
    {
        data = new Data();
        data.level = GameManager.instance.LevelID;
        data.bigCollectibles = new bool[GameManager.instance.MaxCollectibles];
        for(int i = 0; i < data.bigCollectibles.Length; i++)
        {
            data.bigCollectibles[i] = false;
        }
        data.bananas = new bool[GameManager.instance.MaxBananas];
        for(int i = 0; i < data.bananas.Length; i++)
        {
            data.bananas[i] = false;
        }
    }

    public void ApplyLoadedData(Data data)
    {
        this.data = data;
    }

    public void AddBanana(int id)
    {
        data.bananas[id - 1] = true; 
        string line = "Bananas = ";
        foreach (var item in data.bigCollectibles)
        {
            line += item + " ";
        }
        Debug.Log(line);
    }

    public void AddBig(int id)
    {
        data.bigCollectibles[id - 1] = true;
        string line = "Big = ";
        foreach(var item in data.bigCollectibles)
        {
            line += item + " ";
        }
        Debug.Log(line);
    }

    public void AddTimeStamp()
    {
        data.timeStamp = System.DateTime.Today.ToString("d") + " - " + System.DateTime.Now.ToString("t");
    }
}
