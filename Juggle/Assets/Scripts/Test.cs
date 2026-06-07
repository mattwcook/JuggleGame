using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private Camera cam;

    [Header("Zoom Settings")]
    public float zoomSpeed = 2f;
    public float minSize = 25f;
    public float maxSize = 60f;
    Vector3 prevPosition;

    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            ZoomToMouse(scrollInput);
        }
    }
    void ZoomToMouse(float increment)
    {
        Debug.Log("Increment: " + increment);
        Vector3 mouseWorldBeforeZoom = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        Debug.Log(mouseWorldBeforeZoom);

        //cam.fieldOfView -= increment * zoomSpeed;
        //cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minSize, maxSize);
        cam.transform.position += cam.transform.forward *increment * Time.deltaTime * zoomSpeed;
        //Vector3 mouseWorldAfterZoom = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseWorldAfterZoom = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        Vector3 positionDifference = mouseWorldBeforeZoom - mouseWorldAfterZoom;
        //Vector3 positionDifference = mouseWorldBeforeZoom - prevPosition;
        cam.transform.position += new Vector3(positionDifference.x, positionDifference.y, 0);
        //prevPosition = mouseWorldBeforeZoom;
    }

    void ZoomToMouse2(float increment)
    {
        // 1. Get the world position of the mouse before changing the zoom level
        Vector3 mouseWorldBeforeZoom = cam.ScreenToWorldPoint(Input.mousePosition);

        // 2. Adjust the orthographic camera size based on input
        cam.orthographicSize -= increment * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);

        // 3. Get the new world position of the mouse after zooming
        Vector3 mouseWorldAfterZoom = cam.ScreenToWorldPoint(Input.mousePosition);

        // 4. Calculate the difference and shift the camera to bridge the gap
        Vector3 positionDifference = mouseWorldBeforeZoom - mouseWorldAfterZoom;
        transform.position += positionDifference;
    }
}
