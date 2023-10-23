using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateDialogueNodeQuestStep : QuestStep
{
    [SerializeField] private Dialogue targetDialogue;
    [SerializeField] private int nodeIndex;

    private DialogueManager _dialogueManager;

    private void OnEnable()
    {
        _dialogueManager = DialogueManager.Instance;
        _dialogueManager.OnNodeChanged += CheckNode;   
    }

    private void OnDisable()
    {
        _dialogueManager.OnNodeChanged -= CheckNode;
    }

    private void CheckNode(Dialogue dialogue, int index)
    {
        if (dialogue == targetDialogue)
        {
            if (nodeIndex == index)
            {
                FinishQuestStep();
            }
        }   
    }
}