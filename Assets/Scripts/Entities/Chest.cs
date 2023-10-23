using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Entity, IInteractable
{
    public void Interact()
    {
        ShowInventoryPanel();     
    }

    private void ShowInventoryPanel()
    {
        UserInterfaceManager.Instance.VisualizeEntityInventory(this);
    }
}