using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PathGrid))]
[RequireComponent(typeof(PathRequestManager))]
public class Pathfinding : MonoBehaviour
{
    private PathGrid _pathGrid;
    private PathRequestManager _pathRequestManager;

    private void Awake()
    {
        _pathGrid = GetComponent<PathGrid>();
        _pathRequestManager = GetComponent<PathRequestManager>();
    }

    private IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathFound = false;

        PathNode startNode = _pathGrid.GetNodeFromWorldPosition(startPosition);
        PathNode targetNode = _pathGrid.GetNodeFromWorldPosition(targetPosition);

        if (startNode.Walkable && targetNode.Walkable)
        {
            Heap<PathNode> openSet = new Heap<PathNode>(_pathGrid.GetMaxGridSize());
            HashSet<PathNode> closedSet = new HashSet<PathNode>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                PathNode currentNode = openSet.RemoveFirstHeapItem();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("Path found: " + sw.ElapsedMilliseconds + " ms");
                    pathFound = true;
                    break;
                }

                foreach (PathNode neighbour in _pathGrid.GetNodeNeighbours(currentNode))
                {
                    if (neighbour.Walkable == false || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCost = currentNode.GCost + GetDistance(currentNode, neighbour) + neighbour.MovementPenalty;
                    if (newMovementCost < neighbour.GCost || openSet.Contains(neighbour) == false)
                    {
                        neighbour.GCost = newMovementCost;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.ParentNode = currentNode;

                        if (openSet.Contains(neighbour) == false)
                        {
                            openSet.Add(neighbour);
                        } 
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }                          
        yield return null;

        if (pathFound == true)
        {    
            waypoints = RetracePath(startNode, targetNode);
        }

        _pathRequestManager.FinishProcessingPath(waypoints, pathFound);
    }

    private Vector3[] RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    private Vector3[] SimplifyPath(List<PathNode> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 oldDirection = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 newDirection = new Vector2(path[i-1].GridX - path[i].GridX, 
                path[i-1].GridY - path[i].GridY);

            if (newDirection != oldDirection)
            {
                waypoints.Add(path[i].WorldPosition);
            }

            oldDirection = newDirection;
        }

        return waypoints.ToArray();
    }

    private int GetDistance(PathNode nodeA, PathNode nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distanceY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }   
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }

    public void StartFindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        StartCoroutine(FindPath(startPosition, targetPosition));     
    }
}