using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAppearance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
    }

}
