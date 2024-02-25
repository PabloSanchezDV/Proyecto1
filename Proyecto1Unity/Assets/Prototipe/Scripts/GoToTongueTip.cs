using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTongueTip : MonoBehaviour
{
    [SerializeField] private GameObject _tongueTip;

    void Update()
    {
        transform.position = _tongueTip.transform.position;
    }
}
