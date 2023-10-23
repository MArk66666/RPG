using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private QuestManager _questManager;

    protected void FinishQuestStep()
    {
        if (_questManager == null)
        {
            Debug.LogError("Quest manager is not assigned. Perhaps, the questStep wasn't initialized correctly!");
            return;
        }

        _questManager.MoveToNextQuestStep();
        Destroy(gameObject);
    }

    public void Initialize(QuestManager questManager)
    {
        _questManager = questManager;
    }
}