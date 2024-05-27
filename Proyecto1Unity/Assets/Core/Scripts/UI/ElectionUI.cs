using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectionUI : MonoBehaviour
{
    public static ElectionUI instance;

    [SerializeField] private GameObject _electionPanel;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _electionPanel.SetActive(false);
    }

    public void ShowElectionUI()
    {
        _electionPanel.SetActive(true);
    }

    public void AttackButtonPressed()
    {
        _electionPanel.SetActive(false);
        CinematicManager.instance.PlayBadEndingCinematic();
    }

    public void LeaveButtonPressed()
    {
        _electionPanel.SetActive(false);
        CinematicManager.instance.PlayGoodEndingCinematic();
    }
}
