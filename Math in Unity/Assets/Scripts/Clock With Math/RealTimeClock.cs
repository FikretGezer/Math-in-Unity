using System;
using UnityEditor;
using UnityEngine;

public class RealTimeClock : MonoBehaviour
{
    [Range(0, 60)]
    [SerializeField] private float secondsTest;
    [SerializeField] private float radiusOfSphere;
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Vector2 positionOfLabel;
    [SerializeField] private Color colorOfClock;
    [SerializeField] private Gradient colorOfClockGrad;
    [Header("Booleans")]
    [SerializeField] private bool smoothSeconds;
    [SerializeField] private bool smoothMinutes;
    [SerializeField] private bool smoothHours;
    [Header("Second")]
    [SerializeField] private float thicknessOfSeconds;
    [SerializeField] private float lengthOfSeconds;
    [Header("Hour")]
    [SerializeField] private float thicknessOfHour;
    [SerializeField] private float lengthOfHour;
    [Header("Clock Hand Sec")]
    [SerializeField] private float thicknessOfClockHandSec;
    [SerializeField] private float lengthOfClockHandSec;
    [SerializeField] private Color colorOfClockHandSec;
    [Header("Clock Hand Min")]
    [SerializeField] private float thicknessOfClockHandMin;
    [SerializeField] private float lengthOfClockHandMin;
    [SerializeField] private Color colorOfClockHandMin;
    [Header("Clock Hand Hour")]
    [SerializeField] private float thicknessOfClockHandHour;
    [SerializeField] private float lengthOfClockHandHour;
    [SerializeField] private Color colorOfClockHandHour;

    private float current, target;
    private Color colorOfThicks;
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        //Changes color of clock over time
        if(current == 1f) target = 0f;
        else if(current == 0f) target = 1f;
        current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
        colorOfThicks = colorOfClockGrad.Evaluate(current);

        //Circle for the clock
        using (new Handles.DrawingScope(colorOfThicks))
            Handles.DrawWireDisc(default, Vector3.forward, 1f);

        //Middle point in the centre
        Gizmos.color = colorOfThicks;
        Gizmos.DrawSphere(default, radiusOfSphere);

        //Resets gizmos color
        Gizmos.color = Color.white;

        //Gets current time
        DateTime time = DateTime.Now;

        //Smooths the sec hand movement
        float seconds = time.Second;
        float minutes = time.Minute;
        float hours = time.Hour;
        Handles.Label(positionOfLabel, $"{hours:00}:{minutes:00}:{seconds:00}");
        if(smoothSeconds)
            seconds += time.Millisecond / 1000f;
        if(smoothMinutes)
            minutes += time.Second / 60f;
        if(smoothHours)
            hours += time.Minute / 60f;

        //Draws Clock Hands
        //DrawClockHand(SecorMinToDir(seconds), lengthOfClockHandSec, thicknessOfClockHandSec, colorOfClockHandSec);
        DrawClockHand(SecorMinToDir(seconds), lengthOfClockHandSec, thicknessOfClockHandSec, colorOfThicks);

        //Draws Min Clock Hand        
        DrawClockHand(SecorMinToDir(minutes), lengthOfClockHandMin, thicknessOfClockHandMin, colorOfClockHandMin);

        //Draws Hour Clock Hand                
        DrawClockHand(HourToDir(hours), lengthOfClockHandHour, thicknessOfClockHandHour, colorOfClockHandHour);

        //Draws second thicks
        for (int i = 0; i < 60f; i++)
        {
            var dir = SecorMinToDir(i);
            DrawTick(dir, lengthOfSeconds, thicknessOfSeconds);
        }
        //Draws hour thicks
        for (int i = 0; i < 12f; i++)
        {
            var dir = HourToDir(i);
            DrawTick(dir, lengthOfHour, thicknessOfHour);
        }
    }
    //Draws tick on clock
    private void DrawTick(Vector2 dir, float length, float thickness)
    {
        using(new Handles.DrawingScope(colorOfThicks))
            Handles.DrawLine(dir, dir * (1f - length), thickness);
    }
    //Draws clock hands
    private void DrawClockHand(Vector2 dir, float length, float thickness, Color color)
    {
        using(new Handles.DrawingScope(color))
            Handles.DrawLine(default, dir * length, thickness);
    }
    //Calculates direction by using giving angle in radians
    private Vector2 AngToDir(float angleRad) => new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    
    //Calculates direction for sec or min hand
    private Vector2 SecorMinToDir(float seconds)
    {
        float t = seconds / 60;
        return ValueToDir(t);
    }
    //Calculates direction for hour hand
    private Vector2 HourToDir(float hour)
    {
        float t = hour / 12;
        return ValueToDir(t);
    }
    //Calculate direction for current step
    private Vector2 ValueToDir(float t)
    {
        float angleRad = (0.25f - t) * 2 * Mathf.PI;
        return AngToDir(angleRad);
    }
    
}
