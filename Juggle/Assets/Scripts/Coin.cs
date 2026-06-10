using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    protected float initialForceVertical = 13;
    public override void OnClickDown()
    {
        base.OnClickDown();
        if (gameOver)
        {
            return;
        }
        CustomEvents.AddPoints(1);
        StartCoroutine(DelayedDisable(disableDelay));
        //rb.isKinematic = true;
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(0, 2.0f, 0);
    }
    public override float GetInitialForce()
    {
        return initialForceVertical;
    }

}
