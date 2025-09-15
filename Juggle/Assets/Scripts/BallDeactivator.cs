using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeactivator : MonoBehaviour
{
    public BallSpawner ballSpawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.SetActive(false);
            ballSpawner.BallFell();
        }
    }
}
