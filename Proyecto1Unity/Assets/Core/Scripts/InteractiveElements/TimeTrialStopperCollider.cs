using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrialStopperCollider : MonoBehaviour
{
    [SerializeField] private TimeTrialInteactiveElement _timeTrial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayTimeTrialEnd(GameManager.instance.MainCamera);
            _timeTrial.StopTimeTrial();
            gameObject.SetActive(false);
        }
    }
}
