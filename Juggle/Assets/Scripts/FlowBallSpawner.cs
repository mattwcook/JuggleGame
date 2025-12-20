using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowBallSpawner : BallSpawner
{
    float reductionTimeThreshold = 5.0f;
    float increaseTimeThreshold = 30.0f;
    float[] timeHistory = new float[] { 0, 0, 0 };
    float flowTimer = 0;
    bool ballClicked = false;
    int noEffortCounter = 0;
    protected override void Awake()
    {
        InitializeBalls(100);
        maxBalls = SettingsScript.maxBalls;
        CustomEvents.onAddPoints += BallClickListener;
    }
    void BallClickListener(int i)
    {
        // i irrelevant, just repurposing an existing event
        ballClicked = true;
        noEffortCounter = 0;
    }
    public override void BallGone()
    {
        base.BallGone();
        
        flowTimer = 0;
        float lastTime = timeKeeper.GetLastTime();
        if (ballClicked == true)
        {
            ShiftTimeHistory(lastTime);
            float meanTime = GetMeanTime(timeHistory);
            if (maxBalls > 1 && IsHistoryComplete() && meanTime < reductionTimeThreshold)
            {
                maxBalls -= 1;
                timeHistory = new float[] { 0, 0, 0 };
            }
        }
        else
        {
            noEffortCounter += 1;
            if (noEffortCounter >= 3)
            {
                timeHistory = new float[] { 0, 0, 0 };
            }
        }
        ballClicked = false;
    }
    protected override void Update()
    {
        if (timeKeeper.IsTimeRunning())
        {
            flowTimer += Time.deltaTime;
        }

        if (flowTimer >= increaseTimeThreshold)
        {
            maxBalls += 1;
            flowTimer = 0;
            timeHistory = new float[] { 0, 0, 0 };
        }
        base.Update();
    }
    void ShiftTimeHistory(float newEntry)
    {
        for (int i = 0; i < timeHistory.Length - 1; i++)
        {
            timeHistory[i] = timeHistory[i + 1];
        }
        timeHistory[^1] = newEntry;

        string arrayPrint = "";
        foreach(float entry in timeHistory) 
        {
            arrayPrint += entry + ", ";
        }
        Debug.Log(arrayPrint[..^2]);
    }
    bool IsHistoryComplete()
    {
        foreach(float time in timeHistory)
        {
            if (time == 0)
            {
                return false;
            }
        }
        return true;
    }
    float GetMeanTime(float[] timeArray)
    {
        float mean = 0;
        foreach (float time in timeArray)
        {
            mean += time;
        }
        return mean / timeArray.Length;
    }
    bool AllValuesBeneathThreshold(float[] valueArray, float threshold)
    {
        foreach(float value in valueArray)
        {
            if (value >= threshold)
            {
                return false;
            }
        }
        return true;
    }

}
