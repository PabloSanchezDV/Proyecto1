using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWaterfallSoundAtStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayWaterfall(gameObject);
    }
}
