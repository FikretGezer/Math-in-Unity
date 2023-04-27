using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTransform : MonoBehaviour
{
    public Transform localObjTransform;
    public Vector2 localSpacePoint;

    private void OnDrawGizmos()
    {
        Vector2 _objPos = transform.position;
        Vector2 _right = transform.right;
        Vector2 _up = transform.up;

        DrawAxis(_objPos, _right, _up);
        DrawAxis(Vector2.zero, Vector2.right, Vector2.up);

        //Vector2 pt = worldToLocal(localSpacePoint);
        //Vector2 WorldToLocal(Vector2 worldPoint)
        //{
        //    Vector2 localOffset = _right * worldPoint.x + _up * worldPoint.y;
        //    return (Vector2)_objPos + localOffset;
        //}

        Vector2 LocalToWorld(Vector2 localPoint)
        {
            Vector2 relativePoint = localPoint - _objPos;
            float x = Vector2.Dot(relativePoint, _right);
            float y = Vector2.Dot(relativePoint, _up);
            return new Vector2(x, y);
        }
        
        localObjTransform.localPosition = LocalToWorld(localSpacePoint);
        // Vector2 pt = LocalToWorld(WorldToLocal(localSpacePoint));

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawSphere(pt, .1f);
    }
    private void DrawAxis(Vector2 _pos, Vector2 _right, Vector2 _up)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_pos, _up);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_pos, _right);
        Gizmos.color = Color.white;
    }
}
