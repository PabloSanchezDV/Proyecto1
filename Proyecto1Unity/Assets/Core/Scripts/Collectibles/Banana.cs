using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : CollectibleBase
{
    public int id;

    public override void Collect()
    {
        GameManager.instance.LastBananaID = id;
        EventHolder.instance.onBananaColleted?.Invoke();
    }
}
