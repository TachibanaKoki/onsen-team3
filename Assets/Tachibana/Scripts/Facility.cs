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
    public int GreadLevel = 1;
    public IntVector2 Index;
    public RectTransform rectTransform;
    public FacilityType facilityType = FacilityType.None;
    FacilityBase facilitybase = new FacilityBase();

    Image m_Image;

    [SerializeField]
    Slider slider;


    

    void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    void Update()
    {
        if(GameSystem.I.NowState == GameSystem.GameState.Practice)
        {
            facilitybase.Update();
        }
    }

    public void Reset()
    {
        slider.gameObject.SetActive(false);
        facilitybase = new FacilityBase();
        SetFaciltyType(FacilityType.None);
        GreadLevel = 1;
    }
    public void GreadUP()
    {
        GreadLevel++;
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
            m_Image.sprite = null;
        }
        facilityType = facType;
    }

    public void TapArea()
    {

        //todo 施設開拓中
        if (GameSystem.I.NowState == GameSystem.GameState.Installation)
        {
            if (FacilityManager.I.m_createFacilityState == CreateFacilityState.Create) return;
            //すでに施設がある
            if (facilityType != FacilityType.None)
            {
                GrreadUpPanel.I.Open(this);
                return;
            }

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
            //if (facilityType == FacilityType.PowerPlant)
            //{
            //    facilityAction.Update();
            //}
        }
    }

    public bool CreatePublicBath()
    {
        if (GameParame.I.Money < GameParame.I.PublicBathCost) return false;

        PowerPlant powerPlant=null;
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
                    powerPlant = (PowerPlant)FacilityManager.I.m_facility[(int)index.x][(int)index.y].facilitybase;
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
        facilitybase = new onsen();
        onsen onsen = (onsen)facilitybase;
        onsen.powerPlant = powerPlant;
        onsen.Start();
        SetFaciltyType(FacilityType.PublicBath);
        return true;

    }

    public void CreateAquaculture()
    {
        if (GameParame.I.Money < GameParame.I.AquacultureCost) return;
        GameParame.I.Money -= GameParame.I.AquacultureCost;
        SetFaciltyType(FacilityType.Aquaculture);
        facilitybase = new Farms();
        facilitybase.Start();
    }

   public void CreatePowerPlant()
    {
        if (GameParame.I.Money < GameParame.I.PowerPlantCost) return;
        GameParame.I.Money -= GameParame.I.PowerPlantCost;
        SetFaciltyType(FacilityType.PowerPlant);
        slider.gameObject.SetActive(true);
        facilitybase = new PowerPlant();
        PowerPlant power = (PowerPlant)facilitybase;
        power.m_facility = slider;
        power.Start();
    }

}
