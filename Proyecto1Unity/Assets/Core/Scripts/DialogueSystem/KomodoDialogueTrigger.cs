using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomodoDialogueTrigger : DialogueTrigger
{
    [SerializeField] private PressurePlate[] _pressurePlates;
    [SerializeField] private GameObject[] _mosquitos;
    [SerializeField] private int _idAfterCompletion;
    private bool _arePressurePlatesEnabled = false;

    protected override void Talk()
    {
        EventHolder.instance.onStartDialogue?.Invoke();
        _cameraRef.transform.localPosition = (player.transform.position - transform.position) / 2;
        _cameraRef.transform.localPosition = new Vector3(-_cameraRef.transform.localPosition.x,
                                                           -0.5f,
                                                            -_cameraRef.transform.localPosition.z);
        CameraSwitcher.instance.SwitchCamera(_virtualCamera, _cameraRef);
        StartDialogue();
        if (_overwriteAtEnd)
            OverwriteAtEnd();
        if (!_arePressurePlatesEnabled)
            EnablePressurePlatesAndMosquitoesAtEnd();
    }

    public void EnablePressurePlatesAndMosquitoesAtEnd()
    {
        foreach(PressurePlate pressurePlate in _pressurePlates)
        {
            pressurePlate.SetConditionCompleteAs(true);
            pressurePlate.ResetBox();
        }

        foreach(GameObject mosquito in _mosquitos)
        {
            mosquito.SetActive(true);
        }

        _arePressurePlatesEnabled = true;
        EventHolder.instance.onAllCollectiblesCollected.AddListener(ChangeDialogueLines);
    }

    private void ChangeDialogueLines()
    {
        _id = _idAfterCompletion;
        EventHolder.instance.onEndDialogue.AddListener(GameManager.instance.NextScene);
    }
}
