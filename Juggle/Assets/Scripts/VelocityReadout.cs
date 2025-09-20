using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class VelocityReadout : MonoBehaviour
{

    public TMP_Text readout;
    public Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        readout.text = rb.velocity.magnitude.ToString(".0");
    }
    private void Start()
    {
        readout = GetComponent<TMP_Text>();
    }
}
