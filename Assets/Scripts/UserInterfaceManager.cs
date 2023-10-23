using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private InventoryVisualizer inventoryPanel;
    [SerializeField] private ActionsPanel actionsPanel;
    [SerializeField] private QuestVisualizer questVisualizer;
    [SerializeField] private DialogueVisualizer dialogueVisualizer;

    private SquadController _squadController;

    public static UserInterfaceManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _squadController = SquadController.Instance;
        questVisualizer.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_squadController.GetCurrentSelectedCharacter() != null)
            {
                if (inventoryPanel.gameObject.activeSelf == true)
                {
                    ToggleUIComponent(inventoryPanel.gameObject);
                    return;
                }

                VisualizeEntityInventory(_squadController.GetCurrentSelectedCharacter());
            }
        }
    }

    public void VisualizeEntityInventory(Entity entity)
    {
        if (entity == null)
        {
            return;
        }

        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.AssignInventory(entity.Inventory);
    }

    public void ToggleUIComponent(GameObject target)
    {
        bool activness = target.activeSelf;
        target.SetActive(!activness);
    }

    public void InitializeActionPanel(GameObject targetObject)
    {
        actionsPanel.gameObject.SetActive(true);
        actionsPanel.SetPosition(Input.mousePosition);
        actionsPanel.VisualizeActions(targetObject);
    }

    public void DeactivateActionsPanel()
    {
        actionsPanel.gameObject.SetActive(false);
    }

    public void VisualizeQuest(Quest quest)
    {
        questVisualizer.gameObject.SetActive(true);
        questVisualizer.Clear();
        if (quest == null)
        {
            Debug.LogError("Quest parameter is null!");
            return;
        }

        questVisualizer.Setup(quest);
    }

    public void UpdateQuestUI(int currentStepIndex)
    {
        questVisualizer.RedrawStepsList(currentStepIndex);
    }

    public void DisableQuestUI()
    {
        questVisualizer.gameObject.SetActive(false);
    }

    public void VisualizeDialogue(Dialogue dialogue)
    {
        dialogueVisualizer.gameObject.SetActive(true);
        dialogueVisualizer.Clear();
        if (dialogue == null)
        {
            Debug.LogError("Dialogue parameter is null!");
            return;
        }

        dialogueVisualizer.Setup(dialogue);
    }

    public void UpdateDialogueUI(int nodeIndex)
    {
        dialogueVisualizer.RedrawDialogueFields(nodeIndex);
    }

    public void DisableDialogueUI()
    {
        dialogueVisualizer.gameObject.SetActive(false);
        dialogueVisualizer.Clear();
    }

    public ItemSlot GetItemSlotPrefab()
    {
        return itemSlotPrefab;
    }
}