using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    int maxBalls;
    int currentBalls = 0;
    float startHeight;
    float viewWidth;
    public GameObject ball;
    public Transform ballParent;
    float timer = 0;

    public TimeKeeping timeKeeper;
    // Start is called before the first frame update
    void Awake()
    {
        maxBalls = SettingsScript.maxBalls;
        for(int i = 0; i < maxBalls; i++)
        {
            GameObject newBall = Instantiate(ball, ballParent);
            if (newBall.GetComponent<Ball>() != null)
            {
                newBall.GetComponent<Ball>().SetRenderOrder(i);
            }
            newBall.SetActive(false);
        }
        startHeight = -(ViewSize.GetViewHeight() + ball.transform.lossyScale.y + 1);
        viewWidth = ViewSize.GetViewWidth();
    }
    IEnumerator WaitBallSpawn()
    {
        yield return new WaitForSeconds(SettingsScript.timeBetweenBalls);
        SpawnBall();
    }
    private void Update()
    {
        if (currentBalls < maxBalls)
        {
            timer += Time.deltaTime;
            if (timer >= SettingsScript.timeBetweenBalls)
            {
                SpawnBall();
                timer = 0;
            }
        }
    }
    public void BallFell()
    {
        //StartCoroutine(WaitBallSpawn());
        currentBalls -= 1;
        timeKeeper.StopTimer();
    }

    public void SpawnBall()
    {
        Transform ballToSpawn = GetNextBall();

        if(ballToSpawn == null)
        {
            return;
        }
        float xPosition = Random.Range(-(viewWidth - ball.transform.lossyScale.x), viewWidth - ball.transform.lossyScale.x);
        ballToSpawn.position = new Vector3(xPosition, startHeight, 0);
        ballToSpawn.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ballToSpawn.gameObject.SetActive(true);
        currentBalls++;
        if (currentBalls >= maxBalls)
        {
            timeKeeper.StartTimer();
        }
    }
    Transform GetNextBall()
    {
        foreach(Transform child in ballParent.transform)
        {
            if (child.gameObject.activeSelf == false)
            {
                return child;
            }
        }
        return null;
    }
}
