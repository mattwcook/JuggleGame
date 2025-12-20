using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject ballTypeToSpawn;
    [SerializeField] protected Transform spawnableParent;
    float viewWidth;
    float startHeight;
    protected int currentBalls = 0;
    protected int maxBalls = 10;
    bool gameOver = false;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        InitializeBalls(maxBalls);
    }
    protected void InitializeBalls(int numBalls)
    {
        viewWidth = ViewSize.GetViewWidth();
        startHeight = -(ViewSize.GetViewHeight() + ballTypeToSpawn.transform.lossyScale.y + 1);
        for (int i = 0; i < numBalls; i++)
        {
            GameObject newBall = Instantiate(ballTypeToSpawn, spawnableParent);
            newBall.name = newBall.name.Replace("(Clone)", i.ToString());
            if (newBall.GetComponent<Ball>() != null)
            {
                newBall.GetComponent<Ball>().SetRenderOrder(i);
            }
            newBall.SetActive(false);
        }
    }
    private void Start()
    {
        CustomEvents.onGameOver += GameOverListener;
    }
    public virtual void SpawnBall()
    {
        if (gameOver)
        {
            return;
        }
        Transform ballToSpawn = GetNextBall();

        if (ballToSpawn == null)
        {
            return;
        }
        float xPosition = Random.Range(-(viewWidth - ballTypeToSpawn.transform.lossyScale.x), viewWidth - ballTypeToSpawn.transform.lossyScale.x);
        ballToSpawn.position = new Vector3(xPosition, startHeight, 0);
        ballToSpawn.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ballToSpawn.gameObject.SetActive(true);
        currentBalls++;
        
    }
    Transform GetNextBall()
    {
        foreach (Transform child in spawnableParent)
        {
            if (child.gameObject.activeSelf == false)
            {
                return child;
            }
        }
        return null;
    }
    public virtual void BallGone()
    {
        currentBalls -= 1;
        
    }
    void GameOverListener()
    {
        gameOver = true;
    }
    private void OnDestroy()
    {
        CustomEvents.onGameOver -= GameOverListener;
    }


}
