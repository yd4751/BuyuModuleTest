using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {


    Vector3 vLeftBottom;
    Vector3 vRightTop;
    // Use this for initialization
    private void Start()
    {
        float nWidth = GetComponent<BoxCollider2D>().bounds.size.x * 100;
        float nHeight = GetComponent<BoxCollider2D>().bounds.size.y * 100;
        float nOffsetx = (Screen.width - nWidth)/2;
        float nOffsety = (Screen.height - nHeight)/2;


        vLeftBottom = new Vector3(nOffsetx, nOffsety, 0);
        vRightTop = new Vector3(nWidth + vLeftBottom.x, nHeight + vLeftBottom.y, vLeftBottom.z);
        vLeftBottom = Camera.main.ScreenToWorldPoint(vLeftBottom);
        vRightTop = Camera.main.ScreenToWorldPoint(vRightTop);
        //Debug.Log(vLeftBottom.ToString() + "  : " + vRightTop.ToString());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            //Debug.Log(collision.name);
            collision.gameObject.GetComponent<bullet>().StopMove();
            ReachScreenBoder(collision.gameObject);
            collision.gameObject.GetComponent<bullet>().StartMove();
        }
    }

    void ReachScreenBoder(GameObject target)
    {
        float x = target.transform.position.x;
        float y = target.transform.position.y;

        if (x >= vRightTop.x)
        {
            OnTouchRight(gameObject, target);
        }
        else if (x <= vLeftBottom.x)
        {
            OnTouchLeft(gameObject, target);
        }
        else
        {
            if (y >= vRightTop.y)
            {
                OnTouchTop(gameObject, target);
            }
            else if (y < vLeftBottom.y)
            {
                OnTouchBottom(gameObject, target);
            }
        }

    }
 
    void OnTouchRight(GameObject wall,GameObject collision)
    {
        //Debug.Log("OnTouchRight");
        float angle = 0;
        Transform target = wall.transform;
        Transform changeObject = collision.transform;
        angle = Vector3.Angle(changeObject.up, target.up);

        if (0 < angle && angle < 90)
        {
            changeObject.Rotate(0f, 0f, angle * 2);
        }
        else if (90 < angle && angle < 180)
        {
            angle = 180 - angle;
            changeObject.Rotate(0f, 0f, -angle*2);
        }
        else
        {
            changeObject.Rotate(0f, 0f, 180);
        }
    }
    void OnTouchLeft(GameObject wall, GameObject collision)
    {
        //Debug.Log("OnTouchLeft");
        float angle = 0;
        Transform target = wall.transform;
        Transform changeObject = collision.transform;
        angle = Vector3.Angle(changeObject.up, target.up);

        if (0 < angle && angle < 90)
        {
            changeObject.Rotate(0f, 0f, -angle * 2);
        }
        else if (90 < angle && angle < 180)
        {
            angle = 180 - angle;
            changeObject.Rotate(0f, 0f, angle * 2);
        }
        else
        {
            changeObject.Rotate(0f, 0f, 180);
        }
    }
    void OnTouchTop(GameObject wall, GameObject collision)
    {
        //Debug.Log("OnTouchTop");
        float angle = 0;
        Transform target = wall.transform;
        Transform changeObject = collision.transform;
        angle = Vector3.Angle(changeObject.up, target.right);

        if (0 < angle && angle < 90)
        {
            changeObject.Rotate(0f, 0f, -angle * 2);
        }
        else if (90 < angle && angle < 180)
        {
            angle = 180 - angle;
            changeObject.Rotate(0f, 0f, angle*2);
        }
        else
        {
            changeObject.Rotate(0f, 0f, 180);
        }
    }
    void OnTouchBottom(GameObject wall, GameObject collision)
    {
        //Debug.Log("OnTouchBottom");
        float angle = 0;
        Transform target = wall.transform;
        Transform changeObject = collision.transform;
        angle = Vector3.Angle(changeObject.up, target.right);

        if (0 < angle && angle < 90)
        {
            changeObject.Rotate(0f, 0f, angle * 2);
        }
        else if (90 < angle && angle < 180)
        {
            angle = 180 - angle;
            changeObject.Rotate(0f, 0f, -angle * 2);
        }
        else
        {
            changeObject.Rotate(0f, 0f, 180);
        }
    }
}
