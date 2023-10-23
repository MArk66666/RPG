using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField, TextArea(2, 5)] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject prefab;

    public string GetItemName()
    {
        return itemName;
    }

    public string GetItemDescription()
    {
        return description;
    }

    public Sprite GetItemIcon()
    {
        return icon;
    }

    public GameObject GetItemPrefab()
    {
        return prefab;
    }

    public virtual void Interact(Entity entity)
    {

    }
}