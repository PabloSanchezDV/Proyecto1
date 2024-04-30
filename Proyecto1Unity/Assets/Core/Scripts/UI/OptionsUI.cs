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

    [Header("Buttons")]
    [SerializeField] private Image _sfxMuteButtonImage;
    [SerializeField] private Image _musicMuteButtonImage;
    [SerializeField] private Image _dialoguesMuteButtonImage;

    [Header("Sliders")]
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _dialoguesSlider;

    private int _panelID = 0; //0 - audio, 1 - video, 2 - controls

    // Start is called before the first frame update
    void Start()
    {
        _audioOptionsUIPanel.SetActive(false);
        _videoOptionsUIPanel.SetActive(false);
        _controlsOptionsUIPanel.SetActive(false);
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

        ShowPanelByID(_panelID);
    }

    public void PrevPanel()
    {
        _panelID--;

        if (_panelID < 0)
            _panelID = 2;

        ShowPanelByID(_panelID);
    }

    private void ShowPanelByID(int id)
    {
        switch (_panelID)
        {
            case 0:
                _audioOptionsUIPanel.SetActive(true);
                _videoOptionsUIPanel.SetActive(false);
                _controlsOptionsUIPanel.SetActive(false);
                break;
            case 1:
                _audioOptionsUIPanel.SetActive(false);
                _videoOptionsUIPanel.SetActive(true);
                _controlsOptionsUIPanel.SetActive(false);
                break;
            case 2:
                _audioOptionsUIPanel.SetActive(false);
                _videoOptionsUIPanel.SetActive(false);
                _controlsOptionsUIPanel.SetActive(true);
                break;
            default:
                throw new System.Exception("Panel ID is out of bounds. Panel ID: " + _panelID);
        }
    }
    #endregion

    #region Audio  
    // DO ALL BY AUDIOMANAGER BASTARD
    public void MuteUnmuteSFX()
    {
        if(AudioManager.instance.AreSoundsEnabled)
        {
            _sfxMuteButtonImage.sprite = _muteSprite;
        }
        else
        {
            _sfxMuteButtonImage.sprite = _unmuteSprite;
        }
        AudioManager.instance.EnableDisableSounds();
    }

    public void ChangeSFXVolume()
    {
        AudioManager.instance.SetSoundsVolumeModifier(_sfxSlider.value);
    }

    public void MuteUnmuteMusic()
    {
        if (AudioManager.instance.IsMusicEnabled)
        {
            _musicMuteButtonImage.sprite = _muteSprite;
        }
        else
        {
            _musicMuteButtonImage.sprite = _unmuteSprite;
        }
        AudioManager.instance.EnableDisableMusic();
    }

    public void ChangeMusicVolume()
    {
        AudioManager.instance.SetMusicVolumeModifier(_musicSlider.value);
    }

    public void MuteUnmuteDialogues()
    {
        if (PrefsManager.instance.GetBool(Pref.AreDialoguesEnabled))
        {
            _dialoguesMuteButtonImage.sprite = _muteSprite;
            PrefsManager.instance.SetBool(Pref.AreDialoguesEnabled, false);
        }
        else
        {
            _dialoguesMuteButtonImage.sprite = _unmuteSprite;
            PrefsManager.instance.SetBool(Pref.AreDialoguesEnabled, true);
        }
    }

    public void ChangeDialoguesVolume()
    {
        PrefsManager.instance.SetFloat(Pref.DialoguesVolume, _dialoguesSlider.value);
    }
    #endregion
}
