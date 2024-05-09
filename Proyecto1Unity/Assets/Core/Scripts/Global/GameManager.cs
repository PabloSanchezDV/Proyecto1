using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float _respawnTime;

    private bool _isPaused;
    public bool IsPaused {  get { return _isPaused; } }

    private int _maxHealth = 3;
    private int _currentHealth;
    public int CurrentHealth { get { return _currentHealth; } }

    private int _bananas = 0;
    public int Bananas {  get { return _bananas; } }
    private int _collectibles = 0;
    public int Collectibles { get { return _collectibles; } }
    private int _maxCollectibles = 0;
    public int MaxCollectibles { get { return _maxCollectibles; } }

    private bool _isInvincible = false;
    public bool IsInvincible { get { return _isInvincible; } set { _isInvincible = value; } }

    private Vector3 _lastRespawnPosition;
    public Vector3 LastRespawnPosition { get {  return _lastRespawnPosition; } set { _lastRespawnPosition = value; } }

    private GameObject _player;
    public GameObject Player;

    private Cinematic _nextCinematic;

    public Cinematic NextCinematic { get {  return _nextCinematic; } }

    private CinemachineVirtualCameraBase _playerCamera;
    public CinemachineVirtualCameraBase PlayerCamera { get { return _playerCamera; } set { _playerCamera = value; } }

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
        EventHolder.instance.onDeath.AddListener(Respawn);

        EventHolder.instance.onBananaColleted.AddListener(AddBanana);
        EventHolder.instance.onBigCollectibleCollected.AddListener(AddBigCollectible);

        _player = GameObject.FindGameObjectWithTag("Player");
        if (_player != null)
            _lastRespawnPosition = _player.transform.position;
        _currentHealth = _maxHealth;
        SetMaxCollectiblesAmount();
        UIManager.instance.UpdateHUD();
    }

    #region Pause
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
    #endregion

    #region Health
    public void TakeDamage(int damage)
    {
        if (_isInvincible)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _player.GetComponent<Rigidbody>().isKinematic = true;
            _player.GetComponent<Collider>().enabled = false;
            EventHolder.instance.onDeath?.Invoke();
        }
        else
            EventHolder.instance.onHurt?.Invoke();
    }

    //public void Heal(int healAmount)
    //{
    //    _currentHealth += healAmount;

    //    if (_currentHealth > 3)
    //    {
    //        _currentHealth = 3;
    //        EventHolder.instance.onHeal?.Invoke();
    //    }
    //}
    #endregion

    #region Respawn
    public void Respawn()
    {
        StartCoroutine(RespawnAfter());
    }

    IEnumerator RespawnAfter()
    {
        yield return new WaitForSeconds(_respawnTime);
        _player.GetComponent<Rigidbody>().isKinematic = false;
        _player.GetComponent<Collider>().enabled = true;
        _player.transform.position = LastRespawnPosition;
        _currentHealth = _maxHealth;
        UIManager.instance.UpdateHUD();
        EventHolder.instance.onRespawn?.Invoke();
    }
    #endregion

    #region Scene Management
    public void NextScene()
    {
        UIManager.instance.FadeOut();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        //TODO - LoadSceneAsync always.
        //  Show LoadingScreen when in Gameplay.
        //  Check if the next scene is loaded once the cinematic is done.
        //  If not, show LoadingScreen until then. 

        //  Also set _nextCinematic as it should on every load
        switch(sceneIndex)
        {
            default:
                throw new NotImplementedException("GameManager Scene Management method still not available");
        }
    }
    #endregion

    #region Collectibles
    private void AddBanana()
    {
        _bananas++;
        UIManager.instance.UpdateHUD();
        UIManager.instance.ShowBananasHUD();
    }

    private void AddBigCollectible()
    {
        _collectibles++;

        if(_collectibles >= _maxCollectibles)
            EventHolder.instance.onAllCollectiblesCollected?.Invoke();

        UIManager.instance.UpdateHUD();
        UIManager.instance.ShowCollectiblesHUD();
    }

    private void SetMaxCollectiblesAmount()
    {
        _maxCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
    }
    #endregion
}
