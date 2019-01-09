using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cannon : MonoBehaviour {

    public GameObject prefbBullet;
    public float nFireForce;
    public float nSetGenerateTime = 0.4f;

    //
    private Transform objCannon;
    private Transform objBullet;
    private bool bStartGenerateBullet = false;
    private float nLastGenerateTime = 0f;


    void Start () {
        Transform[] childs = GetComponentsInChildren<Transform>();
        foreach(var child in childs)
        {
            if(child.name == "cover")
            {
                objCannon = child;
                break;
            }
        }

        Transform[] cannonChilds = GetComponentsInChildren<Transform>();
        foreach(var child in cannonChilds)
        {
            if (child.name == "bullet")
            {
                objBullet = child;
                break;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                bStartGenerateBullet = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                StopGenerateBullet();
            }
        }
        else
        {
            StopGenerateBullet();
        }
        StartGenerateBullet();
        FollowMouse();
    }

    void FollowMouse()
    {
        Vector3 direct = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direct.z = objCannon.position.z;
        Vector2 direction = direct - objCannon.position;
        float nMouseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (nMouseAngle > 0 && nMouseAngle < 180)
        {
            float angle = nMouseAngle - 90f;
            objCannon.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void StartGenerateBullet()
    {
        if (!bStartGenerateBullet) return ;

        if(nLastGenerateTime <= nSetGenerateTime)
        {
            nLastGenerateTime += Time.deltaTime;
            return;
        }

        GameObject.Instantiate(prefbBullet, objBullet.position, objBullet.rotation);
        CNetWork.NetWork.SendFire();
        nLastGenerateTime = 0;
    }
    
    void StopGenerateBullet()
    {
        bStartGenerateBullet = false;
    }
}
