using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class CinematicManager : MonoBehaviour
{
    public static CinematicManager instance;

    private VideoPlayer _videoPlayer;
    private InputActions _inputActions;

    private void Awake()
    {
        instance = this;

        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.clip = VideoImporter.LoadVideo(GetCinematicPath());
        _videoPlayer.Play();
        _inputActions = new InputActions();
    }

    void Start()
    {
        _videoPlayer.loopPointReached += GoToNextScene;
        _inputActions.Menu.SkipVideo.started += SkipCinematic;
        _inputActions.Menu.Enable();
    }

    private string GetCinematicPath()
    {
        switch(GameManager.instance.NextCinematic)
        {
            case Cinematic.Initial:
                return "Initial";
            case Cinematic.Repair:
                return "Repair";
            case Cinematic.Race:
                return "Race";
            case Cinematic.PreBoss:
                return "PreBoss";
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

    private void SkipCinematic(InputAction.CallbackContext context)
    {
        GameManager.instance.NextScene();
    }

    private void GoToNextScene(VideoPlayer source)
    {
        GameManager.instance.NextScene();
    }
}
