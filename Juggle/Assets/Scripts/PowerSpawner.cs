using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : Spawner
{
    [SerializeField] float spawnMinTime;
    [SerializeField] float spawnMaxTime = 10.0f;
    float spawnTime;
    float timer = 0;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnMinTime < 0)
        {
            spawnMinTime = 0;
        }
        if (spawnMaxTime < spawnMinTime )
        {
            spawnMaxTime = spawnMinTime + 5;
        }
        spawnTime = CalculateSpawnTime();

        CustomEvents.onGameOver += GameOverListener;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                spawnTime = CalculateSpawnTime();
                SpawnBall();
            }
        }
    }

    void GameOverListener()
    {
        gameOver = true;
    }

    float CalculateSpawnTime()
    {
        return Random.Range(spawnMinTime, spawnMaxTime);
    }
    private void OnDestroy()
    {
        CustomEvents.onGameOver -= GameOverListener;
    }
}
