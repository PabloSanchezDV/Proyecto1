using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : CollectibleBase
{
    public int id;

    public override void Collect()
    {
        AudioManager.instance.PlayBanana(GameManager.instance.MainCamera);
        GameManager.instance.LastBananaID = id;
        EventHolder.instance.onBananaColleted?.Invoke();
    }
}
