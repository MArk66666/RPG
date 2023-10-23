using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestStepVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text titleField;

    private StepState _currentState;

    public void VisualizeStep(string title)
    {
        titleField.text = title;
        _currentState = StepState.Disabled;
    }

    public void ChangeStepState(StepState state)
    {
        _currentState = state;

        switch (state)
        {
            case StepState.Disabled:
                titleField.color = Color.grey;
                break;
            case StepState.InProgress:
                titleField.color = Color.white;
                break;
            case StepState.Completed:
                titleField.color = Color.green;
                break;
        }
    }
}

public enum StepState
{
    Disabled,
    InProgress,
    Completed
}