using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetFacilityImage : MonoBehaviour
{
    [SerializeField]
    Image m_targetImage;

    [SerializeField]
    Text m_costText;

	// Use this for initialization
	void Start () {
        m_targetImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(FacilityManager.I.SelectFacilityType == FacilityType.PowerPlant)
        {
            m_targetImage.sprite = FacilityManager.I.m_PowerPlant;
            m_costText.text = "コスト："+GameParame.I.PowerPlantCost.ToString();
        }
        else if(FacilityManager.I.SelectFacilityType == FacilityType.PublicBath)
        {
            m_targetImage.sprite = FacilityManager.I.m_PublicBath;
            m_costText.text = "コスト：" + GameParame.I.PublicBathCost.ToString();
        }
        else if(FacilityManager.I.SelectFacilityType == FacilityType.Aquaculture)
        {
            m_targetImage.sprite = FacilityManager.I.m_Aquaculture;
            m_costText.text = "コスト：" + GameParame.I.AquacultureCost.ToString();
        }
	}
}
