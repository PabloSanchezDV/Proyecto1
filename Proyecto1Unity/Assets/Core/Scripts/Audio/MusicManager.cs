using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [NonSerialized] public static MusicManager instance;

    [NonSerialized] public AudioSource musicAS;

    void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        musicAS = PlayInGameMusic();
    }

    public AudioSource PlayInGameMusic()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (sceneIndex)
        {
            case 0:
                musicAS = AudioManager.instance.PlayMainThemeMusic();
                return musicAS;
            case 1:
                musicAS = AudioManager.instance.PlayJungleThemeMusic();
                return musicAS;
            case 2:
                musicAS = AudioManager.instance.PlayMountainThemeMusic();
                return musicAS;
            case 3:
                musicAS = AudioManager.instance.PlaySwampThemeMusic();
                return musicAS;
            default:
                throw new Exception("MusicManager can't process the music.");
        }        
    }
}
