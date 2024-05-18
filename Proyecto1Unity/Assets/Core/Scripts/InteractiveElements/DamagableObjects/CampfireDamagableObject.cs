using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireDamagableObject : DamagableObject
{
    void Start()
    {
        AudioManager.instance.PlayCampfire(gameObject);
    }
}
