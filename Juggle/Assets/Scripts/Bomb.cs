using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PowerUp
{
    float explosionForce = 1000;
    float explosionRadius;
    protected float initialForceVertical = 17.0f;

    //float disableDelay = 1.5f;
    float gameOverDelay = 1;
    private void OnEnable()
    {
        SetRendering(false);
        base.OnEnable();
        //rb.isKinematic = false;
    }
    public override void OnClickDown()
    {
        base.OnClickDown();
        if (gameOver == true)
        {
            return;
        }
        CustomEvents.DisableWalls();
        //SetRendering(true);
        Explosion();
        StartCoroutine(DelayedDisable(disableDelay));
        //rb.isKinematic = true;
        //StartCoroutine(DelayedGameOver());
        CustomEvents.DelayedGameOver(gameOverDelay);
    }
    void Explosion()
    {
        explosionRadius = ViewSize.GetViewHeight() * 2;
        //explosionRadius = 100;
        // Blow Shit Up
        List<Rigidbody> detectedRigidBodies = new List<Rigidbody>();
        foreach (Collider collider in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null && detectedRigidBodies.Contains(rb) == false)
            {
                detectedRigidBodies.Add(rb);
                collider.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(0, -3.0f, 0, ForceMode.Force);
    }
    public override float GetInitialForce()
    {
        return initialForceVertical;
    }


    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        CustomEvents.GameOver();
    }
}
