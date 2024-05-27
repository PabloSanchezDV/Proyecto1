using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicManager : MonoBehaviour
{
    public static CinematicManager instance;

    private VideoPlayer _videoPlayer;
    public VideoPlayer VideoPlayer { get { return _videoPlayer; } }
    private InputActions _inputActions;

    private void Awake()
    {
        instance = this;

        _videoPlayer = GetComponent<VideoPlayer>();
        Debug.Log(GetCinematicPath(SceneTransitioner.instance.nextCinematic));
        _videoPlayer.clip = VideoImporter.LoadVideo(GetCinematicPath(SceneTransitioner.instance.nextCinematic));
        _videoPlayer.Play();
        _inputActions = new InputActions();
    }

    void Start()
    {
        if(SceneTransitioner.instance.nextSceneIsElectionScene)
            _videoPlayer.loopPointReached += ShowElectionUI;
        else
            _videoPlayer.loopPointReached += GoToNextScene;
        _inputActions.Menu.Enable();        
    }

    private string GetCinematicPath(Cinematic cinematic)
    {
        switch(cinematic)
        {
            case Cinematic.Initial:
                return "Initial";
            case Cinematic.Repair:
                return "Repair";
            case Cinematic.Race:
                return "Race";
            case Cinematic.AfterBoss:
                return "AfterBoss";
            case Cinematic.PacificEnd:
                return "PacificEnd";
            case Cinematic.ViolentEnd:
                return "ViolentEnd";
            default:
                throw new System.Exception("Cinematic can't be loaded. Please make sure to select the proper cinematic option.");
        }
    }

    private void GoToNextScene(VideoPlayer source)
    {
        SceneTransitioner.instance.GoToNextScene();
    }

    private void ShowElectionUI(VideoPlayer source)
    {
        Cursor.lockState = CursorLockMode.None;
        ElectionUI.instance.ShowElectionUI();
    }

    public void PlayGoodEndingCinematic()
    {
        _videoPlayer.clip = VideoImporter.LoadVideo(GetCinematicPath(Cinematic.PacificEnd));
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += GoToNextScene;
        SceneTransitioner.instance.nextCinematic = Cinematic.PacificEnd;
        Debug.Log("PeacefulEnding");
    }

    public void PlayBadEndingCinematic()
    {
        _videoPlayer.clip = VideoImporter.LoadVideo(GetCinematicPath(Cinematic.ViolentEnd));
        _videoPlayer.Play();
        _videoPlayer.loopPointReached += GoToNextScene;
        SceneTransitioner.instance.nextCinematic = Cinematic.ViolentEnd;
        Debug.Log("ViolentEnding");
    }
}
