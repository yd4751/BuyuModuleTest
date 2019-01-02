using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float nSpeed = 5;
    // Use this for initialization
    private bool bMoveStatus = true;
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    void Move()
    {
        if (!bMoveStatus) return;

        transform.Translate(Vector3.up * Time.deltaTime * nSpeed);
    }

    public void StopMove()
    {
        bMoveStatus = false;
    }
    public void StartMove()
    {
        bMoveStatus = true;
    }
    public void ChangeDirection(float angle)
    {
        transform.Rotate(0f, 0f, angle);
    }
}
