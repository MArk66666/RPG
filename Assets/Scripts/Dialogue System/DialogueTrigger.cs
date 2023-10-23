using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DialogueTrigger : MonoBehaviour, ITalkable
{
    [SerializeField] private Dialogue dialogue;

    public void StartConversation()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}