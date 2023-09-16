using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MathExampleSpring : MonoBehaviour
{
    GameObject a;
    const float TAU = 6.28318530718f;
    public bool firstWay;
    public bool enableTimer;
    public bool torusEnabler;
    public AnimationCurve _curve;
    public Gradient _gradient;
    public Color clr;

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
    public float lerpSpeed = 1f;
    private float current, target;
    [Header("Torus Params")]
    public float minorRadius = 1f;
    public float majorRadius = 2f;
    void Awake()
    {
        current = 0;
    }
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
            //if(current == target) target = current == 0f ? 10f : 0f;
            if(enableTimer)
            {
                target = 10f;
                if(current == 10f)
                {
                    current = 0f;
                }
                current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);                
            }

            Vector3 prev = GetPointSecond(0);
            for (int i = 1; i < detail2; i++)
            {
                float t = i / (detail2 - 1f);
                //_curve.Evaluate(t);
                if(enableTimer)
                    Gizmos.color = _gradient.Evaluate(t);
                else
                    Gizmos.color = clr;
                
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
        Vector2 dir = AngleToDir(angle) * minorRadius;//spring radius

        if(torusEnabler)
        {
            var ang = t * TAU;

            Vector3 pDir = AngleToDir(ang);
            Vector3 p = pDir * majorRadius;//torus radius
            Vector3 localUp = new Vector3(0,0,1);
            
            return p + dir.x * pDir + dir.y * localUp;
        }
        else
        {
            return new Vector3(dir.x, dir.y, t * height);
        }
    }
    Vector2 AngleToDir(float angleRad)
    {
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    
}
