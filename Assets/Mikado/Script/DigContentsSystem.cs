using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigContentsSystem : MonoBehaviour {

    [SerializeField]
    GameObject facilityManager;
    [SerializeField]
    GameObject megamiPrefab;
    const int c_money = 30;
    const int c_unagi = 30;
    int moneyCount = 0;
    int unagiCount = 0;

    // Use this for initialization
    void Start () {

    }

    public void Initialize(int MapSiz)
    {
        int x = 0, y=0;
        moneyCount = (int)((MapSiz * MapSiz) * (c_money / 100.0)) * DigContents.c_maxDig;
        unagiCount = (int)((MapSiz * MapSiz) * (c_unagi / 100.0)) * DigContents.c_maxDig;
        for (int i = 0; i < unagiCount; i++)//ウナギ格納
        {
            x = Random.Range(0, MapSiz);
            y = Random.Range(0, MapSiz);

            FacilityManager.I.m_facility[x][y].GetComponentInChildren<DigContents>().SetDigState(DigContents.DigItem.Unagi);
        }

        //金格納
        for (int i = 0; i < moneyCount; i++)
        {
            x = Random.Range(0, MapSiz);
            y = Random.Range(0, MapSiz);
            FacilityManager.I.m_facility[x][y].GetComponentInChildren<DigContents>().SetDigState(DigContents.DigItem.Money);

        }
        Debug.Log("金入れた" + unagiCount);
        //女神格納（各種）
        for (int i = 0; i <=(int)MegamiStateEnum.RightFut; i++)//最後の！
        {
            x = Random.Range(0, MapSiz);
            y = Random.Range(0, MapSiz);
   
            // プレハブからインスタンスを生成
            GameObject megami = Instantiate(megamiPrefab) as GameObject;

            megami.transform.SetParent(FacilityManager.I.m_facility[x][y].transform);
            megami.GetComponent<MegamiState>().SetMegami((MegamiStateEnum)i); 
        }

    }

}
