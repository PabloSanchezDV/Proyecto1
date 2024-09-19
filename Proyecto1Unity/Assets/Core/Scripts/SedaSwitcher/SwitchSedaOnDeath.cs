using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSedaOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject _sedaToActivate;
    private RespawnPoint _spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        _spawnPoint = GetComponent<RespawnPoint>();
        EventHolder.instance.onDeath.AddListener(OnDeathSwitchSeda);
    }

    private void OnEnable()
    {
        if(EventHolder.instance != null)
            EventHolder.instance.onDeath.AddListener(OnDeathSwitchSeda);
    }

    private void OnDisable()
    {
        EventHolder.instance.onDeath.RemoveListener(OnDeathSwitchSeda);
    }

    void OnDeathSwitchSeda()
    {
        if (_spawnPoint != null)
        {
            if(_spawnPoint.IsActive)
            {
                SedaSwitcher.instance.SwitchSedas(_sedaToActivate);
            }
        }
    }
}
