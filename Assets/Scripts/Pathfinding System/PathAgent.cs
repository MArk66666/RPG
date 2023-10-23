using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAgent : MonoBehaviour
{
    private const float pathUpdateMoveThreshold = .5f;
    private const float pathUpdateTime = .2f;

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float stoppingDistance = 10f;
    [SerializeField] private float turnDistance = 5f;

    private Coroutine _moveToDestinationCoroutine;
    private Coroutine _updatePathCoroutine;

    private Transform _followTransform;
    private NavigationPath _path;

    private IEnumerator MoveToDestination()
    {
        bool follwoingPath = true;
        int pathIndex = 0;
        transform.LookAt(_path.LookPoints[0]);

        float speedPrecent = 1f;

        while (follwoingPath)
        {
            if (pathIndex >= _path.LookPoints.Length)
            {
                Debug.LogError("Path index is out of range! Stopping movement.");
                yield break;
            }

            Vector2 flatPosition = new Vector2(transform.position.x, transform.position.z);
            while (_path.TurnBoundaries[pathIndex].HasCrossedLine(flatPosition))
            {
                if (pathIndex == _path.FinishLineIndex)
                {
                    follwoingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if (follwoingPath)
            {
                if (pathIndex >= _path.slowDownIndex && stoppingDistance > 0)
                {
                    speedPrecent = _path.TurnBoundaries[_path.FinishLineIndex].GetDistanceFromPoint(flatPosition) / stoppingDistance;
                    speedPrecent = Mathf.Clamp01(speedPrecent);

                    if (speedPrecent <= 0.01f)
                    {
                        follwoingPath = false;
                    }
                }

                Vector3 lookDIrection = _path.LookPoints[pathIndex] - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(lookDIrection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * speedPrecent * Time.deltaTime, Space.Self);
            }

            yield return null;
        }
    }

    private IEnumerator UpdatePath()
    {
        if (_followTransform == null)
        {
            Debug.Log("No object to follow!");
            yield return new WaitForSeconds(pathUpdateTime);
        }

        if (Time.timeSinceLevelLoad < 0.3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        SetDestination(_followTransform.position);

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 oldTargetPosition = _followTransform.position;

        while (true)
        {
            yield return new WaitForSeconds(pathUpdateTime);   
            if ((_followTransform.position - oldTargetPosition).sqrMagnitude > sqrMoveThreshold)
            {
                SetDestination(_followTransform.position);
                oldTargetPosition = _followTransform.position;
            }
        }
    }

    private void OnPathFound(Vector3[] waypoints, bool pathFound)
    {
        if (pathFound)
        {
            _path = new NavigationPath(waypoints, transform.position, turnDistance, stoppingDistance);

            if (_moveToDestinationCoroutine != null)
            {
                StopCoroutine(_moveToDestinationCoroutine);
            }

            _moveToDestinationCoroutine = StartCoroutine(MoveToDestination());
        }
    }

    private void OnDrawGizmos()
    {
        if (_path != null)
        {
            _path.DrawWithGizmos();
        }
    }

    public void SetDestination(Vector3 destination)
    {
        PathRequestManager.RequestPath(transform.position, destination, OnPathFound);
    }

    public void SetFollowPoint(Transform target)
    {
        if (_updatePathCoroutine != null)
        {
            StopCoroutine(_updatePathCoroutine);
        }

        _followTransform = target;
        _updatePathCoroutine = StartCoroutine(UpdatePath());
    }

    public void StopFollowingPoint()
    {
        if (_updatePathCoroutine != null)
        {
            StopCoroutine(_updatePathCoroutine);
        }

        _followTransform = null;
    }
}