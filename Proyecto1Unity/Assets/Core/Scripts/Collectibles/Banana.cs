using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : CollectibleBase
{
    public override void Collect()
    {
        Debug.Log("Banana collected");
    }
}
