using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareUi : MonoBehaviour
{
    enum SideRelation { Length, Width}
    [SerializeField] float edgeBufferMagnitude = 5;
    [SerializeField] SideRelation sideRelation = SideRelation.Length;
    [SerializeField] [Range(0,1.0f)] float fractionOfScreen = .1f;
    enum HeightRelation { Top, Middle, Bottom}
    enum WidthRelation { Left, Middle, Right}
    [SerializeField] HeightRelation heightRelation = HeightRelation.Top;
    [SerializeField] WidthRelation widthRelation = WidthRelation.Left;

    // Start is called before the first frame update
    void Start()
    {
        float pixelWidth;
        int sideLength;
        if (sideRelation == SideRelation.Length)
        {
            sideLength = Screen.height;
        }
        else 
        {
            sideLength = Screen.width;
        }

        pixelWidth = sideLength * fractionOfScreen;

        GetComponent<RectTransform>().sizeDelta = new Vector2(pixelWidth, pixelWidth);
        //Debug.Log("Dimensions " + (-pixelWidth / 2));
        float positionOffsetX = 0;
        float positionOffsetY = 0;
        float edgeBufferX = 0;
        float edgeBufferY = 0;
        float uiOffsetY = 0;
        float uiOffsetX = 0;

        if (heightRelation == HeightRelation.Top)
        {
            edgeBufferY = -edgeBufferMagnitude;
            positionOffsetY = Screen.height / 2;
            uiOffsetY = -pixelWidth/2;
        }
        else if(heightRelation == HeightRelation.Bottom)
        {
            edgeBufferY = edgeBufferMagnitude;
            positionOffsetY = -Screen.height / 2;
            uiOffsetY = pixelWidth/2;
        }
        if (widthRelation == WidthRelation.Left)
        {
            edgeBufferX = edgeBufferMagnitude;
            positionOffsetX = -Screen.width /2;
            uiOffsetX = pixelWidth / 2;
        }
        else if (widthRelation == WidthRelation.Right)
        {
            edgeBufferX = -edgeBufferMagnitude;
            positionOffsetX = Screen.width/2;
            uiOffsetX = -pixelWidth / 2;
        }


        GetComponent<RectTransform>().localPosition = new Vector3(positionOffsetX + uiOffsetX + edgeBufferX, positionOffsetY + uiOffsetY + edgeBufferY, 0);
        //GetComponent<RectTransform>().localPosition = new Vector3(edgeBufferX + uiOffsetX + positionOffsetX, edgeBufferY + uiOffsetY + positionOffsetY, 0);
    }

}
