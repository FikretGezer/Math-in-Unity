using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLessonTrajectoriesAndDerivatives : MonoBehaviour
{
    public float launchSpeed = 1f;
    public float duration = 1f;
    public float radius = 0.1f;
    Vector3 position => transform.position;
    Vector3 velocity => transform.right * launchSpeed;
    Vector3 acceleration = Physics.gravity;
    [Range(0, 80)] public float curPos;
    private void OnDrawGizmos() {
        
        int detail = 80;
        float Func()
        {
            for (int i = 0; i < detail - 1; i++)
            {
                var t = i / (detail - 1f);
                var t2 = (i + 1) / (detail - 1f);
                float time = t * duration;
                var col = Physics.CheckSphere(GetPoint(t), radius);
                if(col) return t;
                Gizmos.DrawLine(GetPoint(t), GetPoint(t2));  
            }
            return default;
        }
        var p = Func();
        var ti = curPos / (detail - 1f);
        if(ti < p)
            Gizmos.DrawSphere(GetPoint(ti), 0.02f);

    }

    private Vector3 GetPoint(float t)
    {
        return position + velocity * t + (acceleration/2) * t * t;
    }
}
