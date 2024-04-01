using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private bool _isSolved = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _box)
        {
            Interact(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _box)
        {
            Interact(false);
        }
    }

    private void Interact(bool state)
    {
        _isSolved = state;
        Debug.Log("Box detected: " + _isSolved);
    }
}
