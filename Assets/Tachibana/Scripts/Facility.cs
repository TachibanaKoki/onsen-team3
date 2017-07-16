using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FacilityType
{
    None,
    PublicBath,
    Aquaculture,
    PowerPlant
}

public class Facility : MonoBehaviour
{

    public RectTransform rectTransform;
    public FacilityType facilityType = FacilityType.None;
    FacilityTypeInterface facilityAction=null;

    Image m_Image;

    void Start()
    {
        m_Image = GetComponent<Image>();
    }

    void Update()
    {
        if (facilityAction == null) return;
        facilityAction.Update();
    }

	// Use this for initialization
	public void GetRectTransform()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    public void SetFaciltyType(FacilityType facType)
    {
        if(facType==FacilityType.PublicBath)
        {
            m_Image.sprite = FacilityManager.I.m_PublicBath;
        }
        else if (facType == FacilityType.PowerPlant)
        {
            m_Image.sprite = FacilityManager.I.m_PowerPlant;
        }
        else if (facType == FacilityType.Aquaculture)
        {
            m_Image.sprite = FacilityManager.I.m_Aquaculture;
        }
        else
        {

        }
        facilityType = facType;
    }

    public void TapArea()
    {

        //todo 施設開拓中
        if (GameSystem.I.NowState == GameSystem.GameState.Installation)
        {
            //すでに施設がある
            if (facilityType != FacilityType.None) return;

            //施設設置
            if (FacilityManager.I.SelectFacilityType == FacilityType.PublicBath)
            {
                if (GameParame.I.Money < GameParame.I.PublicBathCost) return;
                GameParame.I.Money -= GameParame.I.PublicBathCost;
                facilityAction = new PublicBath();

            }
            else if(FacilityManager.I.SelectFacilityType ==FacilityType.PowerPlant)
            {
                if (GameParame.I.Money < GameParame.I.PowerPlantCost) return;
                GameParame.I.Money -= GameParame.I.PowerPlantCost;
            }
            else if(FacilityManager.I.SelectFacilityType ==FacilityType.Aquaculture)
            {
                if (GameParame.I.Money < GameParame.I.AquacultureCost) return;
                GameParame.I.Money -= GameParame.I.AquacultureCost;
            }

            SetFaciltyType(FacilityManager.I.SelectFacilityType);
        }
        //todo 営業中
        else if(GameSystem.I.NowState == GameSystem.GameState.Practice)
        {
            //if(facilityType==FacilityType.PowerPlant)
            //{
            //    facilityAction.Update();            
            //}
        }
    }

}
