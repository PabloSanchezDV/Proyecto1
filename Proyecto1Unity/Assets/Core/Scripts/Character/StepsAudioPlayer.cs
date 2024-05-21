using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsAudioPlayer : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;

    public void PlayBufoStepsRight()
    {
        AudioManager.instance.PlayBufoStepsRight(gameObject);
    }

    public void PlayBufoStepsLeft()
    {
        AudioManager.instance.PlayBufoStepsLeft(gameObject);
    }
    public void ResetCanAttack()
    {
        _characterManager.CanAttack = true;
    }
}
