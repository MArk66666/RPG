using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacle : MonoBehaviour
{
    private Collider _collider;
    public bool Walkable { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        Walkable = false;
        UpdateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleWalkable();
        }
    }

    private void ToggleWalkable()
    {
        SetWalkable(!Walkable);
    }

    private void UpdateGrid()
    {
        PathGrid.Instance.UpdateGrid(this);
    }

    public void SetWalkable(bool value)
    {
        Walkable = value;
        Debug.Log("Walksable set to " + value);
        UpdateGrid();
    }

    public Bounds GetObstacleBounds()
    {
        return _collider.bounds;    
    }  
}