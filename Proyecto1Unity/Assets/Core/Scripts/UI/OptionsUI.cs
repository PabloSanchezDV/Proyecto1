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

    [Header("Buttons")]
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

    private CinemachineFreeLook playerCamera;

    private int _panelID = 0; //0 - audio, 1 - video, 2 - controls

    // Start is called before the first frame update
    void Start()
    {
        _audioOptionsUIPanel.SetActive(false);
        _videoOptionsUIPanel.SetActive(false);
        _controlsOptionsUIPanel.SetActive(false);
        if (GameManager.instance != null)
            playerCamera = (CinemachineFreeLook)GameManager.instance.PlayerCamera;
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
        if (_dialoguesMuteButtonImage.sprite.Equals(_unmuteSprite))
        {
            AudioManager.instance.AreSoundsEnabled = true;
            _dialoguesMuteButtonImage.sprite = _muteSprite;
        }
        else
        {
            AudioManager.instance.AreSoundsEnabled = false;
            _dialoguesMuteButtonImage.sprite = _unmuteSprite;
        }
    }

    public void ChangeDialoguesVolume()
    {
        AudioManager.instance.SetDialogueVolumeModifier(_sfxSlider.value);
    }
    #endregion

    #region Inputs
    public void CheckUncheckXInversion()
    {
        if (_invertXButtonImage.color.a == 100)
        {
            if (playerCamera != null)
                playerCamera.m_XAxis.m_InvertInput = false;
            _invertXButtonImage.color = new Color(_invertXButtonImage.color.r, _invertXButtonImage.color.g, _invertXButtonImage.color.b, 0);
        }
        else
        {
            if (playerCamera != null)
                playerCamera.m_XAxis.m_InvertInput = true;
            _invertXButtonImage.color = new Color(_invertXButtonImage.color.r, _invertXButtonImage.color.g, _invertXButtonImage.color.b, 100);
        }
    }

    public void CheckUncheckYInversion()
    {
        if (_invertYButtonImage.color.a == 100)
        {
            if (playerCamera != null)
                playerCamera.m_YAxis.m_InvertInput = false;
            _invertYButtonImage.color = new Color(_invertYButtonImage.color.r, _invertYButtonImage.color.g, _invertYButtonImage.color.b, 0);
        }
        else
        {
            if (playerCamera != null)
                playerCamera.m_YAxis.m_InvertInput = true;
            _invertYButtonImage.color = new Color(_invertYButtonImage.color.r, _invertYButtonImage.color.g, _invertYButtonImage.color.b, 100);
        }
    }

    public void ChangeXSensitivity()
    {
        if (playerCamera != null)
        {
            if (_xSensitivitySlider.value < 0.5f)
                playerCamera.m_XAxis.m_MaxSpeed = 200f - _xSensitivitySlider.value * 200f;
            else
                playerCamera.m_XAxis.m_MaxSpeed = 200f + _xSensitivitySlider.value * 100f;
        }
    }

    public void ChangeYSensitivity()
    {
        if (playerCamera != null)
        {
            if (_ySensitivitySlider.value < 0.5f)
                playerCamera.m_YAxis.m_MaxSpeed = 1.7f - _ySensitivitySlider.value * 1.7f;
            else
                playerCamera.m_YAxis.m_MaxSpeed = 1.7f + _ySensitivitySlider.value * 0.85f;
        }
    }
    #endregion
}
