using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : Spawner
{    
    float spawnTimer = 0;

    public TimeKeeping timeKeeper;
    // Start is called before the first frame update
    protected override void Awake()
    {
        maxBalls = SettingsScript.maxBalls;
        InitializeBalls(maxBalls);
    }

    protected virtual void Update()
    {
        //Debug.Log(currentBalls + " < " + maxBalls + ": " + (currentBalls < maxBalls));
        if (currentBalls < maxBalls)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= SettingsScript.timeBetweenBalls)
            {
                SpawnBall();
                spawnTimer = 0;
            }
        }
    }
    public override void SpawnBall()
    {
        base.SpawnBall();
        if (currentBalls >= maxBalls && timeKeeper != null)
        {
            timeKeeper.StartTimer();
        }
    }
    public override void BallGone()
    {
        base.BallGone();
        if (timeKeeper != null)
        {
            timeKeeper.StopTimer();
        }
    }
    

    
    
}
