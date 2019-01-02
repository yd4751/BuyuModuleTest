using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoToFitScreen : MonoBehaviour {

    // Use this for initialization
    
    void Awake()
    {

    }
    void Start () {

        Vector3 scale = transform.localScale;
        float cameraheight = Camera.main.orthographicSize * 2;
        float camerawidth = cameraheight * Camera.main.aspect;
        SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        if (cameraheight >= camerawidth)
        {
            scale *= cameraheight / render.size.y;
        }
        else
        {
            scale *= camerawidth / render.size.x;
        }
        
        transform.localScale = scale;
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
