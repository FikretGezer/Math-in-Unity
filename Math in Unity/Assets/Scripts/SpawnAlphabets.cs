using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAlphabets : MonoBehaviour
{
    Camera cam;
    private float xMin = -9f, xMax = 9f, yMin = -3f, yMax = 5f;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                //hit.collider.gameObject.SetActive(false);
                ChangePos(hit.collider.gameObject);
            }
        }
    }

    private void ChangePos(GameObject obj)
    {
        float randomX = Random.Range(xMin, xMax);
        float randomY = Random.Range(yMin, yMax);
        obj.transform.position = new Vector2(randomX, randomY);
        //obj.SetActive(true);
    }
}
