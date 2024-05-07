using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAbilities : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _character;
    [SerializeField] private Transform _tongueTip;
    [SerializeField] private Transform _tongue;

    [Header("Properties")]
    [SerializeField] private float _maxTongueDistance;
    [SerializeField] private float _minTongueDistance;
    [SerializeField] private float _tongueSpeed;
    [SerializeField] private float _homingAttackDistance;
    [SerializeField] private float _maxDragableObjectDistance;
    [SerializeField] private float _homingAttackTongueDistanceModifier = 1.65f;
    [SerializeField] private LayerMask _tongueInteractiveLayerMask;
    [SerializeField] private LayerMask _tongueDragableLayerMask;

    private CinemachineInputProvider _provider;
    private Transform _tongueReference;
    private Transform _tongueTipReference;
    private TongueTip _tongueTipScript;
    private Rigidbody _rb;

    private float _raycastTongueCheckerDistance;

    [NonSerialized] public CharacterManager characterManager;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _tongueReference = _character.GetChild(1).transform;
        _tongueTipReference = _tongue.GetChild(0).transform;

        _tongueTipScript = _tongueTip.GetComponent<TongueTip>();
        _provider = GetComponent<CharacterManager>().InputProvider;

        _tongue.gameObject.SetActive(false);
        _tongueTip.gameObject.SetActive(false);

        CalculateRaycastTongueCheckerDistance();
    }

    private void OnEnable()
    {
        characterManager.InputActions.Gameplay.ThrowTongue.started += ThrowTongue;
    }

    private void OnDisable()
    {
        characterManager.InputActions.Gameplay.ThrowTongue.started -= ThrowTongue;
    }

    private void FixedUpdate()
    {
        if (characterManager.IsDragging)
        {
            _tongue.position = _tongueReference.position;
            _tongueTip.position = _tongueTipReference.position;
            Vector3 rotation = _tongueReference.rotation.eulerAngles;
            rotation = new Vector3(rotation.x, rotation.y, 0);
            _tongue.rotation = Quaternion.Euler(rotation);
            _tongueTip.rotation = Quaternion.Euler(rotation);
            CheckDragableObjectDistance();
        }
    }

    private void ThrowTongue(InputAction.CallbackContext context)
    {
        if (!characterManager.CanThrowTongue)
            return;

        characterManager.CanThrowTongue = false;
        StartCoroutine(TongueOutCoroutine());
    }

    private void CalculateRaycastTongueCheckerDistance()
    {
        Vector3 startingScale = _tongue.localScale;
        _tongue.localScale = new Vector3(_maxTongueDistance, _tongue.localScale.y, _tongue.localScale.z);
        _tongueTip.transform.position = _tongueTipReference.transform.position;
        _raycastTongueCheckerDistance = (_tongueTipReference.transform.position - _tongue.transform.position).magnitude + (_tongueTip.transform.GetChild(0).position - _tongueTipReference.transform.position).magnitude;
        Destroy(_tongueTip.transform.GetChild(0).gameObject);
        _tongue.localScale = startingScale;
        _tongueTip.transform.position = _tongueTipReference.transform.position;
    }

    private TongueCollision CheckTongueCollision()
    {
        _tongue.rotation = Quaternion.Euler(0f, _character.rotation.eulerAngles.y + 90f, 0f);
        //Debug.DrawRay(transform.position, _tongue.right * _raycastTongueCheckerDistance, Color.red, 10f);
        if (Physics.Raycast(_tongue.transform.position, _tongue.right, out RaycastHit hit, _raycastTongueCheckerDistance, _tongueInteractiveLayerMask))
        {
            TongueCollision collision = new TongueCollision(TongueCollisionType.Enemy, hit.transform.gameObject);
            return collision;
        }
        else if (Physics.Raycast(_tongue.transform.position, _tongue.right, out hit, _raycastTongueCheckerDistance, _tongueDragableLayerMask))
        {
            TongueCollision collision = new TongueCollision(TongueCollisionType.Dragable, hit.transform.gameObject);
            return collision;
        }
        else 
        {
            TongueCollision collision = new TongueCollision(TongueCollisionType.None);
            return collision;
        }
    }

    private void AttachDragableObject(GameObject dragableObject)
    {
        dragableObject.transform.SetParent(_tongueTip, true);
        _tongueTip.GetComponent<Collider>().enabled = false;
        characterManager.IsDragging = true;
        characterManager.DisableJumpOnStartDragging();
        characterManager.InputActions.Gameplay.ThrowTongue.started += DettachDragableObjectIA;
    }

    private void DettachDragableObjectIA(InputAction.CallbackContext context)
    {
        DettachDragableObject(_tongueTip.GetChild(1).gameObject);
    }

    private void DettachDragableObject(GameObject dragableObject)
    {
        characterManager.InputActions.Gameplay.ThrowTongue.started -= DettachDragableObjectIA;

        dragableObject.transform.parent = null;
        characterManager.EnableJumpOnEndDragging();
        characterManager.CanMove = false;
        _rb.isKinematic = true;
        _provider.enabled = false;

        characterManager.IsDragging = false;
    }

    private void CheckDragableObjectDistance()
    {
        if (characterManager.IsDragging)
        {
            if ((_tongueTip.GetChild(0).transform.position - _tongueTip.transform.position).magnitude > _maxDragableObjectDistance)
            {
                DettachDragableObject(_tongueTip.GetChild(1).gameObject);
            }
        }
    }

    IEnumerator TongueOutCoroutine()
    {
        characterManager.CanMove = false;
        characterManager.IsThrowingTongue = true;
        _provider.enabled = false;
        _rb.isKinematic = true;
        _tongue.transform.localScale = new Vector3(_minTongueDistance, _tongue.transform.localScale.y, _tongue.transform.localScale.z);
        _tongue.position = _tongueReference.position;
        _tongueTip.position = _tongueTipReference.position;

        _tongue.gameObject.SetActive(true);
        _tongueTip.gameObject.SetActive(true);
        _tongueTip.GetComponent<SphereCollider>().enabled = true;

        TongueCollision tongueCollision = CheckTongueCollision();

        characterManager.StartThrowTongue();

        switch (tongueCollision.Type)
        {
            case (TongueCollisionType.Enemy):
                characterManager.EnemyAnimatorControllerOnHomingAttack = tongueCollision.Target.GetComponent<EnemyAnimatorController>();
                while (_tongue.transform.localScale.x < _maxTongueDistance && (_tongueTip.transform.position - tongueCollision.Target.transform.position).magnitude > 0.1f)
                {
                    _tongue.position = _tongueReference.position;
                    _tongue.LookAt(tongueCollision.Target.transform.position);
                    _tongue.Rotate(0f, -90f, 0f);
                    _tongue.transform.localScale += Vector3.right * _tongueSpeed * Time.deltaTime;
                    _tongueTip.position = _tongueTipReference.position;
                    yield return null;
                }

                StartCoroutine(HomingAttack(tongueCollision.Target.transform.position));
                break;
            case (TongueCollisionType.Dragable):
                while (_tongue.transform.localScale.x < _maxTongueDistance && (_tongueTip.transform.position - tongueCollision.Target.transform.position).magnitude > 0.1f)
                {
                    _tongue.position = _tongueReference.position;
                    _tongue.LookAt(tongueCollision.Target.transform.position);
                    _tongue.Rotate(0f, -90f, 0f);
                    _tongue.transform.localScale += Vector3.right * _tongueSpeed * Time.deltaTime;
                    _tongueTip.position = _tongueTipReference.position;
                    yield return null;
                }

                AttachDragableObject(tongueCollision.Target);
                characterManager.StartDragging();
                characterManager.CanMove = true;
                _rb.isKinematic = false;
                _provider.enabled = true;

                yield return new WaitUntil(() => characterManager.IsDragging == false);
                characterManager.EndDragging();

                StartCoroutine(TongueInCoroutine());
                break;
            default:
                while (_tongue.transform.localScale.x < _maxTongueDistance && !_tongueTipScript.HasCollided)
                {
                    _tongue.position = _tongueReference.position;
                    _tongue.rotation = Quaternion.Euler(0f, _character.rotation.eulerAngles.y + 90f, 0f);
                    _tongue.transform.localScale += Vector3.right * _tongueSpeed * Time.deltaTime;
                    _tongueTip.transform.position = _tongueTipReference.transform.position;
                    yield return null;
                }

                StartCoroutine(TongueInCoroutine());
                break;
        }
    }

    IEnumerator TongueInCoroutine()
    {
        _tongueTip.GetComponent<SphereCollider>().enabled = false;
        while (_tongue.transform.localScale.x > _minTongueDistance)
        {
            _tongue.position = _tongueReference.position;
            _tongue.rotation = Quaternion.Euler(0f, _character.rotation.eulerAngles.y + 90f, 0f);
            _tongue.transform.localScale -= Vector3.right * _tongueSpeed * Time.deltaTime;
            _tongueTip.transform.position = _tongueTipReference.transform.position;
            yield return null;
        }

        characterManager.EndThrowTongue();

        _tongueTipScript.HasCollided = false;
        _tongueTip.gameObject.SetActive(false);
        _provider.enabled = true;
        _tongue.gameObject.SetActive(false);
        characterManager.CanMove = true;
        characterManager.IsThrowingTongue = false;
        _rb.isKinematic = false;
        yield return null;
    }

    IEnumerator HomingAttack(Vector3 target)
    {
        _tongueTip.GetComponent<SphereCollider>().enabled = false;
        Vector3 direction;
        characterManager.StartHomingAttack();

        while (_tongue.transform.localScale.x > _homingAttackDistance)
        {
            direction = (target - transform.position).normalized;
            transform.Translate(direction * _tongueSpeed * Time.deltaTime);
            _tongue.position = _tongueReference.position;
            _tongue.LookAt(target);
            _tongue.Rotate(0f, -90f, 0f);
            _tongue.transform.localScale -= Vector3.right * _tongueSpeed * _homingAttackTongueDistanceModifier * Time.deltaTime;
            _tongueTip.transform.position = _tongueTipReference.transform.position;
            yield return null;
        }

        characterManager.EndHomingAttack();
        _tongueTipScript.HasCollided = false;
        _tongueTip.gameObject.SetActive(false);
        _provider.enabled = true;
        _tongue.gameObject.SetActive(false);
        characterManager.CanMove = true;
        _rb.isKinematic = false;
        characterManager.CanThrowTongue = true;
        characterManager.IsThrowingTongue = false;
        characterManager.JumpOnHomingAttack();
        yield return null;
    }
}
