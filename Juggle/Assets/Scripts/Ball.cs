using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour, Clickable
{
    Rigidbody rb;
    float initialForceVert = 15.0f;
    float initialForceHorz = 3.0f;
    float clickForceVert = 15.0f;
    AudioSource audioSource;
    [SerializeField] Collider trailCollider;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void OnEnable()
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
        RandomizeAppearance();
        Launch(initialForceVert, horzForce); 
    }

    void Launch(float vertical, float lateral)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.AddForce(new Vector3(lateral, vertical, 0), ForceMode.Impulse);
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

    public void OnClickDown()
    {
        //Launch(clickForceVert, 0);
        LaunchVelocity(10);
        if (audioSource != null)
        {
            audioSource.Play();
        }
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
        foreach(Transform child in parent)
        {
            SetRenderOrder(order, child);
        }
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
