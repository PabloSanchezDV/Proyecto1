using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour
{
    [HideInInspector]
    public MeleeEnemyManager enemyManager;

    [SerializeField] private float _detectionRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _distanceCheckTime;

    private Transform _player;
    public Transform Player { get { return _player; } }

    private bool _inDetectionRange;

    private bool _isAttacking = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CheckDistance());
    }

    IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(_distanceCheckTime);

        if(!DialogueManager.instance.IsInDialogue) 
        {
            float distance = Vector3.Distance(_player.position, transform.position);

            if (distance <= _detectionRange)
            {
                if (!_inDetectionRange)
                {
                    AudioManager.instance.PlayCrocodileDetection(gameObject);
                    _inDetectionRange = true;
                    enemyManager.Detect();
                }
                else if(distance <= _attackRange)
                {
                    if(!_isAttacking)
                    {
                        AudioManager.instance.PlayCrocodileAttack(gameObject);
                        enemyManager.Attack();
                        _isAttacking = true;
                    }
                }
                else
                {
                    enemyManager.ContinueMove();
                }
            }
            else
            {
                if (_inDetectionRange)
                {
                    _inDetectionRange = false;
                    enemyManager.EndDetect();
                }
            }
        }
        else
        {
            if (_inDetectionRange)
            {
                _inDetectionRange = false;
                enemyManager.EndDetect();
            }
        }

        StartCoroutine(CheckDistance());
    }

    public void EndAttack()
    {
        _isAttacking = false;
    }
}
