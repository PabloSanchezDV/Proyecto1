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
        //_areSoundsEnabled = PrefsManager.instance.GetBool(Pref.AreSoundsEnabled);
        //_isMusicEnabled = PrefsManager.instance.GetBool(Pref.IsMusicEnabled);

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
        return CreateAudioSource(go, _audioDatabase.bufoTongueAC, _audioDatabase.bufoTongueCurrentVolume, _audioDatabase.bufoTongueMaxDistance);
    }

    public AudioSource PlayBufoJump(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 4);
        switch(index)
        {
            case 1:
                _audioDatabase.bufoJump1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoJump1Volume);
                return CreateAudioSource(go, _audioDatabase.bufoJump1AC, _audioDatabase.bufoJump1CurrentVolume, 500);
            case 2:
                _audioDatabase.bufoJump2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoJump2Volume);
                return CreateAudioSource(go, _audioDatabase.bufoJump2AC, _audioDatabase.bufoJump2CurrentVolume, 500);
            case 3:
                _audioDatabase.bufoJump3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoJump3Volume);
                return CreateAudioSource(go, _audioDatabase.bufoJump3AC, _audioDatabase.bufoJump3CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayBufoJump.");
        }
    }

    public AudioSource PlayBufoTongueImp(GameObject go)
    {
        _audioDatabase.bufoTongueImpCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoTongueImpVolume);
        return CreateAudioSource(go, _audioDatabase.bufoTongueImpAC, _audioDatabase.bufoTongueImpCurrentVolume, 500);
    }

    public AudioSource PlayBufoFlutter(GameObject go)
    {
        _audioDatabase.bufoFlutterCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoFlutterVolume);
        return CreateAudioSource(go, _audioDatabase.bufoFlutterAC, _audioDatabase.bufoFlutterCurrentVolume, 500, true);
    }

    public AudioSource PlayBufoShiftAir(GameObject go)
    {
        _audioDatabase.bufoShiftAirCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoShiftAirVolume);
        return CreateAudioSource(go, _audioDatabase.bufoShiftAirAC, _audioDatabase.bufoShiftAirCurrentVolume, 500);
    }

    public AudioSource PlayBufoStepsRight(GameObject go)
    {
        _audioDatabase.bufoStepsRightCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoStepsRightVolume);
        return CreateAudioSource(go, _audioDatabase.bufoStepsRightAC, _audioDatabase.bufoStepsRightCurrentVolume, 500);
    }

    public AudioSource PlayBufoStepsLeft(GameObject go)
    {
        _audioDatabase.bufoStepsLeftCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoStepsLeftVolume);
        return CreateAudioSource(go, _audioDatabase.bufoStepsLeftAC, _audioDatabase.bufoStepsLeftCurrentVolume, 500);
    }

    public AudioSource PlayBufoLanding(GameObject go) 
    {
        _audioDatabase.bufoLandingCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoLandingVolume);
        return CreateAudioSource(go, _audioDatabase.bufoLandingAC, _audioDatabase.bufoLandingCurrentVolume, 500);
    }

    public AudioSource PlayBufoDeath(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 3);
        switch(index)
        {
            case 1:
                _audioDatabase.bufoDeath1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDeath1Volume);
                return CreateAudioSource(go, _audioDatabase.bufoDeath1AC, _audioDatabase.bufoDeath1CurrentVolume, 500);
            case 2:
                _audioDatabase.bufoDeath2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDeath2Volume);
                return CreateAudioSource(go, _audioDatabase.bufoDeath2AC, _audioDatabase.bufoDeath2CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayBufoDeath.");
        }        
    }

    public AudioSource PlayBufoBite(GameObject go)
    {
        _audioDatabase.bufoBiteCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoBiteVolume);
        return CreateAudioSource(go, _audioDatabase.bufoBiteAC, _audioDatabase.bufoBiteCurrentVolume, 500);
    }

    public AudioSource PlayBufoHurt(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 5);

        switch(index)
        {
            case 1:
                _audioDatabase.bufoHurt1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoHurt1Volume);
                return CreateAudioSource(go, _audioDatabase.bufoHurt1AC, _audioDatabase.bufoHurt1CurrentVolume, 500);
            case 2:
                _audioDatabase.bufoHurt2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoHurt2Volume);
                return CreateAudioSource(go, _audioDatabase.bufoHurt2AC, _audioDatabase.bufoHurt2CurrentVolume, 500);
            case 3:
                _audioDatabase.bufoHurt3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoHurt3Volume);
                return CreateAudioSource(go, _audioDatabase.bufoHurt3AC, _audioDatabase.bufoHurt3CurrentVolume, 500);
            case 4:
                _audioDatabase.bufoHurt4CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoHurt4Volume);
                return CreateAudioSource(go, _audioDatabase.bufoHurt4AC, _audioDatabase.bufoHurt4CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayBufoHurt.");
        }
    }
    #endregion
    
    #region Interactive Elements 
    public AudioSource PlayLever(GameObject go)
    {
        _audioDatabase.leverCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.leverVolume);
        return CreateAudioSource(go, _audioDatabase.leverAC, _audioDatabase.leverCurrentVolume, 500);
    }

    public AudioSource PlayBoxShift(GameObject go)
    {
        _audioDatabase.boxShiftCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.boxShiftVolume);
        return CreateAudioSource(go, _audioDatabase.boxShiftAC, _audioDatabase.boxShiftCurrentVolume, 500, true);
    }

    public AudioSource PlayPressurePlate(GameObject go)
    {
        _audioDatabase.pressurePlateCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.pressurePlateVolume);
        return CreateAudioSource(go, _audioDatabase.pressurePlateAC, _audioDatabase.pressurePlateCurrentVolume, 500);
    }

    public AudioSource PlayPressurePlateBroken(GameObject go)
    {
        _audioDatabase.pressurePlateBrokenCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.pressurePlateBrokenVolume);
        return CreateAudioSource(go, _audioDatabase.pressurePlateBrokenAC, _audioDatabase.pressurePlateBrokenCurrentVolume, 500);
    }

    public AudioSource PlayCollectable(GameObject go)
    {
        _audioDatabase.collectableCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.collectableVolume);
        return CreateAudioSource(go, _audioDatabase.collectableAC, _audioDatabase.collectableCurrentVolume, 500);
    }

    public AudioSource PlayBanana(GameObject go)
    {
        _audioDatabase.bananaCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bananaVolume);
        return CreateAudioSource(go, _audioDatabase.bananaAC, _audioDatabase.bananaCurrentVolume, 500);
    }

    public AudioSource PlayButton(GameObject go)
    {
        _audioDatabase.buttonCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.buttonVolume);
        return CreateAudioSource(go, _audioDatabase.buttonAC, _audioDatabase.buttonCurrentVolume, 500);
    }

    public AudioSource PlayTimeTrialNormal(GameObject go)
    {
        _audioDatabase.timeTrialNormalCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.timeTrialNormalVolume);
        return CreateAudioSource(go, _audioDatabase.timeTrialNormalAC, _audioDatabase.timeTrialNormalCurrentVolume, 500, true);
    }

    public AudioSource PlayTimeTrialUrgent(GameObject go)
    {
        _audioDatabase.timeTrialUrgentCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.timeTrialUrgentVolume);
        return CreateAudioSource(go, _audioDatabase.timeTrialUrgentAC, _audioDatabase.timeTrialUrgentCurrentVolume, 500, true);
    }

    public AudioSource PlayTimeTrialEnd(GameObject go)
    {
        _audioDatabase.timeTrialEndCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.timeTrialEndVolume);
        return CreateAudioSource(go, _audioDatabase.timeTrialEndAC, _audioDatabase.timeTrialEndCurrentVolume, 500);
    }

    public AudioSource PlayFence(GameObject go)
    {
        _audioDatabase.fenceCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.fenceVolume);
        return CreateAudioSource(go, _audioDatabase.fenceAC, _audioDatabase.fenceCurrentVolume, 500);
    }
    #endregion

    #region Enemies
    public AudioSource PlayCobraThrowingBrick(GameObject go)
    {
        _audioDatabase.cobraThrowingBrickCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraThrowingBrickVolume);
        return CreateAudioSource(go, _audioDatabase.cobraThrowingBrickAC, _audioDatabase.cobraThrowingBrickCurrentVolume, 500);
    }

    public AudioSource PlayCobraFlee(GameObject go)
    {
        _audioDatabase.cobraFleeCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraFleeVolume);
        return CreateAudioSource(go, _audioDatabase.cobraFleeAC, _audioDatabase.cobraFleeCurrentVolume, 500);
    }

    public AudioSource PlayCobraDetection(GameObject go)
    {
        _audioDatabase.cobraDetectionCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraDetectionVolume);
        return CreateAudioSource(go, _audioDatabase.cobraDetectionAC, _audioDatabase.cobraDetectionCurrentVolume, 500);
    }

    public AudioSource PlayCobraDeath(GameObject go)
    {
        _audioDatabase.cobraDeathCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cobraDeathVolume);
        return CreateAudioSource(go, _audioDatabase.cobraDeathAC, _audioDatabase.cobraDeathCurrentVolume, 500);
    }

    public AudioSource PlayMosquitoHum(GameObject go)
    {
        _audioDatabase.mosquitoHumCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.mosquitoHumVolume);
        return CreateAudioSource(go, _audioDatabase.mosquitoHumAC, _audioDatabase.mosquitoHumCurrentVolume, 500, true);
    }

    public AudioSource PlayMosquitoDeath(GameObject go)
    {
        _audioDatabase.mosquitoDeathCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.mosquitoDeathVolume);
        return CreateAudioSource(go, _audioDatabase.mosquitoDeathAC, _audioDatabase.mosquitoDeathCurrentVolume, 500);
    }

    public AudioSource PlayCrocodileAttack(GameObject go)
    {
        _audioDatabase.crocodileAttackCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.crocodileAttackVolume);
        return CreateAudioSource(go, _audioDatabase.crocodileAttackAC, _audioDatabase.crocodileAttackCurrentVolume, 500);
    }

    public AudioSource PlayCrocodileDetection(GameObject go)
    {
        _audioDatabase.crocodileDetectionCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.crocodileDetectionVolume);
        return CreateAudioSource(go, _audioDatabase.crocodileDetectionAC, _audioDatabase.crocodileDetectionCurrentVolume, 500);
    }

    public AudioSource PlayCrocodileDeath(GameObject go)
    {
        _audioDatabase.crocodileDeathCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.crocodileDeathVolume);
        return CreateAudioSource(go, _audioDatabase.crocodileDeathAC, _audioDatabase.crocodileDeathCurrentVolume, 500);
    }
    #endregion

    #region Props Level 1
    public AudioSource PlayWaterfall(GameObject go)
    {
        _audioDatabase.waterfallCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.waterfallVolume);
        return CreateAudioSource(go, _audioDatabase.waterfallAC, _audioDatabase.waterfallCurrentVolume, 500, true);
    }
    #endregion

    #region Props Level 2
    public AudioSource PlayCampfire(GameObject go)
    {
        _audioDatabase.campfireCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.campfireVolume);
        return CreateAudioSource(go, _audioDatabase.campfireAC, _audioDatabase.campfireCurrentVolume, 500, true);
    }

    public AudioSource PlayFactory(GameObject go)
    {
        _audioDatabase.factoryCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.factoryVolume);
        return CreateAudioSource(go, _audioDatabase.factoryAC, _audioDatabase.factoryCurrentVolume, 500, true);
    }

    public AudioSource PlayLightedSign(GameObject go)
    {
        _audioDatabase.lightedSignCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.lightedSignVolume);
        return CreateAudioSource(go, _audioDatabase.lightedSignAC, _audioDatabase.lightedSignCurrentVolume, 500, true);
    }

    public AudioSource PlaySnowplow(GameObject go)
    {
        _audioDatabase.snowplowCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.snowplowVolume);
        return CreateAudioSource(go, _audioDatabase.snowplowAC, _audioDatabase.snowplowCurrentVolume, 500, true);
    }
    #endregion

    #region Props Level 3
    public AudioSource PlayCannonShooting(GameObject go)
    {
        _audioDatabase.cannonShootingCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cannonShootingVolume);
        return CreateAudioSource(go, _audioDatabase.cannonShootingAC, _audioDatabase.cannonShootingCurrentVolume, 500);
    }

    public AudioSource PlayCannonBulletFalling(GameObject go)
    {
        _audioDatabase.cannonBulletFallingCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cannonBulletFallingVolume);
        return CreateAudioSource(go, _audioDatabase.cannonBulletFallingAC, _audioDatabase.cannonBulletFallingCurrentVolume, 500, true);
    }

    public AudioSource PlayCannonBulletExplosion(GameObject go)
    {
        _audioDatabase.cannonBulletExplosionCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cannonBulletExplosionVolume);
        return CreateAudioSource(go, _audioDatabase.cannonBulletExplosionAC, _audioDatabase.cannonBulletExplosionCurrentVolume, 500);
    }

    public AudioSource PlayCannonBroken(GameObject go)
    {
        _audioDatabase.cannonBrokenCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.cannonBrokenVolume);
        return CreateAudioSource(go, _audioDatabase.cannonBrokenAC, _audioDatabase.cannonBrokenCurrentVolume, 500);
    }
    #endregion

    #region NPCs
    public AudioSource PlayBufoDialogue(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 6);
        switch(index)
        {
            case 1:
                _audioDatabase.bufoDialogue1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDialogue1Volume);
                return CreateAudioSource(go, _audioDatabase.bufoDialogue1AC, _audioDatabase.bufoDialogue1CurrentVolume, 500);
            case 2:
                _audioDatabase.bufoDialogue2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDialogue2Volume);
                return CreateAudioSource(go, _audioDatabase.bufoDialogue2AC, _audioDatabase.bufoDialogue2CurrentVolume, 500);
            case 3:
                _audioDatabase.bufoDialogue3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDialogue3Volume);
                return CreateAudioSource(go, _audioDatabase.bufoDialogue3AC, _audioDatabase.bufoDialogue3CurrentVolume, 500);
            case 4:
                _audioDatabase.bufoDialogue4CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDialogue4Volume);
                return CreateAudioSource(go, _audioDatabase.bufoDialogue4AC, _audioDatabase.bufoDialogue4CurrentVolume, 500);
            case 5:
                _audioDatabase.bufoDialogue5CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bufoDialogue5Volume);
                return CreateAudioSource(go, _audioDatabase.bufoDialogue5AC, _audioDatabase.bufoDialogue5CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayBufoDialogue.");
        }
    }

    public AudioSource PlaySedaDialogue(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 6);
        switch (index)
        {
            case 1:
                _audioDatabase.sedaDialogue1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.sedaDialogue1Volume);
                return CreateAudioSource(go, _audioDatabase.sedaDialogue1AC, _audioDatabase.sedaDialogue1CurrentVolume, 500);
            case 2:
                _audioDatabase.sedaDialogue2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.sedaDialogue2Volume);
                return CreateAudioSource(go, _audioDatabase.sedaDialogue2AC, _audioDatabase.sedaDialogue2CurrentVolume, 500);
            case 3:
                _audioDatabase.sedaDialogue3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.sedaDialogue3Volume);
                return CreateAudioSource(go, _audioDatabase.sedaDialogue3AC, _audioDatabase.sedaDialogue3CurrentVolume, 500);
            case 4:
                _audioDatabase.sedaDialogue4CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.sedaDialogue4Volume);
                return CreateAudioSource(go, _audioDatabase.sedaDialogue4AC, _audioDatabase.sedaDialogue4CurrentVolume, 500);
            case 5:
                _audioDatabase.sedaDialogue5CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.sedaDialogue5Volume);
                return CreateAudioSource(go, _audioDatabase.sedaDialogue5AC, _audioDatabase.sedaDialogue5CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlaySedaDialogue.");
        }
    }

    public AudioSource PlayKomodoDialogue(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 6);
        switch (index)
        {
            case 1:
                _audioDatabase.komodoDialogue1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.komodoDialogue1Volume);
                return CreateAudioSource(go, _audioDatabase.komodoDialogue1AC, _audioDatabase.komodoDialogue1CurrentVolume, 500);
            case 2:
                _audioDatabase.komodoDialogue2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.komodoDialogue2Volume);
                return CreateAudioSource(go, _audioDatabase.komodoDialogue2AC, _audioDatabase.komodoDialogue2CurrentVolume, 500);
            case 3:
                _audioDatabase.komodoDialogue3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.komodoDialogue3Volume);
                return CreateAudioSource(go, _audioDatabase.komodoDialogue3AC, _audioDatabase.komodoDialogue3CurrentVolume, 500);
            case 4:
                _audioDatabase.komodoDialogue4CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.komodoDialogue4Volume);
                return CreateAudioSource(go, _audioDatabase.komodoDialogue4AC, _audioDatabase.komodoDialogue4CurrentVolume, 500);
            case 5:
                _audioDatabase.komodoDialogue5CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.komodoDialogue5Volume);
                return CreateAudioSource(go, _audioDatabase.komodoDialogue5AC, _audioDatabase.komodoDialogue5CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayKomodoDialogue.");
        }
    }

    public AudioSource PlayAlironDialogue(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 6);
        switch (index)
        {
            case 1:
                _audioDatabase.alironDialogue1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.alironDialogue1Volume);
                return CreateAudioSource(go, _audioDatabase.alironDialogue1AC, _audioDatabase.alironDialogue1CurrentVolume, 500);
            case 2:
                _audioDatabase.alironDialogue2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.alironDialogue2Volume);
                return CreateAudioSource(go, _audioDatabase.alironDialogue2AC, _audioDatabase.alironDialogue2CurrentVolume, 500);
            case 3:
                _audioDatabase.alironDialogue3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.alironDialogue3Volume);
                return CreateAudioSource(go, _audioDatabase.alironDialogue3AC, _audioDatabase.alironDialogue3CurrentVolume, 500);
            case 4:
                _audioDatabase.alironDialogue4CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.alironDialogue4Volume);
                return CreateAudioSource(go, _audioDatabase.alironDialogue4AC, _audioDatabase.alironDialogue4CurrentVolume, 500);
            case 5:
                _audioDatabase.alironDialogue5CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.alironDialogue5Volume);
                return CreateAudioSource(go, _audioDatabase.alironDialogue5AC, _audioDatabase.alironDialogue5CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayAlironDialogue.");
        }
    }

    public AudioSource PlayBearDialogue(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 5);
        switch (index)
        {
            case 1:
                _audioDatabase.bearDialogue1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bearDialogue1Volume);
                return CreateAudioSource(go, _audioDatabase.bearDialogue1AC, _audioDatabase.bearDialogue1CurrentVolume, 500);
            case 2:
                _audioDatabase.bearDialogue2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bearDialogue2Volume);
                return CreateAudioSource(go, _audioDatabase.bearDialogue2AC, _audioDatabase.bearDialogue2CurrentVolume, 500);
            case 3:
                _audioDatabase.bearDialogue3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bearDialogue3Volume);
                return CreateAudioSource(go, _audioDatabase.bearDialogue3AC, _audioDatabase.bearDialogue3CurrentVolume, 500);
            case 4:
                _audioDatabase.bearDialogue4CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.bearDialogue4Volume);
                return CreateAudioSource(go, _audioDatabase.bearDialogue4AC, _audioDatabase.bearDialogue4CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayBearDialogue.");
        }
    }

    public AudioSource PlayToucanDialogue(GameObject go)
    {
        int index = UnityEngine.Random.Range(1, 6);
        switch (index)
        {
            case 1:
                _audioDatabase.toucanDialogue1CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.toucanDialogue1Volume);
                return CreateAudioSource(go, _audioDatabase.toucanDialogue1AC, _audioDatabase.toucanDialogue1CurrentVolume, 500);
            case 2:
                _audioDatabase.toucanDialogue2CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.toucanDialogue2Volume);
                return CreateAudioSource(go, _audioDatabase.toucanDialogue2AC, _audioDatabase.toucanDialogue2CurrentVolume, 500);
            case 3:
                _audioDatabase.toucanDialogue3CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.toucanDialogue3Volume);
                return CreateAudioSource(go, _audioDatabase.toucanDialogue3AC, _audioDatabase.toucanDialogue3CurrentVolume, 500);
            case 4:
                _audioDatabase.toucanDialogue4CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.toucanDialogue4Volume);
                return CreateAudioSource(go, _audioDatabase.toucanDialogue4AC, _audioDatabase.toucanDialogue4CurrentVolume, 500);
            case 5:
                _audioDatabase.toucanDialogue5CurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.toucanDialogue5Volume);
                return CreateAudioSource(go, _audioDatabase.toucanDialogue5AC, _audioDatabase.toucanDialogue5CurrentVolume, 500);
            default:
                throw new Exception("Index out of range at AudioManager.PlayToucanDialogue.");
        }
    }
    #endregion

    public AudioSource TestDialogue(GameObject go)
    {
        _audioDatabase.testDialogueCurrentVolume = ChangeDialoguesVolumeAsPerModifier(_audioDatabase.testDialogueVolume);
        return CreateAudioSource(go, _audioDatabase.testDialogueAC, _audioDatabase.testDialogueCurrentVolume, 500);
    }

    public AudioSource TestMusic()
    {
        _audioDatabase.testMusicCurrentVolume = ChangeSoundsVolumeAsPerModifier(_audioDatabase.testMusicVolume);
        return CreateMusicAudioSource(_audioDatabase.testMusicAC, _audioDatabase.testMusicCurrentVolume);
    }
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

    private AudioSource CreateAudioSource(GameObject go, AudioClip audioClip, float volume, float maxDistance, bool loop = false)
    {
        AudioSource aS = go.AddComponent<AudioSource>();
        aS.clip = audioClip;
        aS.maxDistance = maxDistance;
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
