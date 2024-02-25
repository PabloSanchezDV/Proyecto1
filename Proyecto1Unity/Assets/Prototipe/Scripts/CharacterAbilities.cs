using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _tongue;
    [SerializeField] private BoxCollider _tongueCollider;
    [SerializeField] private BoxCollider _punchCollider;

    [Header("Properties")]
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _growSpeed;
    [SerializeField] private float _tongueRetractionOnHitSpeed;
    
    private TongueCollisionDetector _detector;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _detector = _tongueCollider.GetComponent<TongueCollisionDetector>();
        _rb = GetComponent<Rigidbody>();

        _tongue.gameObject.SetActive(false);
        _tongueCollider.enabled = false;
        _punchCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            StartCoroutine(TongueOutCoroutine());
        }
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            StartCoroutine(Punch());
        }
    }

    private IEnumerator TongueOutCoroutine()
    {
        Vector3 savedVelocity = _rb.velocity;
        _rb.isKinematic = true;
        _tongue.gameObject.SetActive(true);
        _tongueCollider.enabled=true;

        float thrownDistance = 0;
        while(thrownDistance < _maxDistance && !_detector.HasCollisioned)
        {
            yield return null;
            _tongue.localScale -= Vector3.left * _growSpeed * Time.deltaTime;
            thrownDistance += _growSpeed * Time.deltaTime;
        }

        if (_detector.HasCollisioned )
        {
            Vector3 vectorToTarget = _detector.collisionedObject.transform.position - transform.position;

            while(vectorToTarget.magnitude > 0.5f)
            {
                yield return null;
                transform.Translate(vectorToTarget * _tongueRetractionOnHitSpeed * Time.deltaTime);
                _tongue.localScale += Vector3.left * _tongueRetractionOnHitSpeed * Time.deltaTime;
                thrownDistance -= _tongueRetractionOnHitSpeed * Time.deltaTime;
                vectorToTarget = _detector.collisionedObject.transform.position - transform.position;
            }

            Destroy(_detector.collisionedObject.gameObject);
            _detector.collisionedObject = null;
            _detector.HasCollisioned = false;

            _tongueCollider.enabled = false;
            _rb.isKinematic = false;

            _rb.velocity = Vector3.zero;
            GetComponent<CharacterMovement>().Jump();
        }
        else
        {
            _tongueCollider.enabled = false;
            _rb.isKinematic = false;
            _rb.velocity = savedVelocity;

            while (thrownDistance > 0)
            {
                yield return null;
                _tongue.localScale += Vector3.left * _growSpeed * Time.deltaTime;
                thrownDistance -= _growSpeed * Time.deltaTime;
            }
        }
                
        _tongue.gameObject.SetActive(false);
    }

    private IEnumerator Punch()
    {
        _punchCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _punchCollider.enabled = false;
    }
}
