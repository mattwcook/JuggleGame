using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, Clickable
{

    public void OnClickDown()
    {
        GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //if (Input.GetKeyDown(KeyCode.Space))
       //{
       //    Debug.Log("Space Pressed");
       //}
       //if (Input.GetMouseButtonDown(0))
       //{
       //    Debug.Log("Mouse Down");
       //}
       //Debug.Log("Fire: " + Input.GetAxis("Fire1"));
    }
}
