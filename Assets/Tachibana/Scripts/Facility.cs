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

[System.Serializable]
public struct IntVector2
{
    public IntVector2(int i,int j)
    {
        x = i;
        y = j;
    }


    public int x;
    public int y;
}

public class Facility : MonoBehaviour
{
    public IntVector2 Index;
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
	public void Initialize(int x,int y)
    {
        Index = new IntVector2(x,y);
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
        if (GameSystem.I.NowState == GameSystem.GameState.Installation
            &&FacilityManager.I.m_createFacilityState==CreateFacilityState.Create)
        {
            //すでに施設がある
            if (facilityType != FacilityType.None) return;

            //施設設置
            if (FacilityManager.I.SelectFacilityType == FacilityType.PublicBath)
            {
                if (!CreatePublicBath()) return;
            }
            else if(FacilityManager.I.SelectFacilityType ==FacilityType.PowerPlant)
            {
                CreatePowerPlant();
            }
            else if(FacilityManager.I.SelectFacilityType ==FacilityType.Aquaculture)
            {
                CreateAquaculture();
            }


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

    bool CreatePublicBath()
    {
        if (GameParame.I.Money < GameParame.I.PublicBathCost) return false;

        bool isok = false;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                
                IntVector2 index = new IntVector2(i + (int)Index.x, j + (int)Index.y);
               
                //範囲外なら戻る
                if(index.x<0|| index.x>=FacilityManager.I.m_facility.Length
                    ||index.y<0||index.y>=FacilityManager.I.m_facility.Length)
                {
                    continue;
                }

                if (FacilityManager.I.m_facility[(int)index.x][(int)index.y].facilityType == FacilityType.PowerPlant)
                {
                    Debug.Log("CreateBath");
                    isok = true;
                    break;
                }
            }
        }

        if (!isok)
        {
            return false;
        }

        GameParame.I.Money -= GameParame.I.PublicBathCost;
        facilityAction = new PublicBath();
        SetFaciltyType(FacilityManager.I.SelectFacilityType);
        return true;

    }

    void CreateAquaculture()
    {
        if (GameParame.I.Money < GameParame.I.AquacultureCost) return;
        GameParame.I.Money -= GameParame.I.AquacultureCost;
        SetFaciltyType(FacilityManager.I.SelectFacilityType);
    }

    void CreatePowerPlant()
    {
        if (GameParame.I.Money < GameParame.I.PowerPlantCost) return;

        GameParame.I.Money -= GameParame.I.PowerPlantCost;
        SetFaciltyType(FacilityManager.I.SelectFacilityType);
    }

}
