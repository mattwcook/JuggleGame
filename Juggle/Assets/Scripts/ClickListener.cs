using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickListener : MonoBehaviour
{
    bool clickStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchSupported)
        {
            //Debug.Log("Touch Count " + Input.touchCount);
            if (Input.touchCount > 0 && clickStart == false)
            {
                clickStart = true;
                ClickDown(Input.touches[0].position);
            }
            else if (Input.touchCount == 0 && clickStart == true)
            {
                clickStart = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            ClickDown(Input.mousePosition);
        }
        
    }
    void ClickDown(Vector2 clickPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(clickPosition);
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenToWorldPoint(clickPosition), Vector3.forward);
        List<Clickable> detectedClickables = new List<Clickable>();
        foreach (RaycastHit hit in hits)
        {
            Clickable clickable = hit.transform.GetComponent<Clickable>();
            if (clickable != null && detectedClickables.Contains(clickable) == false)
            {
                detectedClickables.Add(clickable);
                clickable.OnClickDown();
                
            }
            
        }
    }

    //Transform GetParentRigidBody(Transform toSearch)
    //{
    //    if(toSearch.GetComponent<Rigidbody>() != null)
    //    {
    //        return toSearch;
    //    }
    //    return GetParentRigidBody(toSearch.parent);
    //}
}
