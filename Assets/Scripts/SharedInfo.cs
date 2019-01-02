using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SharedInfo{
    //
    public static bool bInitDone = false;
    public static List<PathPointInfo[]> allPathList;
    public static void init()
    {
        Debug.Log("Init shared info!");
        allPathList = new List<PathPointInfo[]>();

        int nMaxPathFile = 5;
        for (int i=0; i< nMaxPathFile; i++)
        {
            PathPointInfo[] cur = ParseFishPath.Parse("path" + i);
            if (cur != null)
            {
                Debug.Log("GetPathLen:" + cur.Length);
                allPathList.Add(cur);
            }
        }

        bInitDone = true;
    }
    public static bool InitDone()
    {
        return bInitDone;
    }
    public static int GetPathSize()
    {
        return allPathList.Count;
    }
    public static int GetTargetPathSize(int nIndex)
    {
        if (allPathList.Count == 0) return 0;
        return allPathList[nIndex].Length;
    }
    public static PathPointInfo GetTargetPathPosInfo(int nIndex,int nPos)
    {
        if (allPathList.Count < nIndex - 1) Debug.Log("Error");
        return allPathList[nIndex][nPos];
    }
}
