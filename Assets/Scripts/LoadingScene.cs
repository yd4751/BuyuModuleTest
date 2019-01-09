using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{

    public Slider processBar;
    public string nextSceneName;
    private AsyncOperation async;
    private uint _nowprocess;
    // Use this for initialization
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        _nowprocess = 0;
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        //异步读取场景。
        //Globe.loadName 就是A场景中需要读取的C场景名称。
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;
        //读取完毕后返回， 系统会自动进入C场景
        yield return async;

    }

    void Update()
    {
        if (async == null)
        {
            return;
        }

        uint toProcess;
        //Debug.Log(async.progress * 100);
        if (async.progress < 0.9f)//坑爹的progress，最多到0.9f
        {
            toProcess = (uint)(async.progress * 100);
        }
        else
        {
            toProcess = 100;
        }

        if (_nowprocess < toProcess)
        {
            _nowprocess++;
        }

        processBar.value = _nowprocess / 100f;

        if (_nowprocess == 100)//async.isDone应该是在场景被激活时才为true
        {
            async.allowSceneActivation = true;
        }
    }

}