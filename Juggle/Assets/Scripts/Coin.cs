using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    
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


}
