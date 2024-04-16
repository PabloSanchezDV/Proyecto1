using System;
using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    public static PrefsManager instance;

    //Prefs
    //{
    //  AreSoundsEnabled,
    //  IsMusicEnabled
    //}

    private bool _areSoundsEnabled;
    private bool _isMusicEnabled;

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
    }

    public bool GetBool(Pref pref)
    {
        switch (pref)
        {
            case Pref.AreSoundsEnabled:
                return _areSoundsEnabled;
            case Pref.IsMusicEnabled:
                return _isMusicEnabled;
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
            default:
                new Exception("Pref is not assigned or is missing");
                break;
        }
    }
}