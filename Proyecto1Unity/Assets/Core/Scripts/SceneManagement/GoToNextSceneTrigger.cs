using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            GameManager.instance.NextScene();
    }
}
