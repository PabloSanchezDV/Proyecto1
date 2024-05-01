using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHolder : MonoBehaviour
{
    public static EventHolder instance;

    [NonSerialized] public UnityEvent onPause;
    [NonSerialized] public UnityEvent onUnpause;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        onPause = new UnityEvent();
        onUnpause = new UnityEvent();
    }
}
