using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Dialogue _currentDialogue;
    private UserInterfaceManager _uiManager;

    private int _nodeIndex = 0;

    public Action<Dialogue, int> OnNodeChanged;
    public static DialogueManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _uiManager = UserInterfaceManager.Instance;
    }

    private void RedrawDialogueUI()
    {
        _uiManager.UpdateDialogueUI(_nodeIndex);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _currentDialogue = dialogue;
        _uiManager.VisualizeDialogue(dialogue);
    }

    public void EndDialogue()
    {
        _uiManager.DisableDialogueUI();
    }

    public void SetNodeIndex(int index)
    {
        if (index >= _currentDialogue.Nodes.Length)
        {
            EndDialogue();
            return;
        }

        _nodeIndex = index;
        OnNodeChanged?.Invoke(_currentDialogue, index);
        RedrawDialogueUI();
    }
}