using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _audioOptionsUIPanel;
    [SerializeField] private GameObject _videoOptionsUIPanel;
    [SerializeField] private GameObject _controlsOptionsUIPanel;

    [Header("Sprites")]
    [SerializeField] private Sprite _muteSprite;
    [SerializeField] private Sprite _unmuteSprite;

    [Header("Button Images")]
    [SerializeField] private Image _sfxMuteButtonImage;
    [SerializeField] private Image _musicMuteButtonImage;
    [SerializeField] private Image _dialoguesMuteButtonImage;
    [SerializeField] private Image _invertXButtonImage;
    [SerializeField] private Image _invertYButtonImage;

    [Header("Sliders")]
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _dialoguesSlider;
    [SerializeField] private Slider _xSensitivitySlider;
    [SerializeField] private Slider _ySensitivitySlider;

    [Header("Buttons")]
    [SerializeField] private Button _prevAudioButton;
    [SerializeField] private Button _nextAudioButton;
    [SerializeField] private Button _prevVideoButton;
    [SerializeField] private Button _nextVideoButton;
    [SerializeField] private Button _prevControlsButton;
    [SerializeField] private Button _nextControlsButton;

    private int _panelID = 0; //0 - audio, 1 - video, 2 - controls

    // Start is called before the first frame update
    void Start()
    {
        _audioOptionsUIPanel.SetActive(false);
        _videoOptionsUIPanel.SetActive(false);
        _controlsOptionsUIPanel.SetActive(false);
        UpdateUIValuesToSettingsValues();
    }

    #region Show/Hide Panels
    public void ShowAudioOptionsUIPanel()
    {
        _panelID = 0;
        _audioOptionsUIPanel.SetActive(true);
    }

    public void HideAudioOptionsUIPanel()
    {
        _audioOptionsUIPanel.SetActive(false);
    }

    public void HideVideoOptionsUIPanel()
    {
        _videoOptionsUIPanel.SetActive(false);
    }

    public void HideControlsOptionsUIPanel()
    {
        _controlsOptionsUIPanel.SetActive(false);
    }

    public void NextPanel()
    {
        _panelID++;

        if(_panelID > 2)
            _panelID = 0;

        ShowPanelByID(_panelID, false);
    }

    public void PrevPanel()
    {
        _panelID--;

        if (_panelID < 0)
            _panelID = 2;

        ShowPanelByID(_panelID, true);
    }

    private void ShowPanelByID(int id, bool isPrev)
    {
        switch (_panelID)
        {
            case 0:
                _audioOptionsUIPanel.SetActive(true);
                _videoOptionsUIPanel.SetActive(false);
                _controlsOptionsUIPanel.SetActive(false);
                if (isPrev)
                    _prevAudioButton.Select();
                else
                    _nextAudioButton.Select();
                break;
            case 1:
                _audioOptionsUIPanel.SetActive(false);
                _videoOptionsUIPanel.SetActive(true);
                _controlsOptionsUIPanel.SetActive(false);
                if (isPrev)
                    _prevVideoButton.Select();
                else
                    _nextVideoButton.Select();
                break;
            case 2:
                _audioOptionsUIPanel.SetActive(false);
                _videoOptionsUIPanel.SetActive(false);
                _controlsOptionsUIPanel.SetActive(true);
                if (isPrev)
                    _prevControlsButton.Select();
                else
                    _nextControlsButton.Select();
                break;
            default:
                throw new System.Exception("Panel ID is out of bounds. Panel ID: " + _panelID);
        }
    }
    #endregion

    #region Audio  
    public void MuteUnmuteSFX()
    {
        if(SettingsManager.instance.AreSoundsEnabled)
        {
            _sfxMuteButtonImage.sprite = _muteSprite;
        }
        else
        {
            _sfxMuteButtonImage.sprite = _unmuteSprite;
        }
        SettingsManager.instance.EnableDisableSounds();
    }

    public void ChangeSFXVolume()
    {
        SettingsManager.instance.SetSoundsVolumeModifier(_sfxSlider.value);
    }

    public void MuteUnmuteMusic()
    {
        if (SettingsManager.instance.IsMusicEnabled)
        {
            _musicMuteButtonImage.sprite = _muteSprite;
        }
        else
        {
            _musicMuteButtonImage.sprite = _unmuteSprite;
        }
        SettingsManager.instance.EnableDisableMusic();
    }

    public void ChangeMusicVolume()
    {
        SettingsManager.instance.SetMusicVolumeModifier(_musicSlider.value);
    }

    public void MuteUnmuteDialogues()
    {
        if (_dialoguesMuteButtonImage.sprite.Equals(_unmuteSprite))
        {
            _dialoguesMuteButtonImage.sprite = _muteSprite;
        }
        else
        {
            _dialoguesMuteButtonImage.sprite = _unmuteSprite;
        }
        SettingsManager.instance.EnableDisableDialogues();
    }

    public void ChangeDialoguesVolume()
    {
        SettingsManager.instance.SetDialogueVolumeModifier(_dialoguesSlider.value);
    }
    #endregion

    #region Inputs
    public void CheckUncheckXInversion()
    {
        if (_invertXButtonImage.color.a == 100)
        {
            SettingsManager.instance.SwitchXInversion(false);
            _invertXButtonImage.color = new Color(_invertXButtonImage.color.r, _invertXButtonImage.color.g, _invertXButtonImage.color.b, 0);
        }
        else
        {
            SettingsManager.instance.SwitchXInversion(true);
            _invertXButtonImage.color = new Color(_invertXButtonImage.color.r, _invertXButtonImage.color.g, _invertXButtonImage.color.b, 100);
        }
    }

    public void CheckUncheckYInversion()
    {
        if (_invertYButtonImage.color.a == 100)
        {
            SettingsManager.instance.SwitchYInversion(false);
            _invertYButtonImage.color = new Color(_invertYButtonImage.color.r, _invertYButtonImage.color.g, _invertYButtonImage.color.b, 0);
        }
        else
        {
            SettingsManager.instance.SwitchYInversion(true);
            _invertYButtonImage.color = new Color(_invertYButtonImage.color.r, _invertYButtonImage.color.g, _invertYButtonImage.color.b, 100);
        }
    }

    public void ChangeXSensitivity()
    {
        SettingsManager.instance.SetXSensitivity(_xSensitivitySlider.value);
    }

    public void ChangeYSensitivity()
    {
        SettingsManager.instance.SetYSensitivity(_ySensitivitySlider.value);
    }
    #endregion

    #region UpdateUIValues
    private void UpdateUIValuesToSettingsValues()
    {
        UpdateSFXMuteUnmuteButtonToSettingsValue();
        UpdateDialoguesMuteUnmuteButtonToSettingsValue();
        UpdateMusicMuteUnmuteButtonToSettingsValue();

        UpdateSFXSliderToSettingsValue();
        UpdateDialoguesSliderToSettingsValue();
        UpdateMusicSliderToSettingsValue();

        UpdateInvertXButtonToSettingsValue();
        UpdateInvertYButtonToSettingsValue();

        UpdateXSensitivitySliderToSettingsValue();
        UpdateYSensitivitySliderToSettingsValue();
    }

    private void UpdateSFXMuteUnmuteButtonToSettingsValue()
    {
        if (SettingsManager.instance.AreSoundsEnabled)
        {
            _sfxMuteButtonImage.sprite = _unmuteSprite;
        }
        else
        {
            _sfxMuteButtonImage.sprite = _muteSprite;
        }
    }

    private void UpdateDialoguesMuteUnmuteButtonToSettingsValue()
    {
        if (SettingsManager.instance.AreDialoguesEnabled)
        {
            _dialoguesMuteButtonImage.sprite = _unmuteSprite;
        }
        else
        {
            _dialoguesMuteButtonImage.sprite = _muteSprite;
        }
    }

    private void UpdateMusicMuteUnmuteButtonToSettingsValue()
    {
        if (SettingsManager.instance.IsMusicEnabled)
        {
            _musicMuteButtonImage.sprite = _unmuteSprite;
        }
        else
        {
            _musicMuteButtonImage.sprite = _muteSprite;
        }
    }

    private void UpdateSFXSliderToSettingsValue()
    {
        _sfxSlider.value = SettingsManager.instance.SoundsVolumeModifier;
    }

    private void UpdateDialoguesSliderToSettingsValue()
    {
        _dialoguesSlider.value = SettingsManager.instance.DialogueVolumeModifier;
    }

    private void UpdateMusicSliderToSettingsValue()
    {
        _musicSlider.value = SettingsManager.instance.MusicVolumeModifier;
    }

    private void UpdateInvertXButtonToSettingsValue()
    {
        if (SettingsManager.instance.IsXInputInverted)
            _invertXButtonImage.color = new Color(_invertXButtonImage.color.r, _invertXButtonImage.color.g, _invertXButtonImage.color.b, 100);
        else
            _invertXButtonImage.color = new Color(_invertXButtonImage.color.r, _invertXButtonImage.color.g, _invertXButtonImage.color.b, 0);
    }

    private void UpdateInvertYButtonToSettingsValue()
    {
        if (SettingsManager.instance.IsYInputInverted)
            _invertYButtonImage.color = new Color(_invertYButtonImage.color.r, _invertYButtonImage.color.g, _invertYButtonImage.color.b, 100);
        else
            _invertYButtonImage.color = new Color(_invertYButtonImage.color.r, _invertYButtonImage.color.g, _invertYButtonImage.color.b, 0);
    }

    private void UpdateXSensitivitySliderToSettingsValue()
    {
        _xSensitivitySlider.value = SettingsManager.instance.GetXSensitivityUIValue();
    }

    private void UpdateYSensitivitySliderToSettingsValue()
    {
        _ySensitivitySlider.value = SettingsManager.instance.GetYSensitivityUIValue();
    }
    #endregion
}
