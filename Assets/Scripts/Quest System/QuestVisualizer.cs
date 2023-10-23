using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text titleField;
    [SerializeField] private TMP_Text descriptionField;
    [SerializeField] private Transform stepsGrid;
    [Space, SerializeField] private QuestStepVisualizer stepPrefab;

    private List<QuestStepVisualizer> _steps = new List<QuestStepVisualizer>();

    private void CreateStepsList(List<Quest.QuestStepData> stepDataList)
    {
        foreach (Quest.QuestStepData data in stepDataList)
        {
            QuestStepVisualizer step = Instantiate(stepPrefab, stepsGrid);
            step.VisualizeStep(data.Description);
            _steps.Add(step);
        }
    }

    public void Setup(Quest quest)
    {
        titleField.text = quest.GetQuestName();
        descriptionField.text = quest.GetQuestDescription();

        CreateStepsList(quest.GetQuestSteps());
        RedrawStepsList(quest.CurrentStep);
    }

    public void RedrawStepsList(int currentStepIndex)
    {
        if (_steps.Count == 0)
        {
            Debug.LogError("Can't redraw steps list since the list is empty!");
            Clear();
            return;
        }

        if (currentStepIndex > _steps.Count)
        {
            Debug.LogError("Passed index is more then the steps list count, visualization is impossible!");
            Clear();
            return;
        }

        for (int i = 0; i < _steps.Count; i++)
        {
            if (i < currentStepIndex)
            {
                _steps[i].ChangeStepState(StepState.Completed);
            }   
            else if (i == currentStepIndex)
            {
                _steps[i].ChangeStepState(StepState.InProgress);
            }
            else
            {
                _steps[i].ChangeStepState(StepState.Disabled);
            }
        }
    }

    public void Clear()
    {
        titleField.text = string.Empty;
        descriptionField.text = string.Empty;

        foreach (QuestStepVisualizer step in _steps)
        {
            Destroy(step.gameObject);
        }

        _steps.Clear();
    }
}