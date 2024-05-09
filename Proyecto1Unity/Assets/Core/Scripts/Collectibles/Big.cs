using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big : CollectibleBase
{
    public override void Collect()
    {
        EventHolder.instance.onBigCollectibleCollected?.Invoke();
    }
}
