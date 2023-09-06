using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MathLessonSeven : MonoBehaviour
{
    [Range(0, 360)]
    public float angleDeg;
    public float value;
    private void OnDrawGizmos() {
        Handles.DrawWireDisc(default, Vector3.forward, 1f);

        var dir = AngleToDir(angleDeg);
        Gizmos.DrawLine(default, dir);

        Handles.Label(default, DirToAngle(dir).ToString());
    }
    private float DegreesToRadians(float degree) => Mathf.Deg2Rad * degree;
    private Vector2 AngleToDir(float angleDeg)
    {
        var angleRad = DegreesToRadians(angleDeg);
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    private float DirToAngle(Vector2 v)
    {
        var angle = Mathf.Rad2Deg * Mathf.Atan2(v.y, v.x);
        if(angle < 0f)
        {
            angle += 360f;
        }
        return angle;
    }
}
