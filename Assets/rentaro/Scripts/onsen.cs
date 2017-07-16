using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 営業時間中に温泉がたっている間は所持金が増える
// お湯の温度調節
// 周りに発電所があれば建てられる
// 発電所をタップ用関数を作成
// 発電所以外はタップイベントはない
// 一定時間ごとに所持金が入る。所持金は発電所のタップイベントの
// 養殖所言って時間ごとにうなぎのパラメータが増えていく
// うなぎが任意のタイミングで一気に手に入る関数を作成

public class onsen : FacilityBase
{
    public int addvalue = 1;
    private float m_addMoneyCount;
    private int m_currntMoneyNum;
    private const float m_addMoneyTime = 0.2f;
    // Use this for initialization
    public override void Start ()
    {
        m_addMoneyCount = 0;
        m_currntMoneyNum = 0;
            
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        m_addMoneyCount += Time.deltaTime;
        if (m_addMoneyCount > m_addMoneyTime)
        {
            m_currntMoneyNum += addvalue;
            GameParame.I.Money += m_currntMoneyNum;
            m_addMoneyCount = 0;
        }
    }
}
