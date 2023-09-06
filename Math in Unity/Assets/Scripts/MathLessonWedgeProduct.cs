using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLessonWedgeProduct : MonoBehaviour
{
    public Transform pt;
    public Transform a, b, c;
    public float value;
    private void OnDrawGizmos()
    {

        var ptA = a.position;
        var ptB = b.position;
        var ptC = c.position;
        var ptPos = pt.position;

        Gizmos.DrawSphere(ptPos, 0.01f);

        Gizmos.color = IsItInside(ptA, ptB, ptC, ptPos) ? Color.red : Color.white;

        Gizmos.DrawLine(ptA, ptB);
        Gizmos.DrawLine(ptB, ptC);
        Gizmos.DrawLine(ptC, ptA);    
    }
    private bool IsItInside(Vector2 a, Vector2 b, Vector2 c, Vector2 pt)
    {
        var ab = GetSide(a, b, pt);
        var bc = GetSide(b, c, pt);
        var ca = GetSide(c, a, pt);
        return ab == bc && bc == ca;
    }
    private bool GetSide(Vector2 a, Vector2 b, Vector2 pt)
    {
        var vec = b - a;
        var ptRel = pt - a;
        return WedgeProduct(vec, ptRel) > 0;
    }
    private float WedgeProduct(Vector2 a, Vector2 b)
    {
        return a.x*b.y - a.y*b.x;
    }
}
