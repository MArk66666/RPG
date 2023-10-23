using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour, IPickable
{
    [SerializeField] private Item item;

    public void PickUp(EntityInventory inventory)
    {
        inventory.AddItem(item);
        Destroy(gameObject);
    }
}