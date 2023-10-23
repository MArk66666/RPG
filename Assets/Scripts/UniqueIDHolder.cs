using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

public class UniqueIDHolder : MonoBehaviour
{
    [SerializeField, ReadOnly, InspectorIcon(InspectorIcon.Hierarchy)] private string id;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        if (id == string.Empty || IsUnique(id))
        {
            //ResetID();
        }
        #endif
    }

    [ContextMenu("Force set ID")]
    private void ResetID()
    {
        id = GenerateID();
    }

    private string GenerateID()
    {
        string newID = Guid.NewGuid().ToString();
        return newID;
    }

    public static bool IsUnique(string id)
    {
        return Resources.FindObjectsOfTypeAll<UniqueIDHolder>().Count(x => x.id == id) == 1;
    }

    public string GetID()
    {
        return id;
    }
}