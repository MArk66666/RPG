using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();

    public event Action OnInventoryModified;

    public void AddItem(Item item)
    {
        items.Add(item);
        OnInventoryModified?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        if (HasItem(item) == false)
        {
            return;
        }

        items.Remove(item);
        OnInventoryModified?.Invoke();
    }

    public List<Item> GetItemsList()
    {
        return items;
    }

    public void TransferItem(Item item, EntityInventory targetInventory)
    {
        if (HasItem(item) == false)
        {
            return;
        }

        RemoveItem(item);
        targetInventory.AddItem(item);
    }

    public bool HasItem(Item targetItem)
    {
        foreach (Item item in items)
        {
            if (targetItem == item)
            {
                return true;
            }
        }

        Debug.LogError(targetItem.GetItemName() + " does not exist in items list");
        return false;
    }
}
