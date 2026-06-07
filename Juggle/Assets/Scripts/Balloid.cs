using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloid : MonoBehaviour, Clickable
{
    protected Rigidbody rb;
    protected AudioSource audioSource;
    protected float initialForceVert = 15.0f;
    protected float initialForceHorz = 3.0f;
    protected bool gameOver = false;

    [SerializeField] Collider trailCollider;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        CustomEvents.onGameOver += GameOverListener;
        rb = GetComponent<Rigidbody>();
    }
    void GameOverListener()
    {
        gameOver = true;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        //rb.velocity = Vector3.zero;
    }
    public virtual void OnClickDown()
    {
        
        if (gameOver)
        {
            return;
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
    protected virtual void OnEnable()
    {
        float horzForce;
        if (transform.position.x >= Camera.main.transform.position.x)
        {
            horzForce = Random.Range(-initialForceHorz, 0);
        }
        else
        {
            horzForce = Random.Range(0, initialForceHorz);
        }
        Launch(GetInitialForce(), horzForce);
    }
    void Launch(float vertical, float lateral)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.AddForce(new Vector3(lateral, vertical, 0), ForceMode.Impulse);
    }
    private void FixedUpdate()
    {
        if (trailCollider != null)
        {
            //Debug.Log(rb.velocity.magnitude);
            //if(rb.velocity.magnitude >= 10)
            //{
            //    Debug.Log("Fast");
            //}
            trailCollider.gameObject.SetActive(rb.velocity.y <= -9);
        }

    }
    public void SetRenderOrder(int order, Transform parent = null)
    {
        if (parent == null)
        {
            parent = transform;
        }
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder += order;
        }
        foreach (Transform child in parent)
        {
            SetRenderOrder(order, child);
        }
    }
    public virtual float GetInitialForce()
    {
        return initialForceVert;
    }
    private void OnDestroy()
    {
        CustomEvents.onGameOver -= GameOverListener;
    }

}
