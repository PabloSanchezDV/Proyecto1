using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    private CinemachineFreeLook _playerCamera;
    public CinemachineFreeLook PlayerCamera { get { return _playerCamera; } set { _playerCamera = value; } }

    private float _musicVolumeModifier = 1;
    public float MusicVolumeModifier { get { return _musicVolumeModifier; } set { _musicVolumeModifier = value; } }
    private float _soundsVolumeModifier = 1;
    public float SoundsVolumeModifier { get { return _soundsVolumeModifier; } set { _soundsVolumeModifier = value; } }
    private float _dialogueVolumeModifier = 1;
    public float DialogueVolumeModifier { get { return _dialogueVolumeModifier; } set { _dialogueVolumeModifier = value; } }

    private bool _areSoundsEnabled = true;
    public bool AreSoundsEnabled { get { return _areSoundsEnabled; } set { _areSoundsEnabled = value; } }
    private bool _isMusicEnabled = true;
    public bool IsMusicEnabled { get { return _isMusicEnabled; } set { _isMusicEnabled = value; } }
    private bool _areDialoguesEnabled = true;
    public bool AreDialoguesEnabled { get { return _areDialoguesEnabled; } set { _areDialoguesEnabled = value; } }

    private bool _isXInputInverted = false;
    public bool IsXInputInverted { get { return _isXInputInverted; } set { _isXInputInverted = value; } }
    private bool _isYInputInverted = false;
    public bool IsYInputInverted { get { return _isYInputInverted; } set { _isYInputInverted = value; } }
    private float _XInputSensitivity = 1;
    public float XInputSensitivity { get { return _XInputSensitivity; } set { _XInputSensitivity = value; } }
    private float _YInputSensitivity = 1;
    public float YInputSensitivity { get { return _YInputSensitivity; } set { _YInputSensitivity = value; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    #region Audio Methods
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
        AudioManager.instance.ChangeMusicVolume();
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
    }

    public void SetMusicVolumeModifier(float mod)
    {
        if (mod < 0)
        {
            mod = 0;
        }
        else if (mod > 1)
        {
            mod = 1;
        }

        _musicVolumeModifier = mod;

        AudioManager.instance.ChangeMusicVolume();
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
    }
    #endregion

    #region Input Methods
    public void SwitchXInversion(bool status)
    {
        _isXInputInverted = status;
        if(_playerCamera != null)
            _playerCamera.m_XAxis.m_InvertInput = status;
    }

    public void SwitchYInversion(bool status)
    {
        _isYInputInverted = status;
        if (_playerCamera != null)
            _playerCamera.m_YAxis.m_InvertInput = status;
    }

    public void SetXSensitivity(float inputValue)
    {
        if (inputValue < 0.5f)
        {
            _XInputSensitivity = 200f - inputValue * 200f;
        }
        else
        {
            _XInputSensitivity = 200f + inputValue * 100f;
        }
        if (_playerCamera != null)
            _playerCamera.m_XAxis.m_MaxSpeed = _XInputSensitivity;
    }

    public void SetYSensitivity(float inputValue)
    {
        if (inputValue < 0.5f)
        {
            _YInputSensitivity = 1.7f - inputValue * 1.7f;
        }
        else
        {
            _YInputSensitivity = 1.7f + inputValue * 0.85f;
        }
        if (_playerCamera != null)
            _playerCamera.m_YAxis.m_MaxSpeed = _YInputSensitivity;
    }

    public float GetXSensitivityUIValue()
    {
        if(_XInputSensitivity < 100f)
        {
            return (_XInputSensitivity - 200f) / (-200f);
        }
        else
        {
            return (_XInputSensitivity - 200f) / 100f;
        }
    }

    public float GetYSensitivityUIValue()
    {
        if (_YInputSensitivity < 0.85f)
        {
            return (_XInputSensitivity - 1.7f) / (-1.7f);
        }
        else
        {
            return (_XInputSensitivity - 1.7f) / 0.85f;
        }
    }
    #endregion
}
