using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableLightedSign : ActivableObject
{
    GameObject _screen;
    private bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //_screen = transform.GetChild(0).gameObject;
    }

    public override void Activate()
    {
        if (!_isActive)
            Debug.Log("Activating lighted screen");
            //_screen.SetActive(true);
    }
}
