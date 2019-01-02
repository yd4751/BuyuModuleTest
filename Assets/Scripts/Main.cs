using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private float nGenerateTimeSep = 0.5f;
    private float nLastGenerateTime = 0f;
    private List<GameObject> allPrefabFishs;
    private Dictionary<int,GameObject> allFishs;
    private int nCurFishID = 0;
    private int nMaxFishCount = 30;
    void Awake()
    {
        allPrefabFishs = new List<GameObject>();
        allFishs = new Dictionary<int, GameObject>();
        Init();
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
        Debug.Log("fish die! " + fishID);
        Destroy(allFishs[fishID]);
        allFishs.Remove(fishID);
    }
}
