using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : CollectibleBase
{
    public override void Collect()
    {
        Debug.Log("Health collected");
    }
}
