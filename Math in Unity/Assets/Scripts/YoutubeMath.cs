using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class YoutubeMath : MonoBehaviour
{
    [Header("Example Enablears")]
    public bool enableClock;
    public bool dotProd;
    public bool scaleChanger;
    public bool enableTrigonometry;

    [Header("Clock")]
    [Range(0f, 360f)]
    public float angleDeg = 45f;
    public float slower = 1f;

    [Header("Dot Product")]
    public Transform pointA;
    public Transform pointB;
    public float value;

    [Header("Curve Scaler")]
    public AnimationCurve _curveX;
    public AnimationCurve _curveY;
    public AnimationCurve _curveZ;
    public float elapsedTime;
    public float multiplier = 1f;

    [Header("Trigonometry")]
    [Range(0f, 90f)]
    public float angleDegV2 = 45f;
    public float restAngleDeg = 0f;
    private void OnDrawGizmos() {

        if(enableClock)
        {
            Handles.DrawWireDisc(default, Vector3.forward, 1f);
            var angleRad = Mathf.Deg2Rad * angleDeg;
            var angleTurn = (float)EditorApplication.timeSinceStartup;

            Vector2 VectorWithAngle(float _angleTurn) => new Vector2(Mathf.Cos(_angleTurn * 2 * Mathf.PI), Mathf.Sin(_angleTurn * 2 * Mathf.PI));

            Vector2 v = VectorWithAngle(slower * angleTurn);
            
            Gizmos.DrawLine(default, v);
        }
        if(dotProd)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(default, pointA.position);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(default, pointB.position);
            

            Gizmos.color = Color.yellow;
            var normA = pointA.position.normalized;
            var normB = pointB.position.normalized;
            value = Vector2.Dot(normA, pointB.position);
            Vector2 point = value * normA;
            Gizmos.DrawSphere(point, 0.1f);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointB.position, point);
        }
        if(scaleChanger)
        {
            if(elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * multiplier;
            }
            else{
                elapsedTime = 0f;
            }
            var valX = _curveX.Evaluate(elapsedTime);
            var valY = _curveY.Evaluate(elapsedTime);
            var valZ = _curveZ.Evaluate(elapsedTime);

            transform.localScale = new Vector3(valX, valY, valZ);
        }
        if(enableTrigonometry)
        {
            Vector2 Vectoral(float rad) => new Vector2(Mathf.Cos(rad),Mathf.Sin(rad));
            var rad = Mathf.Deg2Rad * angleDegV2;
            //Gizmos.color = Color.green;
            Vector2 v2 = Vectoral(rad);
            Vector2 v3 = new Vector2(-v2.x, v2.y);

            var rest = Mathf.Acos(Vector2.Dot(Vector2.up, v2)); //rest angle in radians
            
            restAngleDeg = 2f * Mathf.Rad2Deg * rest;

            if(restAngleDeg < 80f)
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            Gizmos.DrawLine(default, v2);
            Gizmos.DrawLine(default, v3);
            Handles.DrawWireArc(default, Vector3.forward, v2, restAngleDeg, 1f);  
        }
    }
}
