using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueTip : MonoBehaviour
{
    private bool _hasCollided;
    public bool HasCollided { get { return _hasCollided; } set { _hasCollided = value; } }

    private void OnTriggerEnter(Collider other)
    {
        //Collision against homing attackable elements
        if (other.CompareTag("HomingAttackable"))
        {
            other.GetComponent<Collider>().enabled = false;
        }
        //Collision against dragable object
        if (other.CompareTag("Dragable"))
        {
            other.GetComponent<Collider>().enabled = false;
        }
        //Collision against interactable elements
        if (other.GetComponent<IInteractable>() != null)
        {
            other.GetComponent<IInteractable>().Interact();
        }
        // Collision against any other physical object
        if (!other.CompareTag("Player"))
        {
            _hasCollided = true;
        }
    }
}
