using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trig : MonoBehaviour
{
    const float TAU = 6.28318530718f;
    int dotCount = 16;
    private void OnDrawGizmos()
    {
        for (int i = 0; i < dotCount; i++)
        {
            float alpha = i * TAU / dotCount;
            float x = Mathf.Cos(alpha);
            float y = Mathf.Sin(alpha);


            Gizmos.DrawSphere(transform.position + new Vector3(x, y), .2f);
        }
    }
}
