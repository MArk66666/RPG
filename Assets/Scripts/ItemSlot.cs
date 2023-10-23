using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(EventTrigger))]
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text countText;

    private Item _item;
    private InventoryVisualizer _inventoryPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                Use();
                break;

            case PointerEventData.InputButton.Right:
                ShowOtherActions();
                break;
        }
    }

    private void Use()
    {
        SquadController squadController = SquadController.Instance;
        Entity character = squadController.GetCurrentSelectedCharacter();
        EntityInventory entityInventory = _inventoryPanel.GetLinkedInventory();

        _inventoryPanel.RemoveSlot(this);

        if (character.Inventory == entityInventory)
        {
            _item.Interact(character);
            character.Inventory.RemoveItem(_item);
        }
        else
        {
            entityInventory.TransferItem(_item, character.Inventory);
        }
    }

    private void ShowOtherActions()
    {
        
    }

    public void SetupSlot(Item item, InventoryVisualizer inventoryPanel)
    {
        _item = item;
        _inventoryPanel = inventoryPanel;

        itemIcon.sprite = _item.GetItemIcon();
    }
}