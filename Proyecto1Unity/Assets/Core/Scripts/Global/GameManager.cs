using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool _isPaused;
    public bool IsPaused {  get { return _isPaused; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EventHolder.instance.onPause.AddListener(Pause);
        EventHolder.instance.onUnpause.AddListener(Unpause);
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0.0f;
        _isPaused = true;
    }

    private void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        _isPaused = false;
    }
}
