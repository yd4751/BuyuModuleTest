using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour {

    // Use this for initialization
    
    public float Speed;
    public float turnSpeed;
    private bool bCanMove = false;
    private int nCurPos = 0;
    private PathPointInfo curPosInfo;

    Vector3 lastPos;
    void Start ()
    {
        bCanMove = UpdatePosInfo();
        lastPos = new Vector3(curPosInfo.x, curPosInfo.y, 0);
        lastPos = Camera.main.ScreenToWorldPoint(lastPos);
        lastPos.z = transform.position.z;
        transform.position = lastPos;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log("xxxx:" + transform.hasChanged);
        if (transform.hasChanged)
        {
            bCanMove = UpdatePosInfo();
            lastPos = new Vector3(curPosInfo.x, curPosInfo.y, 0);
            lastPos = Camera.main.ScreenToWorldPoint(lastPos);
            lastPos.z = transform.position.z;
        }
        
        if (bCanMove)
        {
            //插值移动
            //Vector3 curDirction = lastPos - transform.position;
            //transform.Translate(curDirction * Time.deltaTime);
            transform.position = lastPos;

            //获得方向
            //Vector3 direc = lastPos - transform.position;
            //direc.z = 0;// transform.position.z;
            //绝对朝向
            // transform.right = direc;

            //Debug.Log(" Cur:" + transform.position.ToString());
        }
        //LookAtMouse();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        bCanMove = false;
        Debug.Log("OnTrigger :" + other.name);
        //销毁
        Destroy(other.gameObject);
    }

    bool UpdatePosInfo()
    {
        if (nCurPos >= SharedInfo.defaultPathInfo.Length)
        {
            return false;
        }

        curPosInfo = SharedInfo.defaultPathInfo[nCurPos];
        ++nCurPos;
        return true;
    }
    void LookAtMouse()
    {
        Vector3 ptStart = new Vector3(0, 540, 10);
        Vector3 ptEnd = new Vector3(1920, 540, 10);
        Vector3 millLine = Camera.main.ScreenToWorldPoint(ptEnd) - Camera.main.ScreenToWorldPoint(ptStart);
        Debug.DrawLine(Camera.main.ScreenToWorldPoint(ptStart), Camera.main.ScreenToWorldPoint(ptEnd), Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newDirect = Input.mousePosition;// - Vector3.zero;  //也是按原点计算的夹角
            newDirect.z = 0;
            millLine.z = 0;
            //newDirect.Normalize();

            //float target = Mathf.Atan2(newDirect.y, newDirect.x) * Mathf.Rad2Deg;
            float target = Vector3.Angle(millLine, Camera.main.ScreenToWorldPoint(newDirect));
            //Debug.Log("Test:" + target);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, target), turnSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0, 0, 30 * turnSpeed));
            //Mathf.
        }
    }
}

