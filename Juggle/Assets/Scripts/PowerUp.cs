using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Balloid
{
    [SerializeField] protected float disableDelay;
    [SerializeField] GameObject[] visualsToDisable;
    [SerializeField] GameObject[] visualsToEnable;
    
    protected virtual void OnEnable()
    {
        SetRendering(false);
        base.OnEnable();
        rb.isKinematic = false;
    }
    protected void SetRendering(bool isDisabling)
    {
        //Debug.Log("Set Rendering");
        foreach (GameObject g in visualsToDisable)
        {
            g.SetActive(!isDisabling);
        }
        foreach (GameObject g in visualsToEnable)
        {
            g.SetActive(isDisabling);
        }
    }
    protected IEnumerator DelayedDisable(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    public override void OnClickDown()
    {
        base.OnClickDown();
        SetRendering(true);
        rb.isKinematic = true;
    }
}
