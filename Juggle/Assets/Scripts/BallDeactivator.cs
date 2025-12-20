using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeactivator : MonoBehaviour
{
    public BallSpawner ballSpawner;
    public GameOverManager gameOverManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.SetActive(false);
            if (ballSpawner != null)
            {
                ballSpawner.BallGone();
            }
            if(gameOverManager != null)
            {
                gameOverManager.GameOver();
            }
        }
        else if (other.gameObject.layer == 7)
            {
                other.gameObject.SetActive(false);
            }
    }
}
