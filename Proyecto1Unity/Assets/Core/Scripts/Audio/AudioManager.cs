using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioDatabase _audioDatabase;
    [NonSerialized] private float _musicVolumeModifier;
    [NonSerialized] private float _soundsVolumeModifier;
    [NonSerialized] private float _dialogueVolumeModifier;

    //To add a new sound create an AudioSource, a Play method and a Stop method and add the Volume and Audioclip to AudioDatabase scriptableObject
    #region AudioSources
    [NonSerialized] public AudioSource testSoundAS;
    [NonSerialized] public AudioSource testDialogueAS;
    [NonSerialized] public AudioSource testMusicAS;
    #endregion

    private bool _areSoundsEnabled = true;
    public bool AreSoundsEnabled { get { return _areSoundsEnabled; } }
    private bool _isMusicEnabled = true;
    public bool IsMusicEnabled { get { return _isMusicEnabled;} }
    private bool _areDialoguesEnabled = true;
    public bool AreDialoguesEnabled { get { return _areDialoguesEnabled; } }

    private List<AudioSource> aSList = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        _areSoundsEnabled = PrefsManager.instance.GetBool(Pref.AreSoundsEnabled);
        _isMusicEnabled = PrefsManager.instance.GetBool(Pref.IsMusicEnabled);

        _musicVolumeModifier = 1;
        _soundsVolumeModifier = 1;
        _dialogueVolumeModifier = 1;
    }

    #region Settings Methods
    public void EnableDisableSounds()
    {
        if (!_areSoundsEnabled)
        {
            _areSoundsEnabled = true;
        }
        else
        {
            _areSoundsEnabled = false;
        }
        PrefsManager.instance.SetBool(Pref.AreSoundsEnabled, _areSoundsEnabled);
    }

    public void EnableDisableMusic()
    {
        if (!_isMusicEnabled)
        {
            _isMusicEnabled = true;
        }
        else
        {
            _isMusicEnabled = false;
        }
        PrefsManager.instance.SetBool(Pref.IsMusicEnabled, _isMusicEnabled);
        ChangeMainTitleMusicVolume();
    }

    public void EnableDisableDialogues()
    {
        if (!_areDialoguesEnabled)
        {
            _areDialoguesEnabled = true;
        }
        else
        {
            _areDialoguesEnabled = false;
        }
        PrefsManager.instance.SetBool(Pref.AreDialoguesEnabled, _areDialoguesEnabled);
    }

    public void SetMusicVolumeModifier(float mod)
    {
        if(mod < 0)
        {
            mod = 0;
        }
        else if(mod > 1)
        {
            mod = 1;
        }

        _musicVolumeModifier = mod;
        PrefsManager.instance.SetFloat(Pref.MusicVolume, mod);

        ChangeMainTitleMusicVolume();
    }

    public void SetSoundsVolumeModifier(float mod)
    {
        if (mod < 0)
        {
            mod = 0;
        }
        else if (mod > 1)
        {
            mod = 1;
        }

        _soundsVolumeModifier = mod;
        PrefsManager.instance.SetFloat(Pref.SoundsVolume, mod);
    }

    public void SetDialogueVolumeModifier(float mod)
    {
        if (mod < 0)
        {
            mod = 0;
        }
        else if (mod > 1)
        {
            mod = 1;
        }

        _dialogueVolumeModifier = mod;
        PrefsManager.instance.SetFloat(Pref.DialoguesVolume, mod);
    }

    private void ChangeMainTitleMusicVolume()
    {
        if(MusicManager.instance.testMusicAS != null)
        {
            MusicManager.instance.testMusicAS.volume = ChangeMusicVolumeAsPerModifier(_audioDatabase.testMusicVolume);
        }
    }

    #endregion

    #region Play Sounds Methods
    #region Player Movement
    public AudioSource TestSound(GameObject go)
    {
        _audioDatabase.testSoundCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.testSoundVolume);
        return CreateAudioSource(go, _audioDatabase.testSoundAC, _audioDatabase.testSoundCurrentVolume, true);
    }

    public AudioSource TestDialogue(GameObject go)
    {
        _audioDatabase.testDialogueCurrentVolume = ChangeDialoguesVolumeAsPerModifier(_audioDatabase.testDialogueVolume);
        return CreateAudioSource(go, _audioDatabase.testDialogueAC, _audioDatabase.testDialogueCurrentVolume);
    }

    public AudioSource TestMusic()
    {
        _audioDatabase.testMusicCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.testMusicVolume);
        return CreateMusicAudioSource(_audioDatabase.testMusicAC, _audioDatabase.testMusicCurrentVolume);
    }
    #endregion
    #endregion

    #region Stop Sounds Methods
    public void StopAudioSource(AudioSource aS)
    {
        if (aS != null)
        {
            aS.Stop();
            Destroy(aS);
        }
    }

    public void DestroyAllAudioSourcesOnSceneChange()
    {        
        foreach(AudioSource aS in aSList)
        {
            Destroy(aS);
        }
    }
    #endregion

    #region Private Methods
    private float ChangeMusicVolumeAsPerModifier(float originalVolume)
    {
        if (_isMusicEnabled)
        {
            return originalVolume * _musicVolumeModifier;
        }
        else
        {
            return 0;
        }
    }

    private float ChangeSoundsVolumeAsPerModifier(float originalVolume)
    {
        if (_areSoundsEnabled)
        {
            return originalVolume * _soundsVolumeModifier;
        }
        else
        {
            return 0;
        }        
    }

    private float ChangeDialoguesVolumeAsPerModifier(float originalVolume)
    {
        if (_areSoundsEnabled)
        {
            return originalVolume * _dialogueVolumeModifier;
        }
        else
        {
            return 0;
        }
    }

    private AudioSource CreateMusicAudioSource(AudioClip audioclip, float volume)
    {
        AudioSource aS = gameObject.AddComponent<AudioSource>();
        aS.clip = audioclip;
        if (_isMusicEnabled)
        {
            aS.volume = volume;
        }
        else
        {
            aS.volume = 0;
        }

        aS.Play();
        aS.loop = true;
        return aS;
    }

    private AudioSource CreateAudioSource(GameObject go, AudioClip audioClip, float volume, bool loop = false)
    {
        AudioSource aS = go.AddComponent<AudioSource>();
        aS.clip = audioClip;
        if (!loop)
        {
            if (_areSoundsEnabled)
            {
                aS.volume = volume;
            }
            else
            {
                aS.volume = 0;
            }
        }
        else
        {
            if (_isMusicEnabled)
            {
                aS.volume = volume;
            }
            else
            {
                aS.volume = 0;
            }
        }

        aS.spatialBlend = 1;
        aS.loop = loop;
        aS.Play();

        aSList.Add(aS);

        if (!loop)
        {
            StartCoroutine(DestroyAudioSourceWhenFinish(aS));
        }

        return aS;
    }

    IEnumerator DestroyAudioSourceWhenFinish(AudioSource audioSource)
    {
        bool isPlaying = true;
        while (isPlaying)
        {
            if(audioSource != null)
            {
                if (!audioSource.isPlaying)
                {
                    isPlaying = false;
                }
            }
            /*if(GameManager.Instance.HasSceneChanged())
            {
                break;
            }*/

            yield return null;
        }
        try
        {
            aSList.Remove(audioSource);
            Destroy(audioSource);
        }
        catch(Exception e)
        {
            Debug.Log(audioSource.name + " cannot be found and cannot be destroyed. " + e.Message);
        }
    }

    IEnumerator LoopMusicAC(AudioSource audioSource, AudioClip[] audioClips)
    {
        while (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                int i = UnityEngine.Random.Range(0, audioClips.Length);
                audioSource.clip = audioClips[i];
                audioSource.Play();
            }
            yield return null;
        }
    }
    #endregion
}
