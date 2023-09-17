using UnityEngine;

public class GridCube : MonoBehaviour
{
    public float moveTime = 1f;
    public bool moveFree;
    public AnimationCurve _curve;

    Vector3 startPos;
    Vector3 targetPos;
    bool isAnimating;
    float animTime;

    void Update()
    {
        //Moves Free
        if(moveFree)
            Animating();
        else
        {
            //Moves 1 by 1
            if(isAnimating)
            {
                Animating();
            }
            else
                CheckMove();
        }        
    }
    void Animating()
    {   
        if(moveFree)  
            CheckMove();   
        animTime += Time.deltaTime;

        float t = Mathf.Clamp01(animTime / moveTime);
        float tValue = _curve.Evaluate(t);

        transform.position = Vector3.LerpUnclamped(startPos, targetPos, tValue);

        if(t >= 1f)
            isAnimating = false;
    }
    void CheckMove()
    {
        void Move(KeyCode kc, Vector3 dir)
        {
            if(Input.GetKeyDown(kc))
            {
                startPos = transform.position;
                targetPos = startPos + dir;
                isAnimating = true;
                animTime = 0f;
            } 
        }
        Move(KeyCode.A, Vector2.left);
        Move(KeyCode.W, Vector2.up);
        Move(KeyCode.S, Vector2.down);
        Move(KeyCode.D, Vector2.right);
    }
    //Smoothing functions
    float QuadEaseIn(float t) => t * t;
    float QuadEaseOut(float t) => 1f - QuadEaseIn(1f - t);
    float CubicInOut(float t) => t * t * (3 - 2 * t);
    float EaseOutBack(float t) => CustomEase(5, 0, t);
    float CustomEase(float a, float b, float t)
    {
        float c3 = a + b - 2;
        float c2 = 3 - 2 * a - b;
        float t2 = t * t;
        float t3 = t2 * t;
        return c3 * t3 + c2 * t2 + a * t;
    }
}
