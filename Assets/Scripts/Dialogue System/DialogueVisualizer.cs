using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text speakerName;
    [SerializeField] private TextVisualizer dialogueTextField;

    [SerializeField] private Image speakerAvatar;

    [SerializeField] private Transform choicesGrid;
    [SerializeField] private ChoiceButton choicePrefab;

    [SerializeField] private SquadController squadController;
    [SerializeField] private QuestManager questManager;

    private List<ChoiceButton> _choices = new List<ChoiceButton>();
    private Dialogue _dialogue;

    private bool ChoiceConditionsMet(DialogueChoice choice)
    {
        if (choice.ItemCondition())
        {
            if (squadController.ItemPresentInSquadInventory(choice.RequiredItem) == null)
            {
                return false;
            }        
        }

        if (choice.QuestCondition())
        {
            if (choice.Completed)
            {
                if (questManager.QuestFinished(choice.RequiredQuest) == false)
                {
                    return false;
                }
            }  
            else
            {
                if (questManager.QuestAvailable(choice.RequiredQuest) == false)
                {
                    return false;
                }
            } 
        }

        return true;
    }

    private void CreateChoices(DialogueChoice[] choicesList)
    {
        foreach (ChoiceButton choice in _choices)
        {
            Destroy(choice.gameObject);
        }

        _choices.Clear();

        foreach (DialogueChoice choice in choicesList)
        {
            if (ChoiceConditionsMet(choice) == false) continue;

            ChoiceButton choiceButton = Instantiate(choicePrefab, choicesGrid);
            choiceButton.Setup(choice);
            _choices.Add(choiceButton);
        }
    }

    public void Setup(Dialogue dialogue)
    {
        _dialogue = dialogue;
        RedrawDialogueFields(0);
    }

    public void RedrawDialogueFields(int currentNodeIndex)
    {
        if (_dialogue == null)
        {
            Debug.LogError("No dialogue to visualize!");
            Clear();
            return;
        }

        if (currentNodeIndex > _dialogue.Nodes.Length)
        {
            Debug.LogError("Passed node index is more then the dialogue's nodes amount, visualization is impossible!");
            Clear();
            return;
        }

        DialogueNode node = _dialogue.Nodes[currentNodeIndex];

        speakerName.text = node.Speaker.GetName();
        speakerAvatar.sprite = node.Speaker.GetAvatar();
        dialogueTextField.Write(node.NodeText);

        CreateChoices(node.Choices);
    }

    public void Clear()
    {
        speakerName.text = string.Empty;

        _dialogue = null;

        foreach (ChoiceButton choice in _choices)
        {
            Destroy(choice.gameObject);
        }

        _choices.Clear();
    }
}