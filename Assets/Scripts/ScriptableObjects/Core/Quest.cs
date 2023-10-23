using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest")]
public class Quest : ScriptableObject
{
    [ReadOnly, InspectorIcon(InspectorIcon.Hierarchy)] public string id;

    [HorizontalLine(" General ", color: FixedColor.Cyan)]
    [SerializeField] private string questName;
    [SerializeField, TextArea(5, 10)] private string questDescription;

    [SerializeField] private List<Quest> questPrerequisites;
    [SerializeField] private List<QuestStepData> steps;

    public int CurrentStep { get; set; }

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

    public string GetQuestName()
    {
        return questName;
    }

    public string GetQuestDescription()
    {
        return questDescription;
    }

    public List<QuestStepData> GetQuestSteps()
    {
        return new List<QuestStepData>(steps);
    }

    public QuestStep GetCurrentQuestStep()
    {
        return steps[CurrentStep].Step;
    }

    [System.Serializable]
    public class QuestStepData
    {
        public string Description;
        public QuestStep Step;
    }
}