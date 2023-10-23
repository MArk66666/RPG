using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[System.Serializable]
public class DialogueNode
{
    [Indent(-1), ReadOnly, InspectorIcon(InspectorIcon.Hierarchy)] public int id;

    [Indent(-1)] public Personality Speaker;
    [Indent(-1), TextArea(2, 5)] public string NodeText;
    [Space] public DialogueChoice[] Choices;

    public void UpdateID(int newID)
    {
        id = newID;
    }
}