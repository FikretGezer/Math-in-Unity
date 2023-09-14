using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MathExampleSpring : MonoBehaviour
{
    const float TAU = 6.28318530718f;
    public float addAmount = 0.01f;
    public int detail = 64;
    void OnDrawGizmos()
    {
        List<Vector3> points = new List<Vector3>();
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        float y = 0f;
        Vector3 prev = GetPoint(0, y);
        for (int i = 1; i < detail; i++)
        {
            y += addAmount;
            float t = i / (detail - 1f);
            t *= TAU;
            //Gizmos.DrawWireSphere(GetPoint(t, j), 0.02f);
            //points.Add(GetPoint(t, y));
            Gizmos.DrawLine(prev, GetPoint(t, y));
            prev = GetPoint(t, y);
        }
        //Handles.DrawAAPolyLine(points.ToArray());
    }
    Vector3 GetPoint(float angleRad, float y)
    {
        return new Vector3(Mathf.Cos(angleRad), y, Mathf.Sin(angleRad));
    }
}
