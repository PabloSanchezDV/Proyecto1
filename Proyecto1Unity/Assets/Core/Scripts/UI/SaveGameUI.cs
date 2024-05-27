using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameUI : MonoBehaviour
{
    [Header("Panel & Pop Ups")]
    [SerializeField] private GameObject _saveGamePanel;
    [SerializeField] private GameObject _saveConfirmationPopUp;
    [SerializeField] private GameObject _savedGamePopUp;
    [SerializeField] private GameObject _loadGamePanel;
    [SerializeField] private GameObject _loadConfirmationPopUp;
    [SerializeField] private GameObject _unableToLoadPopUp;
    [SerializeField] private GameObject _loadedGamePopUp;

    [Header("Save Slots")]
    [SerializeField] private Image _imageSaveSlot1;
    [SerializeField] private Image _imageSaveSlot2;
    [SerializeField] private Image _imageSaveSlot3;
    [SerializeField] private TextMeshProUGUI _levelTextSaveSlot1;
    [SerializeField] private TextMeshProUGUI _levelTextSaveSlot2;
    [SerializeField] private TextMeshProUGUI _levelTextSaveSlot3;
    [SerializeField] private TextMeshProUGUI _infoTextSaveSlot1;
    [SerializeField] private TextMeshProUGUI _infoTextSaveSlot2;
    [SerializeField] private TextMeshProUGUI _infoTextSaveSlot3;

    [Header("Load Slots")]
    [SerializeField] private Image _imageLoadSlot1;
    [SerializeField] private Image _imageLoadSlot2;
    [SerializeField] private Image _imageLoadSlot3;
    [SerializeField] private TextMeshProUGUI _levelTextLoadSlot1;
    [SerializeField] private TextMeshProUGUI _levelTextLoadSlot2;
    [SerializeField] private TextMeshProUGUI _levelTextLoadSlot3;
    [SerializeField] private TextMeshProUGUI _infoTextLoadSlot1;
    [SerializeField] private TextMeshProUGUI _infoTextLoadSlot2;
    [SerializeField] private TextMeshProUGUI _infoTextLoadSlot3;

    [Header("Sprites")]
    [SerializeField] private Sprite _level1Sprite;
    [SerializeField] private Sprite _level2Sprite;
    [SerializeField] private Sprite _level3Sprite;
    [SerializeField] private Sprite _bossSprite;
    [SerializeField] private Sprite _defaultSprite;

    private int _saveSlot = 0;

    void Start()
    {
        if (_saveGamePanel != null)
        {
            _saveGamePanel.SetActive(false);
        }
        _loadGamePanel.SetActive(false);
    }

    #region Show/Hide panels
    public void ShowSaveGamePanel()
    {
        _saveGamePanel.SetActive(true);
    }

    public void HideSaveGamePanel()
    {
        _saveGamePanel.SetActive(false);
    }

    private void ShowSaveConfirmationPopUp()
    {
        _saveConfirmationPopUp.SetActive(true);
    }

    public void HideSaveConfirmationPopUp()
    {
        _saveConfirmationPopUp.SetActive(false);
    }

    private void ShowLoadConfirmationPopUp()
    {
        _loadConfirmationPopUp.SetActive(true);
    }

    public void HideLoadConfirmationPopUp()
    {
        _loadConfirmationPopUp.SetActive(false);
    }

    private void ShowSavedGamePopUp()
    {
        _savedGamePopUp.SetActive(true);
    }

    public void HideSavedGamePopUp()
    {
        _savedGamePopUp.SetActive(false);
    }

    public void ShowLoadGamePanel()
    {
        _loadGamePanel.SetActive(true);
    }

    public void HideLoadGamePanel()
    {
        _loadGamePanel.SetActive(false);
    }

    private void ShowUnableToLoadPopUp()
    {
        _unableToLoadPopUp.SetActive(true);
    }

    public void HideUnableToLoadPopUp()
    {
        _unableToLoadPopUp.SetActive(false);
    }

    private void ShowLoadedGamePopUp()
    {
        _loadedGamePopUp.SetActive(true);
    }

    public void HideLoadedGamePopUp()
    {
        _loadedGamePopUp.SetActive(false);
    }
    #endregion

    #region Save Game
    public void SetSlot1()
    {
        _saveSlot = 1;
        ShowSaveConfirmationPopUp();
    }

    public void SetSlot2()
    {
        _saveSlot = 2;
        ShowSaveConfirmationPopUp();
    }

    public void SetSlot3()
    {
        _saveSlot = 3;
        ShowSaveConfirmationPopUp();
    }

    public void ConfirmSaving()
    {
        SaveDatabase.instance.AddTimeStamp(); 
        SaveSystem.SaveFile(SaveDatabase.instance.data, _saveSlot);
        UpdateSaveSlot(SaveDatabase.instance.data.level);
        UpdateLoadSlot(SaveDatabase.instance.data.level, SaveDatabase.instance.data.timeStamp);
        HideSaveConfirmationPopUp();
        ShowSavedGamePopUp();
    }
    #endregion

    #region Load Game
    public void LoadSlot1()
    {
        _saveSlot = 1;
        ShowLoadConfirmationPopUp();
    }

    public void LoadSlot2()
    {
        _saveSlot = 2;
        ShowLoadConfirmationPopUp();
    }

    public void LoadSlot3()
    {
        _saveSlot = 3;
        ShowLoadConfirmationPopUp();
    }

    public void ConfirmLoading()
    {
        try
        {
            Data data = SaveSystem.LoadFile(_saveSlot, true);
            HideLoadConfirmationPopUp();
            ShowLoadedGamePopUp();
        }
        catch
        {
            HideLoadConfirmationPopUp();
            ShowUnableToLoadPopUp();
        }
    }
    #endregion

    #region Update Slots
    public void UpdateAllSlots()
    {
        for (int i = 1; i <= 3; i++)
        {
            _saveSlot = i;
            try
            {
                Data data = SaveSystem.LoadFile(_saveSlot, false);
                UpdateLoadSlot(data.level, data.timeStamp);
                UpdateSaveSlotOnLoad(data.level, data.timeStamp);
            } catch
            {
                continue;
            }
        }
    }

    public void UpdateSaveSlot(int level)
    {
        switch (_saveSlot)
        {
            case 1:
                _imageSaveSlot1.sprite = GetImageByLevel(level);
                _levelTextSaveSlot1.text = GetLevelWordingByLevel(level);
                _infoTextSaveSlot1.text = System.DateTime.Today.ToString("d") + " - " + System.DateTime.Now.ToString("t");
                break;
            case 2:
                _imageSaveSlot2.sprite = GetImageByLevel(level);
                _levelTextSaveSlot2.text = GetLevelWordingByLevel(level);
                _infoTextSaveSlot2.text = System.DateTime.Today.ToString("d") + " - " + System.DateTime.Now.ToString("t");
                break;
            case 3:
                _imageSaveSlot3.sprite = GetImageByLevel(level);
                _levelTextSaveSlot3.text = GetLevelWordingByLevel(level);
                _infoTextSaveSlot3.text = System.DateTime.Today.ToString("d") + " - " + System.DateTime.Now.ToString("t");
                break;
            default:
                throw new System.Exception("Trying to save in a non existing slot");
        }
    }

    public void UpdateSaveSlotOnLoad(int level, string timeStamp)
    {
        switch (_saveSlot)
        {
            case 1:
                _imageSaveSlot1.sprite = GetImageByLevel(level);
                _levelTextSaveSlot1.text = GetLevelWordingByLevel(level);
                _infoTextSaveSlot1.text = timeStamp;
                break;
            case 2:
                _imageSaveSlot2.sprite = GetImageByLevel(level);
                _levelTextSaveSlot2.text = GetLevelWordingByLevel(level);
                _infoTextSaveSlot2.text = timeStamp;
                break;
            case 3:
                _imageSaveSlot3.sprite = GetImageByLevel(level);
                _levelTextSaveSlot3.text = GetLevelWordingByLevel(level);
                _infoTextSaveSlot3.text = timeStamp;
                break;
            default:
                throw new System.Exception("Trying to save in a non existing slot");
        }
    }

    public void UpdateLoadSlot(int level, string time)
    {
        switch (_saveSlot)
        {
            case 1:
                _imageLoadSlot1.sprite = GetImageByLevel(level);
                _levelTextLoadSlot1.text = GetLevelWordingByLevel(level);
                _infoTextLoadSlot1.text = time;
                break;
            case 2:
                _imageLoadSlot2.sprite = GetImageByLevel(level);
                _levelTextLoadSlot2.text = GetLevelWordingByLevel(level);
                _infoTextLoadSlot2.text = time;
                break;
            case 3:
                _imageLoadSlot3.sprite = GetImageByLevel(level);
                _levelTextLoadSlot3.text = GetLevelWordingByLevel(level);
                _infoTextLoadSlot3.text = time;
                break;
            default:
                throw new System.Exception("Trying to load a non existing slot");
        }
    }

    private Sprite GetImageByLevel(int level)
    {
        switch(level)
        {
            case 1:
                return _level1Sprite;
            case 2:
                return _level2Sprite;
            case 3:
                return _level3Sprite;
            case 4:
                return _bossSprite;
            default:
                return _defaultSprite;
        }
    }

    private string GetLevelWordingByLevel(int level)
    {
        switch(level)
        {
            case 1:
                return "Mundo 1";
            case 2:
                return "Mundo 2";
            case 3:
                return "Mundo 3";
            case 4:
                return "Batalla final";
            default:
                return "...";
        }
    }
    #endregion
}
