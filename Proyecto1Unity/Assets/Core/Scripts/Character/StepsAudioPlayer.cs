using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsAudioPlayer : MonoBehaviour
{
    public void PlayBufoStepsRight()
    {
        AudioManager.instance.PlayBufoStepsRight(gameObject);
    }

    public void PlayBufoStepsLeft()
    {
        AudioManager.instance.PlayBufoStepsLeft(gameObject);
    }
}
