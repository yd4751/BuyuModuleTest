using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMgr : MonoBehaviour {

    // Use this for initialization
    public Sprite[] backs;
    private int index = 0;

    void Start () {
        AutoToFit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnChangeBackground()
    {
        ++index;
        if (index >= backs.Length)
        {
            index = 0;
        }
        GetComponent<SpriteRenderer>().sprite = backs[index];

        AutoToFit();
    }
    void AutoToFit()
    {
        Vector3 scale = transform.localScale;
        float cameraheight = Camera.main.orthographicSize * 2;
        float camerawidth = cameraheight * Camera.main.aspect;
        SpriteRenderer render = this.GetComponent<SpriteRenderer>();
        float spriteY = render.sprite.bounds.size.y* scale.x;
        float spriteX = render.sprite.bounds.size.x* scale.y;
        if (cameraheight >= camerawidth)
        {
            scale *= cameraheight / spriteY;
        }
        else
        {
            scale *= camerawidth / spriteX;
        }

        transform.localScale = scale;
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }
}
