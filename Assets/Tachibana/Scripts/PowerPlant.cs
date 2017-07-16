using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPlant : FacilityBase
{
    public Slider m_facility = null;
    public int m_ChageUnagi = 100;
	// Use this for initialization
	public override void Start ()
    {
		
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        m_facility.value = m_ChageUnagi;
	}

    public void ChageUnagi(int value)
    {
        m_ChageUnagi += value;
    }
}
