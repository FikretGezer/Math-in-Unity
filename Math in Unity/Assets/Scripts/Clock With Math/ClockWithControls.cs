using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClockWithControls : MonoBehaviour
{
    [SerializeField] private float radiusOfSphere;
    [SerializeField] private float lerpSpeed;
    [Range(0, 100)] [SerializeField] private float clockSpeedController;
    [SerializeField] private Color colorOfClock;
    [SerializeField] private Gradient colorOfClockGradient;
    [SerializeField] private Vector2 positionOfTimeLabel;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _textOfSpeedOfTime;

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
    private float sec, min, hour;
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Handles.matrix = transform.localToWorldMatrix;

        //Control speed of time
        clockSpeedController = _slider.value;
        _textOfSpeedOfTime.text = ((int)clockSpeedController).ToString();
        //Changes color of clock over time
        if(current == 1f) target = 0f;
        else if(current == 0f) target = 1f;
        current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
        colorOfThicks = colorOfClockGradient.Evaluate(current);

        //Circle for the clock
        using (new Handles.DrawingScope(colorOfThicks))
            Handles.DrawWireDisc(default, Vector3.forward, 1f);

        //Middle point in the centre
        Gizmos.color = colorOfThicks;
        Gizmos.DrawSphere(default, radiusOfSphere);

        //Resets gizmos color
        Gizmos.color = Color.white;

        //Creates a custom time that we can control
        sec += Time.deltaTime * clockSpeedController;
        if(sec > 59)
        {
            sec = 0f;
            min++;
            if(min > 59)
            {
                min = 0;
                hour++;
                if(hour > 23)
                {
                    hour = 0f;
                }
            }
        }

        //Time parameters
        float seconds = sec;
        float minutes = min;
        float hours = hour;

        //Create a label that shows time as a text
        Handles.Label(positionOfTimeLabel, $"{hours:00}:{minutes:00}:{seconds:00}");
        
        //Smooths the clock hands movements
        if(smoothMinutes)
            minutes += sec / 60f;
        if(smoothHours)
            hours += minutes / 60f;

        //Draws Sec Clock Hand
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
