using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryVisualizer : MonoBehaviour
{
    [SerializeField] private Transform itemsHolder;
    
    private List<ItemSlot> _slots = new List<ItemSlot>();
    private EntityInventory _inventory;

    private void ClearSlots()
    {
        List<ItemSlot> slotsToRemove = new List<ItemSlot>(_slots);

        foreach (ItemSlot slot in slotsToRemove)
        {
            RemoveSlot(slot);
        }

        _slots.Clear();
    }

    public void AddSlot(Item item)
    {
        ItemSlot slot = Instantiate(UserInterfaceManager.Instance.GetItemSlotPrefab(), itemsHolder);
        slot.SetupSlot(item, this);
        _slots.Add(slot);
    }

    public void RemoveSlot(ItemSlot slot)
    {
        _slots.Remove(slot);
        Destroy(slot.gameObject);
    }

    public void AssignInventory(EntityInventory inventory)
    {
        ClearSlots();
        _inventory = inventory;

        foreach (Item item in _inventory.GetItemsList())
        {
            AddSlot(item);
        }
    }

    public EntityInventory GetLinkedInventory()
    {
        return _inventory;
    }
}