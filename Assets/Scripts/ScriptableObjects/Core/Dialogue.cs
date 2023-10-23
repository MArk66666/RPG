using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueNode[] Nodes;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        for (int i = 0; i < Nodes.Length; i++)
        {
            DialogueNode node = Nodes[i];
            node.UpdateID(i);
        }
        #endif
    }
}