using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteractiveElement : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] _leverGameObjects;

    public void Interact()
    {
        EnableDisableGameObjects();
    }

    private void EnableDisableGameObjects()
    {
        foreach (GameObject go in _leverGameObjects)
        {
            go.SetActive(!go.activeInHierarchy);
        }
    }
}
