using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : Balloid
{
    
    
    float clickForceVert = 15.0f;
    
    
    [SerializeField] SpriteRenderer spriteRenderer;

    
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        RandomizeAppearance();
    }

    
    void LaunchVelocity(float vertical)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        //rb.velocity = new Vector3(rb.velocity.x, vertical, 0);
        rb.velocity = new Vector3(Random.Range(-3.0f,3.0f), vertical, 0);
    }

    public override void OnClickDown()
    {
        base.OnClickDown();
        if (gameOver == true)
        {
            return;
        }
        //Launch(clickForceVert, 0);
        LaunchVelocity(10);
        CustomEvents.AddPoints(1);
        
    }

    void RandomizeAppearance()
    {
        if (spriteRenderer == null)
        {
            GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        }
        else
        {
            spriteRenderer.color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        }
    }
    

    
    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        rb.velocity = Vector3.zero;
    //        rb.useGravity = false;
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    OnClickDown();
    //}

}
