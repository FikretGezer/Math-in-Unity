using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MathExamples : MonoBehaviour
{
    [Range(-90f,90f)]
    [SerializeField] private float angleDeg;
    [SerializeField] private Transform target;
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        var angleRad = DegToRad(angleDeg);
        var dir = CalculatedDir(angleRad);
        var dir2 = new Vector2(-dir.x, dir.y);
        //Handles.DrawWireDisc(default, Vector3.forward, 1f);
        var restAngleRad = Mathf.Acos(Vector2.Dot(Vector2.up, dir));
        var restAngle = restAngleRad * Mathf.Rad2Deg;
        var restAngleDeg2Side = 2 * restAngleRad * Mathf.Rad2Deg;

        //Is it inside
        
        var dirOfTarget = (target.position - transform.position).normalized;
        

        //
        Gizmos.color = Color.green;
        Gizmos.DrawLine(default, dirOfTarget);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(default, dir);
        Gizmos.DrawLine(default, dir2);
        Handles.DrawWireArc(default, Vector3.forward, dir, restAngleDeg2Side, 1f);
    }
    private Vector2 CalculatedDir(float angleRad)
    {
        return new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
    private float DegToRad(float angleDeg) => angleDeg * Mathf.Deg2Rad;
}
