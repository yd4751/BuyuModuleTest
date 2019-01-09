using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private float nGenerateTimeSep = 0.5f;
    private float nLastGenerateTime = 0f;
    private List<GameObject> allPrefabFishs;
    private GameObject web;
    private GameObject gold;
    private Dictionary<int,GameObject> allFishs;
    private int nCurFishID = 0;
    private int nMaxFishCount = 30;
    void Awake()
    {
        allPrefabFishs = new List<GameObject>();
        allFishs = new Dictionary<int, GameObject>();
        Init();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
 
	// Update is called once per frame
	void Update () {
        if (!SharedInfo.InitDone()) return;
        
        if (nLastGenerateTime < nGenerateTimeSep)
        {
            if (nMaxFishCount > allFishs.Count)
            {
                nLastGenerateTime += Time.deltaTime;
            }
        }
        else
        {
            GenerateFish();
            nLastGenerateTime = 0;
        }
	}
    void Init()
    {
        LoadTypeFishs();
        SharedInfo.init();
    }
    void LoadTypeFishs()
    {
        allPrefabFishs.Clear();
        //枪鱼
        allPrefabFishs.Add(Resources.Load("Prefabs/BulletFish") as GameObject);
        //魔鬼鱼
        allPrefabFishs.Add(Resources.Load("Prefabs/DevilFish") as GameObject);
        //金鲨
        allPrefabFishs.Add(Resources.Load("Prefabs/GoldShark") as GameObject);
        //神仙鱼
        allPrefabFishs.Add(Resources.Load("Prefabs/ImmortalFish") as GameObject);
        //银鲨
        allPrefabFishs.Add(Resources.Load("Prefabs/SilverFish") as GameObject);
        //乌龟
        allPrefabFishs.Add(Resources.Load("Prefabs/Turtle") as GameObject);
        //鱼网
        web = Resources.Load("Prefabs/web") as GameObject;
        //金币
        gold = Resources.Load("Prefabs/gold") as GameObject;

    }
    void GenerateFish()
    {
        //随机鱼类型  随机鱼路径
        GameObject fish = GameObject.Instantiate<GameObject>(allPrefabFishs[Random.Range(0, allPrefabFishs.Count)]);
        fish.GetComponent<FishMove>().Init(nCurFishID, Random.Range(0,SharedInfo.GetPathSize()));
   
        allFishs.Add(nCurFishID, fish);
        nCurFishID += 1;
    }
    void OnFishDie(GameObject fish)
    {
        int fishID = fish.GetComponent<FishMove>().GetID();
        //Debug.Log("fish die! " + fishID);
        
        int nCout = Random.Range(1, 5);
        for(int i=0;i<nCout; i++)
            GameObject.Instantiate<GameObject>(gold, allFishs[fishID].transform.position, Quaternion.identity, this.transform);

        //CNetWork.NetWork.SendFishDie();
        Destroy(allFishs[fishID]);
        allFishs.Remove(fishID);
        
    }
    void OnPlayWeb(GameObject bullet)
    {
        GameObject.Instantiate<GameObject>(web, bullet.transform.position, Quaternion.identity, this.transform);
    }
}

