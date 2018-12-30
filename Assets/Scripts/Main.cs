using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public GameObject prefabFish_SharkBlue;
    public GameObject preInitPosition;

    private Quaternion quaInitRoatate = new Quaternion(0, 0, 0, 0);//
    void Awake()
    {
        Init();
    }
    void Start () {

        Debug.Log("=============!!");
        for (int i = 0; i < 1; i++)
        {
            Vector3 vInitPosition = new Vector3(i, 0, preInitPosition.transform.position.z);
            GameObject fish = Instantiate(prefabFish_SharkBlue, vInitPosition, this.transform.rotation);
        }

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void Init()
    {
        SharedInfo.init();
    }
}
