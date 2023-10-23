using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindItemsQuestStep : QuestStep
{
    [SerializeField] private List<Item> requiredItemsList;
    private SquadController _squadController;
    private Entity _selectedCharacter;

    private List<Item> _availableItems = new List<Item>();

    private void Update()
    {
        Entity selectedCharacter = _squadController.GetCurrentSelectedCharacter();
        if (selectedCharacter != _selectedCharacter)
        {
            ChangeTraceableCharacter(selectedCharacter);
        }
    }

    private void OnEnable()
    {
        _squadController = SquadController.Instance;
        ChangeTraceableCharacter(_squadController.GetCurrentSelectedCharacter());
    }

    private void OnDisable()
    {
        if (_selectedCharacter != null)
        {
            _selectedCharacter.Inventory.OnInventoryModified -= CheckItemsPresence;
        }
    }

    private void ChangeTraceableCharacter(Entity character)
    {
        if (character == null) return;

        if (_selectedCharacter != null)
        {
            _selectedCharacter.Inventory.OnInventoryModified -= CheckItemsPresence;
        }

        _selectedCharacter = character;
        _selectedCharacter.Inventory.OnInventoryModified += CheckItemsPresence;
    }

    private void CheckItemsPresence()
    {
        if (_selectedCharacter == null) return;

        /*List<Entity> squad = _squadController.GetSquadMembers();
        squad.Add(_selectedCharacter);*/

        _availableItems.Clear();

        foreach (Item item in requiredItemsList)
        {
            /* foreach (Entity character in squad)
            {
                if (character.Inventory.HasItem(item))
                {
                    _availableItems.Add(item);
                    break;
                }
            } */
            
            if (_squadController.ItemPresentInSquadInventory(item) != null)
            {
                _availableItems.Add(item);
            }
        }

        if (_availableItems.All(item => requiredItemsList.Contains(item)) &&
            requiredItemsList.All(item => _availableItems.Contains(item)))
        {
            FinishQuestStep();
        }
    }
}