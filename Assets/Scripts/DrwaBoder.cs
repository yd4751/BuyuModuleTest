using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrwaBoder : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        int nOffset = 50;
        Vector3 ptTopLeft = new Vector3(nOffset, Screen.height - nOffset, 10);
        Vector3 ptTopRight = new Vector3(Screen.width - nOffset, Screen.height - nOffset, 10);
        Vector3 ptBottomLeft = new Vector3(nOffset, nOffset, 10);
        Vector3 ptBottomRight = new Vector3(Screen.width - nOffset, nOffset, 10);

        Vector3 worldptTopLeft = Camera.main.ScreenToWorldPoint(ptTopLeft);
        Vector3 worldptTopRight = Camera.main.ScreenToWorldPoint(ptTopRight);
        Vector3 worldptBottomLeft = Camera.main.ScreenToWorldPoint(ptBottomLeft);
        Vector3 worldptBottomRight = Camera.main.ScreenToWorldPoint(ptBottomRight);

        Debug.DrawLine(worldptTopLeft, worldptTopRight, Color.yellow);
        Debug.DrawLine(worldptBottomLeft, worldptBottomRight, Color.yellow);
        Debug.DrawLine(worldptTopLeft, worldptBottomLeft, Color.yellow);
        Debug.DrawLine(worldptTopRight, worldptBottomRight, Color.yellow);
        //
        Vector3 worldBottomMiddle = new Vector3(worldptBottomLeft.x + (worldptBottomRight.x - worldptBottomLeft.x)/2 , worldptBottomLeft.y, worldptBottomLeft.z);
        Vector3 worldRightMiddle = new Vector3(worldptBottomRight.x , worldptBottomRight.y + (worldptTopRight.y - worldptBottomRight.y)/2, worldptBottomLeft.z);
        Vector3 worldTopMiddle = new Vector3(worldptTopLeft.x + (worldptTopRight.x - worldptTopLeft.x) / 2, worldptTopLeft.y, worldptTopLeft.z);
        Vector3 worldLeftMiddle = new Vector3(worldptBottomLeft.x, worldptBottomLeft.y + (worldptTopLeft.y - worldptBottomLeft.y) / 2, worldptBottomLeft.z);
        Debug.DrawLine(worldBottomMiddle, worldRightMiddle, Color.red);
        Debug.DrawLine(worldRightMiddle, worldTopMiddle, Color.red);
        Debug.DrawLine(worldTopMiddle, worldLeftMiddle, Color.red);
        Debug.DrawLine(worldLeftMiddle, worldBottomMiddle, Color.red);
    }
}
