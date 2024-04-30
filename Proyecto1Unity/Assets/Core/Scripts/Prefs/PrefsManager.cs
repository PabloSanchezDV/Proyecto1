using System;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    public static PrefsManager instance;

    //Prefs
    //{
    //  AreSoundsEnabled,
    //  IsMusicEnabled,
    //  AreDialoguesEnabled,
    //  SoundsVolume,
    //  MusicVolume,
    //  DialoguesVolume
    //}

    private bool _areSoundsEnabled;
    private bool _isMusicEnabled;
    private bool _areDialoguesEnabled;
    private float _soundsVolume;
    private float _musicVolume;
    private float _dialoguesVolume;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        ResetAllPrefs();
    }

    public void ResetAllPrefs()
    {
        SetBool(Pref.AreSoundsEnabled, true);
        SetBool(Pref.IsMusicEnabled, true);
        SetBool(Pref.AreDialoguesEnabled, true);
    }

    public bool GetBool(Pref pref)
    {
        switch (pref)
        {
            case Pref.AreSoundsEnabled:
                return _areSoundsEnabled;
            case Pref.IsMusicEnabled:
                return _isMusicEnabled;
            case Pref.AreDialoguesEnabled:
                return _areDialoguesEnabled;
            default:
                new Exception("Pref is not assigned or is missing");
                break;
        }

        return false;
    }

    public void SetBool(Pref pref, bool setAs)
    {
        switch (pref)
        {
            case Pref.AreSoundsEnabled:
                _areSoundsEnabled = setAs;
                break;
            case Pref.IsMusicEnabled:
                _isMusicEnabled = setAs;
                break;
            case Pref.AreDialoguesEnabled:
                _areDialoguesEnabled = setAs;
                break;
            default:
                new Exception("Pref is not assigned or is missing");
                break;
        }
    }

    public float GetFloat(Pref pref)
    {
        switch (pref)
        {
            case Pref.SoundsVolume:
                return _soundsVolume;
            case Pref.MusicVolume:
                return _musicVolume;
            case Pref.DialoguesVolume:
                return _dialoguesVolume;
            default:
                new Exception("Pref is not assigned or is missing");
                break;
        }

        return -1f;
    }

    public void SetFloat(Pref pref, float value)
    {
        switch (pref)
        {
            case Pref.SoundsVolume:
                _soundsVolume = value;
                break;
            case Pref.MusicVolume:
                _musicVolume = value;
                break;
            case Pref.DialoguesVolume:
                _dialoguesVolume = value;
                break;
            default:
                new Exception("Pref is not assigned or is missing");
                break;
        }
    }
}