using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private MultipleParticleSystemManager _multipleParticleSystemManager;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private GameObject _landMark;
    [SerializeField] private GameObject _explosionCollider;
    
    [SerializeField] private LayerMask _landMarkCollidingLayers;
    [SerializeField] private float _height;
    [SerializeField] private float _explosionTime;
    [SerializeField] private float _fallingSpeedMultiplier;
    [SerializeField] private float _maxRotationSpeed;

    [NonSerialized] public Transform playerTransform;
    private Rigidbody _rb;
    private bool _hasCollided = false;
    private float[] _rotationSpeeds;

    private AudioSource _fallingSoundAS;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rotationSpeeds = new float[3];

        _rotationSpeeds[0] = UnityEngine.Random.Range(-_maxRotationSpeed, _maxRotationSpeed);
        _rotationSpeeds[1] = UnityEngine.Random.Range(-_maxRotationSpeed, _maxRotationSpeed);
        _rotationSpeeds[2] = UnityEngine.Random.Range(-_maxRotationSpeed, _maxRotationSpeed);

        _explosionCollider.SetActive(false);
    }

    public void Initialize()
    {
        transform.position = new Vector3(playerTransform.position.x, _height, playerTransform.position.z);
        MoveLandMarkToGround();
        _fallingSoundAS = AudioManager.instance.PlayCannonBulletFalling(gameObject);
    }

    private void FixedUpdate()
    {
        // Stops the bullets while cameras are transitioning
        if (!CannonManager.instance.CanShoot) 
        {
            _rb.velocity = Vector3.zero;
            return;
        }
        if (!_hasCollided)
        {
            _rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * _fallingSpeedMultiplier;
            _bomb.transform.Rotate(new Vector3(_rotationSpeeds[0] * Time.deltaTime, _rotationSpeeds[1] * Time.deltaTime, _rotationSpeeds[2] * Time.deltaTime));
        }
    }

    private void MoveLandMarkToGround()
    {
        Ray ray = new Ray(transform.position + Vector3.down * 0.5f, Vector3.down);
        float maxHeight = -1000f;

        _landMark.transform.parent = null;    

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _landMarkCollidingLayers))
        {
            if (hit.point.y > maxHeight)
            {
                _landMark.transform.position = hit.point + new Vector3(0, 0.001f, 0);
                maxHeight = hit.point.y;
            }
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterManager cm = other.transform.GetComponent<CharacterManager>();
            if (cm != null)
                cm.HittingEnemy = transform;
            GameManager.instance.TakeDamage(1);
        }

        AudioManager.instance.StopAudioSource(_fallingSoundAS);
        AudioManager.instance.PlayCannonBulletExplosion(_multipleParticleSystemManager.gameObject);
        _hasCollided = true;
        _rb.velocity = Vector3.zero;
        _bomb.SetActive(false);
        _explosionCollider.SetActive(true);
        _landMark.SetActive(false);
        _multipleParticleSystemManager.PlayAndDettachParentWhilePlaying();
        StartCoroutine(ReturnToPoolAfterTime(_explosionTime));
    }

    IEnumerator ReturnToPoolAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        CannonPooling.instance.ReturnCannonBullet(gameObject);
        _hasCollided = false;
        _bomb.SetActive(true);
        _explosionCollider.SetActive(false);
        _landMark.transform.SetParent(transform);
        _landMark.transform.localPosition = Vector3.zero;
        _landMark.SetActive(true);
        gameObject.SetActive(false);
    }
}
