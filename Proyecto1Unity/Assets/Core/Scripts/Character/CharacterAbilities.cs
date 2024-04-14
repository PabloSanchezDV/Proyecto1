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
    [SerializeField] private CinemachineInputProvider _provider;

    [Header("Properties")]
    [SerializeField] private float _maxTongueDistance;
    [SerializeField] private float _minTongueDistance;
    [SerializeField] private float _tongueSpeed;
    [SerializeField] private float _homingAttackDistance;
    [SerializeField] private float _maxDragableObjectDistance;
    [SerializeField] private float _homingAttackTongueDistanceModifier = 1.65f;
    [SerializeField] private LayerMask _tongueInteractiveLayerMask;
    [SerializeField] private LayerMask _tongueDragableLayerMask;

    private Transform _tongueReference;
    private Transform _tongueTipReference;
    private TongueTip _tongueTipScript;
    private Rigidbody _rb;

    private float _raycastTongueCheckerDistance;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _tongueReference = _character.GetChild(0).GetChild(0).transform;
        _tongueTipReference = _tongue.GetChild(0).transform;

        _tongueTipScript = _tongueTip.GetComponent<TongueTip>();

        _tongue.gameObject.SetActive(false);
        _tongueTip.gameObject.SetActive(false);

        CalculateRaycastTongueCheckerDistance();
    }

    private void OnEnable()
    {
        CharacterManager.InputActions.Gameplay.ThrowTongue.started += ThrowTongue;
    }

    private void OnDisable()
    {
        CharacterManager.InputActions.Gameplay.ThrowTongue.started -= ThrowTongue;
    }

    private void FixedUpdate()
    {
        if (CharacterManager.IsDragging)
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
        if (!CharacterManager.CanThrowTongue)
            return;

        CharacterManager.CanThrowTongue = false;
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
        CharacterManager.IsDragging = true;
        CharacterManager.DisableJumpOnStartDragging();
        CharacterManager.InputActions.Gameplay.ThrowTongue.started += DettachDragableObjectIA;
    }

    private void DettachDragableObjectIA(InputAction.CallbackContext context)
    {
        DettachDragableObject(_tongueTip.GetChild(0).gameObject);
    }

    private void DettachDragableObject(GameObject dragableObject)
    {
        CharacterManager.InputActions.Gameplay.ThrowTongue.started -= DettachDragableObjectIA;

        dragableObject.transform.parent = null;
        CharacterManager.EnableJumpOnEndDragging();
        CharacterManager.CanMove = false;
        _rb.isKinematic = true;
        _provider.enabled = false;

        CharacterManager.IsDragging = false;
    }

    private void CheckDragableObjectDistance()
    {
        if (CharacterManager.IsDragging)
        {
            if ((_tongueTip.GetChild(0).transform.position - _tongueTip.transform.position).magnitude > _maxDragableObjectDistance)
            {
                DettachDragableObject(_tongueTip.GetChild(0).gameObject);
            }
        }
    }

    IEnumerator TongueOutCoroutine()
    {
        CharacterManager.CanMove = false;
        CharacterManager.IsThrowingTongue = true;
        _provider.enabled = false;
        _rb.isKinematic = true;
        _tongue.transform.localScale = new Vector3(_minTongueDistance, _tongue.transform.localScale.y, _tongue.transform.localScale.z);
        _tongue.position = _tongueReference.position;
        _tongueTip.position = _tongueTipReference.position;

        _tongue.gameObject.SetActive(true);
        _tongueTip.gameObject.SetActive(true);
        _tongueTip.GetComponent<SphereCollider>().enabled = true;

        TongueCollision tongueCollision = CheckTongueCollision();

        CharacterManager.StartThrowTongue();

        switch (tongueCollision.Type)
        {
            case (TongueCollisionType.Enemy):
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
                CharacterManager.StartDragging();
                CharacterManager.CanMove = true;
                _rb.isKinematic = false;
                _provider.enabled = true;

                yield return new WaitUntil(() => CharacterManager.IsDragging == false);
                CharacterManager.EndDragging();

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

        CharacterManager.EndThrowTongue();

        _tongueTipScript.HasCollided = false;
        _tongueTip.gameObject.SetActive(false);
        _provider.enabled = true;
        _tongue.gameObject.SetActive(false);
        CharacterManager.CanMove = true;
        CharacterManager.IsThrowingTongue = false;
        _rb.isKinematic = false;
        yield return null;
    }

    IEnumerator HomingAttack(Vector3 target)
    {
        _tongueTip.GetComponent<SphereCollider>().enabled = false;
        Vector3 direction;
        CharacterManager.StartHomingAttack();

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

        CharacterManager.EndHomingAttack();
        _tongueTipScript.HasCollided = false;
        _tongueTip.gameObject.SetActive(false);
        _provider.enabled = true;
        _tongue.gameObject.SetActive(false);
        CharacterManager.CanMove = true;
        _rb.isKinematic = false;
        CharacterManager.CanThrowTongue = true;
        CharacterManager.IsThrowingTongue = false;
        CharacterManager.JumpOnHomingAttack();
        yield return null;
    }
}
