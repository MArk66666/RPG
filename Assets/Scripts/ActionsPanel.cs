using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionsPanel : MonoBehaviour
{
    [SerializeField] private ActionButton actionButtonPrefab;
    [SerializeField] private SquadController squadController;

    private List<ActionButton> _actionButtons = new List<ActionButton>();

    private void CreateButton(string buttonText, UnityAction action)
    {
        ActionButton actionButton = Instantiate(actionButtonPrefab, transform);
        actionButton.SetupButton(buttonText, action);
        _actionButtons.Add(actionButton);
    }

    public void ClearButtons()
    {
        for (int i = 0; i < _actionButtons.Count; i++)
        {
            if (_actionButtons[i] != null)
            {
                Destroy(_actionButtons[i].gameObject);
            }
        }

        _actionButtons.Clear();
    }

    public void VisualizeActions(GameObject targetObject)
    {
        if (squadController == null)
        {
            Debug.LogError("Squad controller not found, please assign a value!");
            return;
        }

        ClearButtons();

        if (targetObject.GetComponent<IPickable>() != null)
        {
            EntityInventory inventory = squadController.GetCurrentSelectedCharacter().Inventory;
            CreateButton("Pick up", () => squadController.RememberAction(() => 
            targetObject.GetComponent<IPickable>().PickUp(inventory)));
        }

        if (targetObject.GetComponent<IInteractable>() != null)
        {
            CreateButton("Interact", () => squadController.RememberAction(() =>
            targetObject.GetComponent<IInteractable>().Interact()));
        }

        if (targetObject.GetComponent<ITalkable>() != null)
        {
            CreateButton("Talk", () => squadController.RememberAction(() =>
            targetObject.GetComponent<ITalkable>().StartConversation()));
        }

        if (targetObject.GetComponent<IDamageable>() != null)
        {
            CreateButton("Attack", () => squadController.RememberAction(() =>
            targetObject.GetComponent<IDamageable>()));
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
