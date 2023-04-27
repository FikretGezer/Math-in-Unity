using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMathScript : MonoBehaviour
{
    public Transform _a, _b;
    public float _offset = 1f;

    private void Update()
    {
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 a = _a.position;
        Vector2 b = _b.position;
        Vector2 _aToB = (b-a);
        Vector2 _aToBDir = _aToB.normalized;
        Gizmos.DrawLine(a, a + _aToBDir);

        Vector2 _aToBVec = _aToBDir * _offset;


        Gizmos.DrawSphere(a + _aToBVec, 0.05f);
    }

}
