using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathLessonInterpolation : MonoBehaviour
{
    public Image _img;
    public Color colorA, colorB;
    private float current, target;
    public float lerpSpeed = 0.1f;
    public float health = 100f;
    public float maxHealth = 100f;
    float curPosInLerP = 1f;
    public float val;
    [Range(0, 160)]
    public float value;
    void OnDrawGizmos()
    {
        if(current == target) target = current == 0 ? 1 : 0;
        current = Mathf.MoveTowards(current, target, Time.deltaTime * lerpSpeed);
        
        curPosInLerP = health / maxHealth;
        _img.color = Color.Lerp(colorA, colorB, curPosInLerP);

        val = Remap(0, 160f, 0, 1f, value);
        //val = Mathf.InverseLerp(0, 160f, value);
    }
    private float Remap(float from1, float to1, float from2, float to2, float value)    
    {
        var t = Mathf.InverseLerp(from1, to1, value);
        return Mathf.Lerp(from2, to2, t);
    }

}
