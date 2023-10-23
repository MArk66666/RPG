using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "New Personality", menuName = "ScriptableObjects/Personality")]
public class Personality : ScriptableObject
{
    [HorizontalLine(" Appearance ", color: FixedColor.Gray)]
    [SerializeField, Preview(Size.big)] private Sprite icon;
    [SerializeField, Preview(Size.medium)] private Sprite avatar;
    [HorizontalLine(" General ", color: FixedColor.Gray)]
    [SerializeField] private new string name;
    [SerializeField, TextArea(5,10)] private string description;
    [SerializeField] private Race race;
    [SerializeField] private List<Trait> traits;
    [SerializeField] private int initialRelationship;

    private int _currentRelationship;

    public Personality (string newName, string newDescription, int relationship, Race newRace)
    {
        name = newName;
        description = newDescription;
        initialRelationship = relationship;
        race = newRace;
    }

    public void ResetRelationship()
    {
        _currentRelationship = initialRelationship;
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public int GetRelationship()
    {
        return _currentRelationship;
    }

    public Race GetRace()
    {
        return race;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public Sprite GetAvatar()
    {
        return avatar;
    }
}

public enum Race
{
    Human,
    Demon,
    Elf,
    Goblin,
    Undead,
    Inanimate
}

public enum Trait
{
    Kind, 
    Compassionate, 
    Brave,  
    Curious, 
    Pacifist, 
    Rude, 
    Indifferent, 
    Coward, 
    Greedy, 
    Aggresive,
    Cunning
}