using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SharedInfo{
    //
    public static PathPointInfo[] defaultPathInfo;
    public static void init()
    {
        Debug.Log("Init shared info!");
        defaultPathInfo = ParseFishPath.Parse("demo");
        Debug.Log("Parse DefaultPathSize:" + defaultPathInfo.Length);
    }
}
