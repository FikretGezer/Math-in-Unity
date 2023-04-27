using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float maxShakeTime = .4f;
    public float radius = .4f;

    private bool isShaking;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isShaking)
        {
            StartCoroutine(ShakeThatAss(maxShakeTime,radius));
        }
    }
    IEnumerator ShakeThatAss(float maxShakeTime, float radius)
    {
        Vector3 basePosition = transform.position;
        float elapsedTime = 0f;
        isShaking = true;
        while (elapsedTime<maxShakeTime)
        {
            Vector3 tempPos = Random.insideUnitSphere * radius;
            transform.position = basePosition + new Vector3(tempPos.x, tempPos.y);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = basePosition;
        isShaking = false;
    }
}
