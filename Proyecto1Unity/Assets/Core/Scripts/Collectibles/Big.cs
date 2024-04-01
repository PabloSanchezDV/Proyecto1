using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big : CollectibleBase
{
    public override void Collect()
    {
        Debug.Log("Big collected");
    }
}
