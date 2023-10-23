using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[System.Serializable]
public class DialogueChoice
{
    [Indent(-1)] public string Text;
    [Indent(-1)] public int TargetNodeID;
    public Interaction[] Interactions;
    public Condition[] Conditions;

    [HorizontalLine("Modifiers", color: FixedColor.Gray)]
    [Indent(1)] public bool EndConversation;

    [ShowIf("StartQuest")] public Quest Quest;
    [ShowIf("AddItem")] public Item AddableItem;
    [ShowIf("RemoveItem")] public Item RemovableItem;
    [Space(10)]
    [ShowIf("ItemCondition")] public Item RequiredItem;
    [ShowIf("QuestCondition")] public Quest RequiredQuest;
    [ShowIf("QuestCondition")] public bool Completed;

    #region Modifiers
    public bool StartQuest()
    {
        foreach (Interaction interaction in Interactions)
        {
            if (interaction == Interaction.StartQuest) return true;
        }
        return false;
    }
    public bool AddItem()
    {
        foreach (Interaction interaction in Interactions)
        {
            if (interaction == Interaction.AddItem) return true;
        }
        return false;
    }
    public bool RemoveItem()
    {
        foreach (Interaction interaction in Interactions)
        {
            if (interaction == Interaction.RemoveItem) return true;
        }
        return false;
    }
    public bool ItemCondition()
    {
        foreach (Condition condition in Conditions)
        {
            if (condition == Condition.Item) return true;
        }
        return false;
    }
    public bool QuestCondition()
    {
        foreach (Condition condition in Conditions)
        {
            if (condition == Condition.Quest) return true;
        }
        return false;
    }
    #endregion
}

public enum Interaction
{
    StartQuest,
    AddItem,
    RemoveItem
}

public enum Condition
{
    Quest,
    Item
}   