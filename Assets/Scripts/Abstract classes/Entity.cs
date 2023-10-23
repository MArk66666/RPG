using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[RequireComponent(typeof(UniqueIDHolder))]
[RequireComponent(typeof(EntityInventory))]
public abstract class Entity : MonoBehaviour
{
    public EntityInventory Inventory { get; private set; }
    public UniqueIDHolder EntityIDHolder { get; private set; }

    private void Awake()
    {
        Inventory = GetComponent<EntityInventory>();
        EntityIDHolder = GetComponent<UniqueIDHolder>();
    }
}