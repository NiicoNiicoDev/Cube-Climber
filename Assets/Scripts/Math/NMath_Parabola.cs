using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NMath_Parabola
{

    public enum ParabolaSolveFor
    { 
        MaxHeight,
        MaxDistance,
        Time
    }
        
    
    
    public static float Parabola(Vector3 StartPos, float Velocity, float AngleInDegrees, float time)
    {

        Vector3[] XCoords = new Vector3[25];
        Vector3[] YCoords = new Vector3[25];
        
        float y;

        float halfG = (float)0.5 * -Physics.gravity.y;

        float sinAngle = Mathf.Sin(AngleInDegrees * Mathf.Deg2Rad);

        float timePow = Mathf.Pow(time, 2);

        Debug.Log(halfG);
        
        Debug.Log(StartPos.y);
        Debug.Log(sinAngle * Velocity);
        Debug.Log(halfG * timePow);

        float a = (sinAngle * Velocity) * time;
        float b = (halfG * timePow);
        
        y = StartPos.y + a - b;
        return y;
    }

    public static float SolveParabola(ParabolaSolveFor solveFor, Vector3 StartPos, float Velocity, float AngleInDegrees, float time)
    {
        
        return 0;
    }
}
