using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationPath
{
    public readonly Vector3[] LookPoints;
    public readonly PathLine[] TurnBoundaries;
    public readonly int FinishLineIndex;
    public readonly int slowDownIndex;

    public NavigationPath(Vector3[] waypoints, Vector3 startPosition, float turnDistance, float stoppingDistance)
    {
        LookPoints = waypoints;
        TurnBoundaries = new PathLine[LookPoints.Length];
        FinishLineIndex = TurnBoundaries.Length - 1;

        Vector2 previousPoint = ConvertV3ToV2(startPosition);

        for (int i = 0; i < LookPoints.Length; i++)
        {
            Vector2 currentPoint = ConvertV3ToV2(LookPoints[i]);
            Vector2 direction = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundaryPoint = (i == FinishLineIndex) ? currentPoint : currentPoint - direction * turnDistance;

            TurnBoundaries[i] = new PathLine(turnBoundaryPoint, previousPoint - direction * turnDistance);
            previousPoint = turnBoundaryPoint;
        }

        float distanceFromEndPoint = 0;
        for (int i = LookPoints.Length - 1; i > 0; i--)
        {
            distanceFromEndPoint += Vector3.Distance(LookPoints[i], LookPoints[i - 1]);
            if (distanceFromEndPoint > stoppingDistance)
            {
                slowDownIndex = i;
                break;
            }
        }
    }

    private Vector2 ConvertV3ToV2(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;
        foreach (Vector3 point in LookPoints)
        {
            Gizmos.DrawCube(point + Vector3.up, Vector3.one);
        }

        Gizmos.color = Color.white;
        foreach (PathLine line in TurnBoundaries)
        {
            line.DrawWithGizmos(10f);
        }
    }
}