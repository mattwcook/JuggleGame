using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ViewSize 
{
    static Vector3 sceneDimensions;
    public static float GetViewWidth()
    {
        if (sceneDimensions == Vector3.zero || sceneDimensions == null) 
        {
            CalculateDimensions();
        }
        return sceneDimensions.x;
    }
    public static float GetViewHeight()
    {
        if (sceneDimensions == Vector3.zero || sceneDimensions == null)
        {
            CalculateDimensions();
        }
        return sceneDimensions.y;
    }
    static void CalculateDimensions()
    {
        sceneDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }
}
