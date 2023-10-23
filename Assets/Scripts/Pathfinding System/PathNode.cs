using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : IHeapItem<PathNode>
{
    public bool Walkable;
    public Vector3 WorldPosition;

    public int GridX;
    public int GridY;

    public int GCost;
    public int HCost;

    public int MovementPenalty;

    public PathNode ParentNode;
    public int HeapIndex { get => _heapIndex; set => _heapIndex = value; }

    private int _heapIndex;

    public PathNode(bool walkable, Vector3 position, int gridX, int gridY, int penalty)
    {
        Walkable = walkable;
        WorldPosition = position;
        GridX = gridX;
        GridY = gridY;
        MovementPenalty = penalty;
    }


    public int CompareTo(PathNode other)
    {
        int cost = GetFinalCost().CompareTo(other.GetFinalCost());
        if (cost == 0)
        {
            cost = HCost.CompareTo(other.HCost);
        }
        return -cost;
    }

    public int GetFinalCost()
    {
        return GCost + HCost;
    }
}