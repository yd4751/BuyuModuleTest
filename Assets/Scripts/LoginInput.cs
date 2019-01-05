using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginInput : MonoBehaviour {

    public GameObject inputAccount;
    public GameObject inputPassword;
    public GameObject notifyInfo;
    public string strNextScene;
    // Use this for initialization
    private string account;
    private string password;
    private float delayNotifyTime = 3;
    private float nStartTime = 0;
    void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
    }
	
	// Update is called once per frame
	void Update () {
		if(nStartTime > 0)
        {
            nStartTime -= Time.deltaTime;
            if(nStartTime <=0)
            {
                notifyInfo.GetComponent<Text>().text = "";
            }
        }
	}

    public void OnAccountEndInput()
    {
        account = inputAccount.GetComponent<InputField>().text;
        // Debug.Log(inputAccount.GetComponent<InputField>().text);
    }

    public void OnPasswordEndInput()
    {
        password = inputAccount.GetComponent<InputField>().text;
        //Debug.Log(inputPassword.GetComponent<InputField>().text);
    }
    public void OnLogin()
    {
        if(account == null)
        {
            notifyInfo.GetComponent<Text>().text = "无效账户";
            nStartTime = delayNotifyTime;
            return;
        }
        if (password == null)
        {
            notifyInfo.GetComponent<Text>().text = "无效密码";
            nStartTime = delayNotifyTime;
            return;
        }

        notifyInfo.GetComponent<Text>().text = "即将进入游戏，请等待...";
        nStartTime = delayNotifyTime;

        SceneManager.LoadSceneAsync(strNextScene);
    }
}
