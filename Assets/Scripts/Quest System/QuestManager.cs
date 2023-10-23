using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private UserInterfaceManager uiManager;

    private List<Quest> _availableQuests = new List<Quest>();
    private List<Quest> _finishedQuests = new List<Quest>();

    private Quest _selectedQuest;
    private QuestStep _currentStep;

    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;   
    }

    private void UpdateQuestStepPrefab(Quest quest)
    {
        QuestStep questStep = quest.GetCurrentQuestStep();

        if (_currentStep == questStep)
        {
            return;
        }

        if (_currentStep != null)
        {
            Destroy(_currentStep.gameObject);
            _currentStep = null;
        }

        _currentStep = Instantiate(questStep, transform);
        _currentStep.Initialize(this);
    }

    public void Initialize(Quest quest)
    {
        //Check whether the quest was already taken or completed by the player
        if (QuestAvailable(quest) || QuestFinished(quest))
        {
            // if so then return and debug a message in the console
            Debug.Log(quest.id + " is already initialized!");
            return;
        }

        //Otherwise add the quest in the available quests list
        _availableQuests.Add(quest);
        //Reset the quest step
        quest.CurrentStep = 0;
        //Mark the quest as selected 
        SelectQuest(quest);
        Debug.Log(quest.id + " is successfully initialized!");
    }

    public void FinishQuest(Quest quest)
    {
        if (QuestAvailable(quest) == false)
        {
            if (QuestFinished(quest) == true)
            {
                Debug.LogError(quest.id + " quest is already finished!");
            }
            else
            {
                Debug.LogError(quest.id + " quest was not found in the available quests list!");
            }

            return;
        }

        _availableQuests.Remove(quest);
        _finishedQuests.Add(quest);
    }

    public void SelectQuest(Quest quest)
    {
        // Selecting the quest
        _selectedQuest = quest;
        // Visualizing the selected Quest
        uiManager.VisualizeQuest(quest);
        //Spawning the current quest step prefab 
        UpdateQuestStepPrefab(quest);
    }

    public void MoveToNextQuestStep()
    {
        if (_selectedQuest == null)
        {
            Debug.LogError("No quest is currently selected!");
            return;
        }

        int currentStepIndex = _selectedQuest.CurrentStep;
        currentStepIndex++;

        if (currentStepIndex >= _selectedQuest.GetQuestSteps().Count)
        {
            currentStepIndex = _selectedQuest.GetQuestSteps().Count;
            _selectedQuest.CurrentStep = currentStepIndex;

            uiManager.UpdateQuestUI(currentStepIndex);
            FinishQuest(_selectedQuest);

            Debug.Log(_selectedQuest.id + " quest was successfully completed!");

            if (_availableQuests.Count > 0)
            {
                Quest newQuest = _availableQuests[0];
                SelectQuest(newQuest);
                Debug.Log(newQuest.id + " was selected!");
            }
        }
        else
        {
            _selectedQuest.CurrentStep = currentStepIndex;
            UpdateQuestStepPrefab(_selectedQuest);
            uiManager.UpdateQuestUI(currentStepIndex);
        }
    }

    public bool QuestFinished(Quest quest)
    {
        return _finishedQuests.Contains(quest);
    }

    public bool QuestAvailable(Quest quest)
    {
        return _availableQuests.Contains(quest);
    }
}