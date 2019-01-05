using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour {

    // Use this for initialization
    
    public float Speed;
    public float turnSpeed;

    private int nCurPos = 0;
    public int nPathType = 0;
    private PathPointInfo curPosInfo;
    private bool bInitDone = false;
    private int id = -1;

    Vector3 vNextPos;
    void Start ()
    {
        vNextPos = Vector3.zero;
        ResetData();
    }
    private void ResetData()
    {
        nCurPos = 0;
        GetNextPos();
        transform.position = vNextPos;

        GetNextPos();
    }
    public void Init(int number, int pathType)
    {
        id = number;
        nPathType = pathType;
        //设置渲染层级
        GetComponent<SpriteRenderer>().sortingOrder = id;
        ResetData();
        bInitDone = true;
    }
    public int GetID()
    {
        return id;
    }
    void GetNextPos()
    {
        if (!SharedInfo.InitDone()) return;

        UpdatePosInfo();
        vNextPos = new Vector3(curPosInfo.x, curPosInfo.y, 0);
        vNextPos = Camera.main.ScreenToWorldPoint(vNextPos);
        vNextPos.z = transform.position.z;

        Vector3 curDirction = vNextPos - transform.position;
        float angle = Mathf.Atan2(curDirction.y, curDirction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
	// Update is called once per frame
	void Update ()
    {
        if (bInitDone)
        {
            //Vector3 vBefore = transform.position;
            //插值移动
            Vector3 curDirction = vNextPos - transform.position;
            //transform.Translate(curDirction * Time.deltaTime * Speed, Space.World);
            //匀速移动
            transform.Translate(curDirction.normalized*Time.deltaTime * Speed, Space.World);

            Vector3 changeInfo = curDirction;
            if (changeInfo.x < Time.deltaTime * Speed && changeInfo.y < Time.deltaTime * Speed)
            {
                // Debug.Log(transform.position.ToString() + ":" + vNextPos.ToString() + " : " + (transform.position - vBefore).ToString());
                GetNextPos();
            }
        }
   
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "bullet")
        {
            //Debug.Log("get bullet!");
            Camera.main.SendMessage("OnPlayWeb", other.gameObject);
            Destroy(other.gameObject);
            //10概率死亡
            if (Random.Range(0, 100) < 10)
            {
                Camera.main.SendMessage("OnFishDie", this.gameObject);
            }
        }
    }

    bool UpdatePosInfo()
    {
        if (nCurPos >= SharedInfo.GetTargetPathSize(nPathType))
        {
            ResetData();
            return false;
        }

        curPosInfo = SharedInfo.GetTargetPathPosInfo(nPathType, nCurPos);
        ++nCurPos;
        return true;
    }

}

