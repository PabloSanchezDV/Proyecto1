using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SedaSwitcherCollider : MonoBehaviour
{
    [SerializeField] private GameObject _sedaToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SedaSwitcher.instance.SwitchSedas(_sedaToActivate);
        }
    }
}
