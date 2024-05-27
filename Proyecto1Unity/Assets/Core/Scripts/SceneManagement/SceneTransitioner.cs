using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner instance;

    private AsyncOperation _nextSceneLoadOperation;

    private bool _loadComplete = false;
    private bool _cinematicEnded = false;
    
    [NonSerialized] public bool nextSceneIsElectionScene = false;
    [NonSerialized] public Cinematic nextCinematic;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (nextCinematic != Cinematic.AfterBoss)
                GoToSceneAsync(GetNextSceneIndex(), CinematicManager.instance.VideoPlayer);
        }
        else
            DontDestroyOnLoad(gameObject);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(GetNextSceneIndex());
    }

    public void GoToNextScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    private void GoToSceneAsync(int nextSceneBuildIndex, VideoPlayer videoPlayer)
    {
        AsyncOperation _nextSceneLoad = SceneManager.LoadSceneAsync(nextSceneBuildIndex);
        StartCoroutine(CheckForCinematicEndAndChangeSceneOnLoadCoroutine());
        _nextSceneLoadOperation.completed += SetLoadCompletedAsTrue;
        videoPlayer.loopPointReached += SetCinematicEndedAsTrue;
    }

    private void SetLoadCompletedAsTrue(AsyncOperation operation)
    {
        _loadComplete = true;
    }

    private void SetCinematicEndedAsTrue(VideoPlayer source)
    {
        _cinematicEnded = true;
    }

    private void CheckForCinematicEndAndChangeSceneOnLoad()
    {
        StartCoroutine(CheckForCinematicEndAndChangeSceneOnLoadCoroutine());
    }

    private int GetNextSceneIndex()
    {
        switch (nextCinematic)
        {
            case Cinematic.Initial:
                return 1; // Jungle
            case Cinematic.Repair:
                return 2; // Mountain
            case Cinematic.Race:
                return 3; // Train Rush
            case Cinematic.PacificEnd:
                return 0; // Main Menu
            case Cinematic.ViolentEnd:
                return 0; // Main Menu
            default:
                throw new System.Exception("Cinematic can't be loaded. Please make sure to select the proper cinematic option.");
        }
    }

    IEnumerator CheckForCinematicEndAndChangeSceneOnLoadCoroutine()
    {
        _nextSceneLoadOperation.allowSceneActivation = false;
        yield return new WaitUntil(() => _loadComplete && _cinematicEnded);
        _nextSceneLoadOperation.allowSceneActivation = true;
    }
}
