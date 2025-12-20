using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomEvents 
{
    public static event Action<int> onAddPoints;
    public static event Action onGameOver;
    public static event Action<float> onDelayedGameOver;
    public static event Action onDisableWalls;
    public static void AddPoints(int numPoints)
    {
        onAddPoints?.Invoke(numPoints);
    }
    public static void GameOver()
    {
        onGameOver?.Invoke();
    }
    public static void DelayedGameOver(float delay)
    {
        onDelayedGameOver?.Invoke(delay);
    }
    public static void DisableWalls()
    {
        onDisableWalls?.Invoke();
    }
}
