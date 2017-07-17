using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class onsen : FacilityBase
{
    public int addvalue = 1;

    public PowerPlant powerPlant;

    private float m_addMoneyCount;
    private int m_currntMoneyNum;
    private const float m_addMoneyTime = 0.5f;
    // Use this for initialization
    public override void Start (Facility fac)
    {
        m_addMoneyCount = 0;
        m_currntMoneyNum = 0;
        base.Start(fac);
            
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        m_addMoneyCount += Time.deltaTime;
        if (m_addMoneyCount > m_addMoneyTime)
        {
            if (powerPlant.m_ChageUnagi <= 0) return;
            powerPlant.RemoveUnagi(3);
            m_currntMoneyNum += addvalue*facility.GreadLevel;
            GameParame.I.Money += m_currntMoneyNum;
            SoundManager.m_instance.CoinGetSound();
            m_addMoneyCount = 0;
        }
    }
}
