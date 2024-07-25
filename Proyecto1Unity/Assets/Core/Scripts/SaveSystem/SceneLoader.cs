using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    [NonSerialized] public Big[] bigCollectibles;
    [NonSerialized] public Banana[] bananas;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (SceneTransitioner.instance.IsLoaded)
        {
            SceneTransitioner.instance.IsLoaded = false;
            LoadScene(SceneTransitioner.instance.LoadedData);
        }
    }

    public void LoadScene(Data data)
    {
        SaveDatabase.instance.ApplyLoadedData(data);
        UpdateCollectibles(data);
        GameManager.instance.UpdateCollectibles(data);
        UIManager.instance.UpdateHUD();
    }

    private void UpdateCollectibles(Data data)
    {
        for (int i = 0; i < bigCollectibles.Length; i++)
        {
            if (data.bigCollectibles[i])
            {
                bigCollectibles[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < bananas.Length; i++)
        {
            if (data.bananas[i])
            {
                bananas[i].gameObject.SetActive(false);
            }
        }
    }
}
