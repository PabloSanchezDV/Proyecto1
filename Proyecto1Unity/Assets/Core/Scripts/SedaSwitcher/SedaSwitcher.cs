using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SedaSwitcher : MonoBehaviour
{
    public static SedaSwitcher instance;

    [SerializeField] private GameObject[] _sedaSwitcherColliders;
    [SerializeField] private GameObject _initialSeda;

    [SerializeField] private GameObject[] _sedaGameObjects;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        _sedaGameObjects = GameObject.FindGameObjectsWithTag("Seda");

        foreach(GameObject switcherCollider in _sedaSwitcherColliders)
        {
            switcherCollider.SetActive(true);
        }

        SwitchSedas(_initialSeda);
    }

    public void SwitchSedas(GameObject sedaToActivate)
    {
        foreach (GameObject seda in _sedaGameObjects)
        {
            if (ReferenceEquals(seda, sedaToActivate))
            {
                sedaToActivate.SetActive(true);
            }
            else
            {
                seda.SetActive(false);
            }
        }
    }
}
