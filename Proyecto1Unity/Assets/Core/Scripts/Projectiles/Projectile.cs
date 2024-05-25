using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyAfterTime = 5f;

    private Vector3 _movementDirection;
    private int uses = 0;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _movementDirection = (player.transform.position + new Vector3(0, 0.25f, 0)) - transform.position;
    }

    private void Update()
    {
        transform.Translate(_movementDirection.normalized * _speed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfter());
    }

    IEnumerator DeactivateAfter()
    {
        yield return new WaitForSeconds(_destroyAfterTime);
        uses++;
        ProjectilePooling.instance.ReturnBullet(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CharacterManager cm = other.transform.GetComponent<CharacterManager>();
            if (cm != null)
                cm.HittingEnemy = transform;
            GameManager.instance.TakeDamage(1);
        }
        ProjectilePooling.instance.ReturnBullet(gameObject);
        gameObject.SetActive(false);
    }
}
