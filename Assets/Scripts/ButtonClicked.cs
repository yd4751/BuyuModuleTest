using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonClicked : MonoBehaviour {

    // Use this for initialization
    public Sprite clickBack;
    private Sprite defaultBack;

	void Start () {
        defaultBack = GetComponent<Image>().sprite;
    }
	
	// Update is called once per frame
	void Update () {
        if(EventSystem.current.currentSelectedGameObject == gameObject)
        {
            GetComponent<Image>().sprite = clickBack;
        }
        else
            GetComponent<Image>().sprite = defaultBack;
    }
}
