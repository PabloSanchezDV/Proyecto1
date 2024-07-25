using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuPanel;
    private SaveGameUI _saveGameUI;
    private OptionsUI _optionsUI;

    public void Start()
    {
        _saveGameUI = GetComponent<SaveGameUI>();
        _optionsUI = GetComponent<OptionsUI>();

        _mainMenuPanel.SetActive(true);
    }

    public void StartNewGame()
    {
        SceneTransitioner.instance.nextCinematic = Cinematic.Initial;
        SceneTransitioner.instance.nextSceneIsElectionScene = false;
        SceneTransitioner.instance.GoToNextScene(4);
    }

    public void ShowLoadGamePanel()
    {
        _saveGameUI.ShowLoadGamePanel();
        _mainMenuPanel.SetActive(false);
    }

    public void BackFromLoadGamePanel()
    {
        _saveGameUI.HideLoadGamePanel();
        _mainMenuPanel.SetActive(true);
    }

    public void ShowAudioOptionsPanel()
    {
        _optionsUI.ShowAudioOptionsUIPanel();
        _mainMenuPanel.SetActive(false);
    }

    public void BackFromAudioOptionsPanel()
    {
        _optionsUI.HideAudioOptionsUIPanel();
        _mainMenuPanel.SetActive(true);
    }

    public void BackFromVideoOptionsPanel()
    {
        _optionsUI.HideVideoOptionsUIPanel();
        _mainMenuPanel.SetActive(true);
    }

    public void BackFromControlsOptionsPanel()
    {
        _optionsUI.HideControlsOptionsUIPanel();
        _mainMenuPanel.SetActive(true);
    }

    public void ShowCredits()
    {
        Debug.Log("Showing credits...");
        //SceneManager.LoadScene(credits)
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
