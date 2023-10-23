using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathLine
{
    public const float verticalLineGradient = 1e5f;

    public float gradient;
    public float yIntercept;
    public float perpendicularLineGradient;

    public Vector2 pointOnLine_1;
    public Vector2 pointOnLine_2;

    public bool approachSide;

    public PathLine(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
    {
        float deltaX = pointOnLine.x - pointPerpendicularToLine.x;
        float deltaY = pointOnLine.y - pointPerpendicularToLine.y;

        if (deltaX == 0)
        {
            perpendicularLineGradient = verticalLineGradient;
        }
        else
        {
            perpendicularLineGradient = deltaY / deltaX;
        }

        if (perpendicularLineGradient == 0)
        {
            gradient = verticalLineGradient;
        }   
        else
        {
            gradient = -1 / perpendicularLineGradient;
        }

        yIntercept = pointOnLine.y - gradient * pointOnLine.x;
        pointOnLine_1 = pointOnLine;
        pointOnLine_2 = pointOnLine + new Vector2(1, gradient);

        approachSide = false;
        approachSide = GetSide(pointPerpendicularToLine);
    }

    private bool GetSide(Vector2 point)
    {
        return (point.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > 
               (point.y - pointOnLine_1.y) * (pointOnLine_2.x - pointOnLine_1.x);  
    }

    public void DrawWithGizmos(float length)
    {
        Vector3 lineDirection = new Vector3(1, 0, gradient).normalized;
        Vector3 lineCenter = new Vector3(pointOnLine_1.x, 0, pointOnLine_1.y) + Vector3.up;
        Gizmos.DrawLine(lineCenter - lineDirection * length / 2f,
            lineCenter + lineDirection * length / 2f);
    }

    public float GetDistanceFromPoint(Vector2 point)
    {
        float yPerpendicularIntercept = point.y - perpendicularLineGradient * point.x;
        float xIntersect = (yPerpendicularIntercept - yIntercept) / (gradient - perpendicularLineGradient);
        float yIntersect = gradient * xIntersect + yIntercept;
        return Vector2.Distance(point, new Vector2(xIntersect, yIntersect));
    }

    public bool HasCrossedLine(Vector2 point)
    {
        return GetSide(point) != approachSide;
    }
}