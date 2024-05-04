using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private GameObject _HUDCompletePanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _fader;

    [Header("Dialogue Panel")]
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [Header("Pause Panel")]
    [SerializeField] private GameObject _quitPopUp;

    [Header("HUD")]
    [SerializeField] private Animator _HUDAnimator;
    [SerializeField] private Image _healthImage;
    [SerializeField] private TextMeshProUGUI _bananasText;
    [SerializeField] private TextMeshProUGUI _collectiblesText;
    [SerializeField] private int _maxCollectibles;
    [SerializeField] private Color _maxHealthColor;
    [SerializeField] private Color _2hitsColor;
    [SerializeField] private Color _1hitColor;

    [Header("Properties")]
    [SerializeField] private float _hideHUDTime;

    [Header("Fader")]
    [SerializeField] private Animator _faderAnimator;
    [SerializeField] private float _deathFadeOutTime;

    private OptionsUI _optionsPanel;
    private SaveGameUI _saveGameUI;


    public static UIManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _optionsPanel = GetComponent<OptionsUI>();
        _saveGameUI = GetComponent<SaveGameUI>();
        EventHolder.instance.onPause.AddListener(ShowPausePanel);
        EventHolder.instance.onUnpause.AddListener(HidePausePanel);
        EventHolder.instance.onHurt.AddListener(UpdateAndShowHealthHUD);
        EventHolder.instance.onDeath.AddListener(UpdateHealth);
        EventHolder.instance.onDeath.AddListener(FadeOutAfterDeath);
        EventHolder.instance.onRespawn.AddListener(FadeIn);
    }

    private void OnEnable()
    {
        if(EventHolder.instance != null)
        {
            EventHolder.instance.onPause.AddListener(ShowPausePanel);
            EventHolder.instance.onUnpause.AddListener(HidePausePanel);
            EventHolder.instance.onHurt.AddListener(UpdateAndShowHealthHUD);
            EventHolder.instance.onDeath.AddListener(UpdateHealth);
            EventHolder.instance.onDeath.AddListener(FadeOutAfterDeath);
            EventHolder.instance.onRespawn.AddListener(FadeIn);
        }
    }

    private void OnDisable()
    {
        EventHolder.instance.onPause.RemoveListener(ShowPausePanel);
        EventHolder.instance.onUnpause.RemoveListener(HidePausePanel);
        EventHolder.instance.onHurt.RemoveListener(UpdateAndShowHealthHUD);
        EventHolder.instance.onDeath.RemoveListener(UpdateHealth);
        EventHolder.instance.onDeath.RemoveListener(FadeOutAfterDeath);
        EventHolder.instance.onRespawn.RemoveListener(FadeIn);
    }

    #region Update Panels
    public void UpdateDialoguePanel(string character, string text)
    {
        _characterText.text = character;
        _dialogueText.text = text;
    }

    public void UpdateHUD()
    {
        UpdateHealth();
        _bananasText.text = GameManager.instance.Bananas + "x";
        _collectiblesText.text = GameManager.instance.Collectibles + " / " + _maxCollectibles;
    }

    private void UpdateHealth()
    {
        switch (GameManager.instance.CurrentHealth)
        {
            case 0:
                _healthImage.fillAmount = 0;
                break;
            case 1:
                _healthImage.fillAmount = 0.36f;
                _healthImage.color = _1hitColor;
                break;
            case 2:
                _healthImage.fillAmount = 0.677f;
                _healthImage.color = _2hitsColor;
                break;
            case 3:
                _healthImage.fillAmount = 1;
                _healthImage.color = _maxHealthColor;
                break;
            default:
                throw new System.Exception("Health is out of bounds for UI updating. Health amount: " + GameManager.instance.CurrentHealth.ToString());
        }
    }
    
    private void UpdateAndShowHealthHUD()
    {
        UpdateHealth();
        ShowHealthHUD();
    }
    #endregion

    #region Show / Hide Panels
    public void ShowDialoguePanel()
    {
        _dialoguePanel.SetActive(true);
    }

    public void HideDialoguePanel()
    {
        _dialoguePanel.SetActive(false);
    }

    public void ShowCompleteHUD()
    {
        _HUDAnimator.SetTrigger("ShowComplete");
        Invoke("HideHUD", _hideHUDTime);
    }

    public void HideHUD()
    {
        _HUDAnimator.SetTrigger("Hide");
    }

    public void ShowHealthHUD()
    {
        _HUDAnimator.SetTrigger("ShowHealth");
        Invoke("HideHUD", _hideHUDTime);
    }

    public void ShowCollectiblesHUD()
    {
        _HUDAnimator.SetTrigger("ShowCollectibles");
        Invoke("HideHUD", _hideHUDTime);
    }

    public void ShowBananasHUD()
    {
        _HUDAnimator.SetTrigger("ShowBananas");
        Invoke("HideHUD", _hideHUDTime);
    }

    public void ShowPausePanel()
    {
        _pausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        _pausePanel.SetActive(false);
    }

    public void ShowOptionsPanel()
    {
        _pausePanel.SetActive(false);
        _optionsPanel.ShowAudioOptionsUIPanel();
    }

    public void BackFromAudioOptionsPanel()
    {
        _pausePanel.SetActive(true);
        _optionsPanel.HideAudioOptionsUIPanel();
    }

    public void BackFromVideoOptionsPanel()
    {
        _pausePanel.SetActive(true);
        _optionsPanel.HideVideoOptionsUIPanel();
    }

    public void BackFromControlsOptionsPanel()
    {
        _pausePanel.SetActive(true);
        _optionsPanel.HideControlsOptionsUIPanel();
    }

    public void ShowSaveGamePanel()
    {
        _pausePanel.SetActive(false);
        _saveGameUI.ShowSaveGamePanel();
    }

    public void BackFromSaveGamePanel()
    {
        _pausePanel.SetActive(true);
        _saveGameUI.HideSaveGamePanel();
    }

    public void ShowLoadGamePanel()
    {
        _pausePanel.SetActive(false);
        _saveGameUI.ShowLoadGamePanel();
    }

    public void BackFromLoadGamePanel()
    {
        _pausePanel.SetActive(true);
        _saveGameUI.HideLoadGamePanel();
    }

    public void ShowQuitPopUp()
    {
        _quitPopUp.SetActive(true);
    }

    public void HideQuitPopUp()
    {
        _quitPopUp.SetActive(false);
    }
    #endregion

    #region Pause Panel
    public void ResumeGame()
    {
        EventHolder.instance.onUnpause?.Invoke();
    }

    public void GoToMainMenu()
    {
        //SceneManager.LoadScene("MainMenu");
        Debug.Log("Going to main menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    #endregion

    #region Fade
    public void FadeOut()
    {
        _faderAnimator.SetTrigger("FadeOut");
    }

    public void FadeOutAfterDeath()
    {
        StartCoroutine(FadeOutAfterTime());
    }

    IEnumerator FadeOutAfterTime()
    {
        yield return new WaitForSeconds(_deathFadeOutTime);
        FadeOut();
    }

    public void FadeIn()
    {
        _faderAnimator.SetTrigger("FadeIn");
    }
    #endregion
}
