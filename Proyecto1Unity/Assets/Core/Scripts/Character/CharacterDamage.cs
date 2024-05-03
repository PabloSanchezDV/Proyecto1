using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField] private float _invincibilityTime;

    public void ActivateInvincibilityFrames()
    {
        GameManager.instance.IsInvincible = true;
        StartCoroutine(DeactivateInvincibilityFrames());
    }

    IEnumerator DeactivateInvincibilityFrames()
    {
        yield return new WaitForSeconds(_invincibilityTime);
        GameManager.instance.IsInvincible = false;
    }
}
