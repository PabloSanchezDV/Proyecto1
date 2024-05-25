using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialInteactiveElement : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] _trialGameObjects;
    [SerializeField] private float _trialTime;
    [SerializeField] private float _urgentSoundMarginTime;

    private Animator _animator;
    private bool _isActive = false;
    private AudioSource _normalTTAS;
    private AudioSource _urgentTTAS;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        EventHolder.instance.onRespawn.AddListener(StopTimeTrial);
    }

    private void OnEnable()
    {
        EventHolder.instance.onRespawn.AddListener(StopTimeTrial);
    }

    private void OnDisable()
    {
        EventHolder.instance.onRespawn.RemoveListener(StopTimeTrial);
    }

    public void Interact()
    {
        if (_isActive)
            return;

        AudioManager.instance.PlayButton(gameObject);
        _isActive = true;
        if(_animator != null)
            _animator.SetTrigger("ChangeState");
        StartCoroutine(TimeTrialCoroutine());
    }

    IEnumerator TimeTrialCoroutine()
    {
        SetGameObjectsAs(true);
        _normalTTAS = AudioManager.instance.PlayTimeTrialNormal(GameManager.instance.MainCamera);
        yield return new WaitForSeconds(_trialTime - _urgentSoundMarginTime);
        if (_isActive)
        {
            AudioManager.instance.StopAudioSource(_normalTTAS);
            _urgentTTAS = AudioManager.instance.PlayTimeTrialUrgent(GameManager.instance.MainCamera);
            yield return new WaitForSeconds(_urgentSoundMarginTime);
            if (_isActive)
            {
                AudioManager.instance.StopAudioSource(_urgentTTAS);
                AudioManager.instance.PlayTimeTrialEnd(GameManager.instance.MainCamera);
                if (_animator != null)
                    _animator.SetTrigger("ChangeState");
                SetGameObjectsAs(false);
                _isActive = false;
            }
        }
    }

    private void SetGameObjectsAs(bool setAs)
    {
        foreach(GameObject go in _trialGameObjects)
        {
            go.SetActive(setAs);
        }
    }

    public void StopTimeTrial()
    {
        if (_isActive)
        {
            SetGameObjectsAs(false);
            if (_normalTTAS != null)
                AudioManager.instance.StopAudioSource(_normalTTAS);
            if (_urgentTTAS != null)
                AudioManager.instance.StopAudioSource(_urgentTTAS);
            if (_animator != null)
                _animator.SetTrigger("ChangeState");
            _isActive = false;
        }
    }
}
