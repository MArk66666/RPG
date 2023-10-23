using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGrid : MonoBehaviour
{
    [SerializeField] private Vector2 gridWorldSize;
    [SerializeField] private float nodeRadius;
    [SerializeField] private LayerMask obstacleLayer;
    [Space][SerializeField] private bool drawGizmos = true;

    private float _nodeDiameter;
    private int _gridSizeX;
    private int _gridSizeY;
    private PathNode[,] _grid;

    public static PathGrid Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        CalculateGridSize();
        CreateGrid();
    }

    private void CalculateGridSize()
    {
        _nodeDiameter = nodeRadius * 2f;
        _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);
    }

    private void CreateGrid()
    {
        _grid = new PathNode[_gridSizeX, _gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 
            - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + nodeRadius)
                    + Vector3.forward * (y * _nodeDiameter + nodeRadius);

                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, obstacleLayer);
                int movementPenalty = 0;
                                        
                _grid[x, y] = new PathNode(walkable, worldPoint, x, y, movementPenalty);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1f, gridWorldSize.y));
        if (_grid != null && drawGizmos == true)
        {
            foreach (PathNode node in _grid)
            {
                if (node.Walkable)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (_nodeDiameter - .1f));
            }
        }
    }

    public void UpdateGrid(DynamicObstacle obstacle)
    {
        Bounds bounds = obstacle.GetObstacleBounds();
        bool walkable = obstacle.Walkable;

        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = _grid[x, y].WorldPosition;
                if (bounds.Contains(worldPoint))
                {
                    _grid[x, y].Walkable = walkable;
                }
            }
        }
    }

    public List<PathNode> GetNodeNeighbours(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();  
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < gridWorldSize.x && checkY >= 0 && checkY < gridWorldSize.y)
                {
                    neighbours.Add(_grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public PathNode GetNodeFromWorldPosition(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

        return _grid[x, y];
    }

    public int GetMaxGridSize()
    {
        return _gridSizeX * _gridSizeY;
    }
}