using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    private Queue<PathRequest> _pathRequestQueue = new Queue<PathRequest>();
    private PathRequest _currentPathRequest;

    private bool _isProcessingPath = false;

    private Pathfinding _pathfinding;
    public static PathRequestManager Instance;

    private struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> caller)
        {
            pathStart = start;
            pathEnd = end;
            callback = caller;
        }
    }

    private void Awake()
    {
        Instance = this;
        _pathfinding = GetComponent<Pathfinding>();
    }

    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        Instance._pathRequestQueue.Enqueue(newRequest);
        Instance.TryProcessNext();
    }

    private void TryProcessNext()
    {
        if (_isProcessingPath == false && _pathRequestQueue.Count > 0)
        {
            _currentPathRequest = _pathRequestQueue.Dequeue();
            _isProcessingPath = true;
            _pathfinding.StartFindPath(_currentPathRequest.pathStart, _currentPathRequest.pathEnd);
        }    
    }

    public void FinishProcessingPath(Vector3[] path, bool success)
    {
        _currentPathRequest.callback(path, success);
        _isProcessingPath = false;
        TryProcessNext();
    }
}