using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionQuestStep : QuestStep
{
    [SerializeField] private string targetDoorID;

    private void OnEnable()
    {
        Door.OnDoorInteracted += CheckDoorStatus;
    }

    private void OnDisable()
    {
        Door.OnDoorInteracted -= CheckDoorStatus;
    }

    private void CheckDoorStatus(string doorID)
    {
        if (doorID == targetDoorID)
        {
            FinishQuestStep();
        }
    }
}