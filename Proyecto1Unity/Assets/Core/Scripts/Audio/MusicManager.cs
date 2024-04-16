using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [NonSerialized] public static MusicManager instance;

    [NonSerialized] public AudioSource testMusicAS;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }  
    }

    public void PlayInGameMusic()
    {
        testMusicAS = AudioManager.instance.TestMusic();
    }

    public void PauseInGameMusic()
    {
        if(testMusicAS != null)
        {
            testMusicAS.Pause();
        }
    }

    public void UnpauseInGameMusic()
    {
        testMusicAS.UnPause();
    }

    public void StopInGameMusic()
    {
        AudioManager.instance.StopAudioSource(testMusicAS);
    }
}
