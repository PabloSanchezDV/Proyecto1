using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big : CollectibleBase
{
    public int id;

    public override void Collect()
    {
        GameManager.instance.LastCollectibleID = id;
        EventHolder.instance.onBigCollectibleCollected?.Invoke();
    }
}
