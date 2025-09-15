using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWalls : MonoBehaviour
{
    [SerializeField] Transform leftWall;
    [SerializeField] Transform rightWall;
    [SerializeField] Transform bottomTrigger;
    [SerializeField] LineRenderer backdrop;

    // Start is called before the first frame update
    void Start()
    {
        float leftWallThickness = leftWall.lossyScale.x;
        float rightWallThickness = rightWall.lossyScale.x;
        Vector3 camPosition = Camera.main.transform.position;
        float viewWidth = ViewSize.GetViewWidth();
        leftWall.position = new Vector3(camPosition.x - viewWidth - leftWallThickness / 2, camPosition.y, 0);
        rightWall.position = new Vector3(camPosition.x + viewWidth + rightWallThickness / 2, camPosition.y, 0);

        float viewHeight = ViewSize.GetViewHeight();
        bottomTrigger.position = new Vector3(0,camPosition.y-(viewHeight + bottomTrigger.position.y / 2 + 5), 0);

        
        Vector3[] backdropPositions = new Vector3[2];
        backdropPositions[0] = new Vector3(0, viewHeight, 0);
        backdropPositions[1] = new Vector3(0, -viewHeight, 0);
        backdrop.SetPositions(backdropPositions);
    }


}
