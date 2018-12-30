using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;//正则表达式
using System.IO;

public class PathPointInfo
{
    public float x;
    public float y;
    public float angle;
    public PathPointInfo()
    {
        x = 0f;
        y = 0f;
        angle = 0f;
    }
    public PathPointInfo(float vx,float vy,float vangle)
    {
        x = vx;
        y = vy;
        angle = vangle;
    }
};

public static class ParseFishPath {

    //Assets/FishPath/demo.dat
    public static PathPointInfo[] Parse(string strFileName)
    {
        //FileStream fsSource = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
        //TextAsset值读取后缀名为txt问题件，Resources.Load输入文件名不包含后缀名
        TextAsset infoAssets = Resources.Load<TextAsset>(strFileName);
        string str = infoAssets.text;

        //69.0,740.0,0,

        //\d{n,}  至少匹配n个数字
        //\.?  匹配或不匹配 .
        string strPattern = @"(\d{1,}\.?\d{0,},)(\d{1,}\.?\d{0,},)(\d{1,}\.?\d{0,},)";
        MatchCollection colls = Regex.Matches(str, strPattern);
        PathPointInfo[] vAllPoints = new PathPointInfo[colls.Count];
        for (int i=0; i<colls.Count; i++)
        {
            string[] strPoints = colls[i].Value.Split(',');
            if (strPoints.Length >= 3)
            {
                vAllPoints[i] = new PathPointInfo(float.Parse(strPoints[0]), float.Parse(strPoints[1]), float.Parse(strPoints[2]));
            }
            //Debug.Log(vAllPoints[i].ToString());
        }

        return vAllPoints;
    }
}
