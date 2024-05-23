using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointController : MonoBehaviour
{
    public static RespawnPointController instance;

    [SerializeField] private RespawnPoint[] _respawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateRespawnPoints(RespawnPoint respawnPointEnabled)
    {
        foreach(RespawnPoint point in _respawnPoints)
        {
            if (!point.Equals(respawnPointEnabled))
                point.ResetSignal();
        }
    }
}
