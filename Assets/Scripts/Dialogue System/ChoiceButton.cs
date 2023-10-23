using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextVisualizer textField;
    private Button _buttonComponent;

    private DialogueChoice _choiceData;

    private void Awake()
    {
        _buttonComponent = GetComponent<Button>();  
    }

    private void SetupInteractions()
    {
        if (_choiceData.StartQuest())
        {
            InitializeQuest(_choiceData.Quest);
        }

        if (_choiceData.AddItem())
        {
            InteractWithInventory(_choiceData.AddableItem);  
        }

        if (_choiceData.RemoveItem())
        {
            InteractWithInventory(_choiceData.RemovableItem, true);
        }
    }

    private void AddBaseOnClickFunctions()
    {
        DialogueManager dialogueManager = DialogueManager.Instance;
        _buttonComponent.onClick.AddListener(() => dialogueManager.SetNodeIndex(_choiceData.TargetNodeID));
    
        if (_choiceData.EndConversation)
        {
            _buttonComponent.onClick.AddListener(() => dialogueManager.EndDialogue());
        }
    }

    private void InitializeQuest(Quest quest)
    {
        _buttonComponent.onClick.AddListener( () => QuestManager.Instance.Initialize(quest));
    }

    private void InteractWithInventory(Item item, bool remove = false)
    {
        SquadController squadController = SquadController.Instance;

        if (remove)
        {
            EntityInventory inventory = squadController.ItemPresentInSquadInventory(item);
            if (inventory != null)
            {
                _buttonComponent.onClick.AddListener(() => inventory.RemoveItem(item));
            }
        }
        else
        {
            Entity selectedCharacter = squadController.GetCurrentSelectedCharacter();
            if (selectedCharacter != null)
            {
                _buttonComponent.onClick.AddListener(() => selectedCharacter.Inventory.AddItem(item));
            }
        }

        Debug.LogWarning("Method called");
    }

    public void Setup(DialogueChoice choiceData)
    {
        textField.Write(choiceData.Text);
        _choiceData = choiceData;

        SetupInteractions();
        AddBaseOnClickFunctions();
    }
}