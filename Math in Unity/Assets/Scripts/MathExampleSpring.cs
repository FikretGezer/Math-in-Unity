using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MathExampleSpring : MonoBehaviour
{
    const float TAU = 6.28318530718f;
    public bool firstWay;

    [Header("First Way Parameters")]
    [Range(0, 2f)] public float radius = 0.1f;
    [Range(0.001f, 0.02f)] public float addAmount = 0.01f;
    [Range(1, 10)] public int turn = 1;
    public int detail = 64;

    [Header("Second Way Parameters")]
    [Range(0, 10)] public float turnCount = 1f;
    [Range(0, 2f)] public float radiusSecond = 0.1f;
    [Range(16, 256)]public float detail2 = 64;
    public float height = 1f;
    void OnDrawGizmos()
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;
        //First way of doing it
        if(firstWay)
        {
            float y = 0f;
            Vector3 prev = GetPoint(0, y);
            for (int j = 0; j < turn; j++)
            {
                for (int i = 1; i < detail; i++)
                {
                    y += addAmount;
                    float t = i / (detail - 1f);
                    t *= TAU;
                    Gizmos.DrawLine(prev, GetPoint(t, y));
                    prev = GetPoint(t, y);
                }
            }
        }
        else //Second way of doing it
        {
            Vector3 prev = GetPointSecond(0);
            for (int i = 1; i < detail2; i++)
            {
                float t = i / (detail2 - 1f);
                Vector3 cur = GetPointSecond(t);
                Gizmos.DrawLine(prev, cur);
                prev = cur;
            }
        }
    }
    Vector3 GetPoint(float angleRad, float y)
    {
        var v = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * radius; 
        return new Vector3(v.x, y, v.y);
    }
    Vector3 GetPointSecond(float t)
    {
        float angle = t * TAU * turnCount;
        Vector2 dir = AngleToDir(angle) * radiusSecond;
        return new Vector3(dir.x, t * height, dir.y);
    }
    Vector2 AngleToDir(float angleRad)
    {
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    
}
