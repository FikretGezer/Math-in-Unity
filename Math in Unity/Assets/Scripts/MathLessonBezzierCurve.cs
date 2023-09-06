using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathLessonBezzierCurve : MonoBehaviour
{
    public Transform pointA, pointB, pointC, pointD, pointLast;
    [Range(0, 1f)] public float t;
    public bool toggleLines;
    private void OnDrawGizmos() {
        Vector3 a = pointA.position;
        Vector3 b = pointB.position;
        Vector3 c = pointC.position;
        Vector3 d = pointD.position;
        Vector3 last = pointLast.position;

        if(toggleLines)
        {
            DrawLine(a, b);
            DrawLine(b, c);
            DrawLine(c, d);
            DrawLine(d, last);
        }

        var point = Bezzier(a,b,c,d,last,t);
        Gizmos.DrawSphere(point, 0.1f);

        int detail = 32;
        var prev = a;
        for (int i = 1; i < detail; i++)
        {
            float tt = i / (detail - 1f);
            Vector3 cur = Bezzier(a,b,c,d,last,tt);
            DrawLine(prev, cur);
            prev = cur;
        }
    }
    void DrawLine(Vector3 a, Vector3 b)
    {
        Gizmos.DrawLine(a, b);
    }
    Vector3 Bezzier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 last, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);
        Vector3 cd = Vector3.Lerp(c, d, t);
        Vector3 dLa = Vector3.Lerp(d, last, t);

        Vector3 e = Vector3.Lerp(ab, bc, t);
        Vector3 f = Vector3.Lerp(bc, cd, t);
        Vector3 g = Vector3.Lerp(cd, dLa, t);

        Vector3 h = Vector3.Lerp(e, f, t);
        Vector3 i = Vector3.Lerp(f, g, t);

        return Vector3.Lerp(h, i, t);
    }
}
