using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _destroyAfterTime = 5f;

    private Vector3 _movementDirection;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _movementDirection = (player.transform.position + new Vector3(0, 0.25f, 0)) - transform.position;
        StartCoroutine(DeactivateAfter());
    }

    private void Update()
    {
        transform.Translate(_movementDirection.normalized * _speed * Time.deltaTime, Space.World);
    }

    IEnumerator DeactivateAfter()
    {
        yield return new WaitForSeconds(_destroyAfterTime);
        gameObject.SetActive(false);
        ProjectilePooling.instance.ReturnBullet(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Damage");
            //DoDamage
        }
        gameObject.SetActive(false);
        ProjectilePooling.instance.ReturnBullet(gameObject);
    }
}
