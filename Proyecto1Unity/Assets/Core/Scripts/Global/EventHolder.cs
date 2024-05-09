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
    [NonSerialized] public UnityEvent onHurt;
    [NonSerialized] public UnityEvent onDeath;
    [NonSerialized] public UnityEvent onRespawn;
    [NonSerialized] public UnityEvent onStartDialogue;
    [NonSerialized] public UnityEvent onEndDialogue;
    [NonSerialized] public UnityEvent onBananaColleted;
    [NonSerialized] public UnityEvent onBigCollectibleCollected;
    [NonSerialized] public UnityEvent onAllCollectiblesCollected;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        onPause = new UnityEvent();
        onUnpause = new UnityEvent();

        onHurt = new UnityEvent();
        onDeath = new UnityEvent();
        onRespawn = new UnityEvent();

        onStartDialogue = new UnityEvent();
        onEndDialogue = new UnityEvent();

        onBananaColleted = new UnityEvent();
        onBigCollectibleCollected = new UnityEvent();
        onAllCollectiblesCollected = new UnityEvent();
    }
}
