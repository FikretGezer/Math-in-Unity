using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color colorA;
    public Color colorB;
    public Renderer _renderer;
    bool isColorChanged;
    public float b;
    public float current, target;
    Color colorTarget;
    private void Awake()
    {
        _renderer.sharedMaterial.color = Color.white;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            current = 0f;
            isColorChanged = true;

            if(colorA == Color.white)
                target = 1f;
            else
                target = 0.5f;
        }
        if(isColorChanged)
        {
            if(current >= target) 
            {
                isColorChanged = false;                
                colorA = colorTarget;
                
            }
            else 
            {
                
                current += b * Time.deltaTime;
                float t = current / 1f;
                colorTarget = Color.Lerp(colorA, colorB, t);
                _renderer.sharedMaterial.color = colorTarget;            
            }
        }
    }
    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0,0,0,0);
        foreach(Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }
}
