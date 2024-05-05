using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTrailToPooling : MonoBehaviour
{
    [SerializeField] private float _returnTime = 1f;

    public void ReturnToPoolOnEnd()
    {
        StartCoroutine(ReturnAfterTime());
    }

    IEnumerator ReturnAfterTime()
    {
        yield return new WaitForSeconds(_returnTime);
        gameObject.SetActive(false);
        TrailPooling.instance.ReturnTrail(gameObject);
    }
}
