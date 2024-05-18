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
    public AudioSource PlayBufoTongue(GameObject go)
    {
        _audioDatabase.bufoTongueCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoTongueVolume);
        return CreateAudioSource(go, _audioDatabase.bufoTongueAC, _audioDatabase.bufoTongueCurrentVolume);
    }

    public AudioSource PlayBufoJump(GameObject go)
    {
        _audioDatabase.bufoJumpCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoJumpVolume);
        return CreateAudioSource(go, _audioDatabase.bufoJumpAC, _audioDatabase.bufoJumpCurrentVolume);
    }

    public AudioSource PlayBufoTongueImp(GameObject go)
    {
        _audioDatabase.bufoTongueImpCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoTongueImpVolume);
        return CreateAudioSource(go, _audioDatabase.bufoTongueImpAC, _audioDatabase.bufoTongueImpCurrentVolume);
    }

    public AudioSource PlayBufoFlutterRight(GameObject go)
    {
        _audioDatabase.bufoFlutterRightCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoFlutterRightVolume);
        return CreateAudioSource(go, _audioDatabase.bufoFlutterRightAC, _audioDatabase.bufoFlutterRightCurrentVolume, true);
    }

    public AudioSource PlayBufoFlutterLeft(GameObject go)
    {
        _audioDatabase.bufoFlutterLeftCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoFlutterLeftVolume);
        return CreateAudioSource(go, _audioDatabase.bufoFlutterLeftAC, _audioDatabase.bufoFlutterLeftCurrentVolume, true);
    }

    public AudioSource PlayBufoShiftAir(GameObject go)
    {
        _audioDatabase.bufoShiftAirCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoShiftAirVolume);
        return CreateAudioSource(go, _audioDatabase.bufoShiftAirAC, _audioDatabase.bufoShiftAirCurrentVolume, true);
    }

    public AudioSource PlayBufoStepsRight(GameObject go)
    {
        _audioDatabase.bufoStepsRightCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoStepsRightVolume);
        return CreateAudioSource(go, _audioDatabase.bufoStepsRightAC, _audioDatabase.bufoStepsRightCurrentVolume, true);
    }

    public AudioSource PlayBufoStepsLeft(GameObject go)
    {
        _audioDatabase.bufoStepsLeftCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoStepsLeftVolume);
        return CreateAudioSource(go, _audioDatabase.bufoStepsLeftAC, _audioDatabase.bufoStepsLeftCurrentVolume);
    }

    public AudioSource PlayBufoLanding(GameObject go)
    {
        _audioDatabase.bufoLandingCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoLandingVolume);
        return CreateAudioSource(go, _audioDatabase.bufoLandingAC, _audioDatabase.bufoLandingCurrentVolume);
    }

    public AudioSource PlayBufoDeath(GameObject go)
    {
        _audioDatabase.bufoDeathCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDeathVolume);
        return CreateAudioSource(go, _audioDatabase.bufoDeathAC, _audioDatabase.bufoDeathCurrentVolume);
    }

    public AudioSource PlayBufoBite(GameObject go)
    {
        _audioDatabase.bufoBiteCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoBiteVolume);
        return CreateAudioSource(go, _audioDatabase.bufoBiteAC, _audioDatabase.bufoBiteCurrentVolume);
    }

    public AudioSource PlayBufoHurt(GameObject go)
    {
        _audioDatabase.bufoHurtCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoHurtVolume);
        return CreateAudioSource(go, _audioDatabase.bufoHurtAC, _audioDatabase.bufoHurtCurrentVolume);
    }

    public AudioSource PlayLever(GameObject go)
    {
        _audioDatabase.leverCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.leverVolume);
        return CreateAudioSource(go, _audioDatabase.leverAC, _audioDatabase.leverCurrentVolume);
    }

    public AudioSource PlayPressurePlate(GameObject go)
    {
        _audioDatabase.pressurePlateCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.pressurePlateVolume);
        return CreateAudioSource(go, _audioDatabase.pressurePlateAC, _audioDatabase.pressurePlateCurrentVolume);
    }

    public AudioSource PlayPressurePlateBroken(GameObject go)
    {
        _audioDatabase.pressurePlateBrokenCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.pressurePlateBrokenVolume);
        return CreateAudioSource(go, _audioDatabase.pressurePlateBrokenAC, _audioDatabase.pressurePlateBrokenCurrentVolume);
    }

    public AudioSource PlayCollectable(GameObject go)
    {
        _audioDatabase.collectableCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.collectableVolume);
        return CreateAudioSource(go, _audioDatabase.collectableAC, _audioDatabase.collectableCurrentVolume);
    }

    public AudioSource PlayTimeTrial(GameObject go)
    {
        _audioDatabase.timeTrialCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.timeTrialVolume);
        return CreateAudioSource(go, _audioDatabase.timeTrialAC, _audioDatabase.timeTrialCurrentVolume);
    }        

    public AudioSource PlayCobraThrowingBrick(GameObject go)
    {
        _audioDatabase.cobraThrowingBrickCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraThrowingBrickVolume);
        return CreateAudioSource(go, _audioDatabase.cobraThrowingBrickAC, _audioDatabase.cobraThrowingBrickCurrentVolume);
    }

    public AudioSource PlayCobraFlee(GameObject go)
    {
        _audioDatabase.cobraFleeCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraFleeVolume);
        return CreateAudioSource(go, _audioDatabase.cobraFleeAC, _audioDatabase.cobraFleeCurrentVolume);
    }

    public AudioSource PlayCobraDetection(GameObject go)
    {
        _audioDatabase.cobraDetectionCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraDetectionVolume);
        return CreateAudioSource(go, _audioDatabase.cobraDetectionAC, _audioDatabase.cobraDetectionCurrentVolume);
    }

    public AudioSource PlayCobraDeath(GameObject go)
    {
        _audioDatabase.cobraDeathCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraDeathVolume);
        return CreateAudioSource(go, _audioDatabase.cobraDeathAC, _audioDatabase.cobraDeathCurrentVolume);
    }

    public AudioSource PlayMosquitoHum(GameObject go)
    {
        _audioDatabase.mosquitoHumCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.mosquitoHumVolume);
        return CreateAudioSource(go, _audioDatabase.mosquitoHumAC, _audioDatabase.mosquitoHumCurrentVolume, true);
    }

    public AudioSource PlayMosquitoDeath(GameObject go)
    {
        _audioDatabase.mosquitoDeathCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.mosquitoDeathVolume);
        return CreateAudioSource(go, _audioDatabase.mosquitoDeathAC, _audioDatabase.mosquitoDeathCurrentVolume);
    }

    public AudioSource PlayCrocodileAttack(GameObject go)
    {
        _audioDatabase.crocodileAttackCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.crocodileAttackVolume);
        return CreateAudioSource(go, _audioDatabase.crocodileAttackAC, _audioDatabase.crocodileAttackCurrentVolume);
    }

    public AudioSource PlayCrocodileDetection(GameObject go)
    {
        _audioDatabase.crocodileDetectionCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.crocodileDetectionVolume);
        return CreateAudioSource(go, _audioDatabase.crocodileDetectionAC, _audioDatabase.crocodileDetectionCurrentVolume);
    }

    public AudioSource PlayCrocodileDeath(GameObject go)
    {
        _audioDatabase.crocodileDeathCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.crocodileDeathVolume);
        return CreateAudioSource(go, _audioDatabase.crocodileDeathAC, _audioDatabase.crocodileDeathCurrentVolume);
    }

    public AudioSource PlayCampfire(GameObject go)
    {
        _audioDatabase.campfireCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.campfireVolume);
        return CreateAudioSource(go, _audioDatabase.campfireAC, _audioDatabase.campfireCurrentVolume, true);
    }

    public AudioSource PlayFactory(GameObject go)
    {
        _audioDatabase.factoryCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.factoryVolume);
        return CreateAudioSource(go, _audioDatabase.factoryAC, _audioDatabase.factoryCurrentVolume, true);
    }

    public AudioSource PlayLightedSign(GameObject go)
    {
        _audioDatabase.lightedSignCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.lightedSignVolume);
        return CreateAudioSource(go, _audioDatabase.lightedSignAC, _audioDatabase.lightedSignCurrentVolume, true);
    }

    public AudioSource PlaySnowplow(GameObject go)
    {
        _audioDatabase.snowplowCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.snowplowVolume);
        return CreateAudioSource(go, _audioDatabase.snowplowAC, _audioDatabase.snowplowCurrentVolume, true);
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
        aS.ignoreListenerPause = true;
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
