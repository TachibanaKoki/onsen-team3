using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FacilityType
{
    None,
    PublicBath,
    Aquaculture,
    PowerPlant,
    Dig,//掘り途中状態
    DigSet,//掘るをセットされた状態
    
}

[System.Serializable]
public struct IntVector2
{
    public IntVector2(int i, int j)
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


    public int setMoney = 0;
    public int setUnagi = 0;

    Image m_Image;

    [SerializeField]
    Slider slider;


    GameObject Effect;

    void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    void Update()
    {
        if (GameSystem.I.NowState == GameSystem.GameState.Practice)
        {
            facilitybase.Update();
        }
        else if (GameSystem.I.NowState == GameSystem.GameState.dig)
        {
            //営業終わって掘るだったらdigに戻す
            if (FacilityType.DigSet == facilityType)
            {
                facilityType = FacilityType.Dig;
                SetFaciltyType(FacilityType.Dig);
            }
        }


   

        if(Effect!=null)
        {
            Effect.transform.localScale = FacilityManager.I.transform.localScale;
        }
    }

    public void Reset()
    {
        slider.gameObject.SetActive(false);
        facilitybase = new FacilityBase();
        SetFaciltyType(FacilityType.None);
        GreadLevel = 1;
        GameObject.Instantiate(EffectDatas.I.BreakeEffect,transform);
        if(Effect!=null)
        {
            Destroy(Effect);
        }
    }
    public void GreadUP()
    {
        GreadLevel++;
    }

    // Use this for initialization
    public void Initialize(int x, int y)
    {
        Index = new IntVector2(x, y);
        rectTransform = GetComponent<RectTransform>();

    }

    public void SetFaciltyType(FacilityType facType,int level = 1)
    {
        m_Image.color = Color.white;
        if (facType == FacilityType.PublicBath||facType==FacilityType.PowerPlant|| facType==FacilityType.Aquaculture)
        {
            m_Image.sprite = FacilityManager.I.GetFacilityImage(facType,level);
        }
        else if (facType == FacilityType.Dig)
        {
            m_Image.sprite = FacilityManager.I.m_Dig;
        }
        else if (facType == FacilityType.DigSet)
        {
            m_Image.sprite = FacilityManager.I.m_SetDigging;
        }
        else
        {
            m_Image.color = new Color(1, 1, 1, 0.5f);
            m_Image.sprite = null;
        }
        facilityType = facType;
    }

    public void TapArea()
    {
        //todo 施設開拓中
        if (GameSystem.I.NowState == GameSystem.GameState.Installation)
        {
            //すでに施設がある
            if (facilityType != FacilityType.None&& facilityType != FacilityType.Dig && facilityType != FacilityType.DigSet)
            {
                FacilityManager.I.digUi.SetActive(false);
                GrreadUpPanel.I.Open(this);
                return;
            }
            if (FacilityManager.I.m_createFacilityState == CreateFacilityState.Create)
            {
                //施設設置
                if (FacilityManager.I.SelectFacilityType == FacilityType.PublicBath)
                {
                    if (!CreatePublicBath()) return;
                }
                else if (FacilityManager.I.SelectFacilityType == FacilityType.PowerPlant)
                {
                    CreatePowerPlant();
                }
                else if (FacilityManager.I.SelectFacilityType == FacilityType.Aquaculture)
                {
                    CreateAquaculture();
                }
            }
             if (FacilityManager.I.m_createFacilityState == CreateFacilityState.Dig)
            {
                if (facilityType==FacilityType.None|| facilityType == FacilityType.Dig)
                {
                    CreateDigging();
                }
                else
                {
                    FacilityManager.I.digUi.SetActive(false);
                }
                
            }
            else
            {
                FacilityManager.I.digUi.SetActive(false);
            }
        }
        //todo 営業中
        else if (GameSystem.I.NowState == GameSystem.GameState.Practice)
        {
            if (facilityType == FacilityType.PowerPlant)
            {
                PowerPlant pp = (PowerPlant)facilitybase;
                if (pp.m_ChageUnagi >= 100)
                {
                    MessageSystem.I.SetMessage("発電所にこれ以上ウナギは必要ないようだ……");
                    return;
                }
                pp.ChageUnagi(10);
            }
        }
    }


    private void SetDigText()
    {
        FacilityManager.I.digUi.transform.Find("MoneyText").gameObject.GetComponent<Text>().text = "お金 " + setMoney;
        FacilityManager.I.digUi.transform.Find("UnagiText").gameObject.GetComponent<Text>().text = "うなぎ " + setUnagi;
    }

    public void MoneyPush()
    {

        var g = this.GetComponentInChildren<DigContents>();


        if (GameParame.I.Money - DigContents.c_costMoney * g.NowDig >= 0 && GameParame.I.Unagi - DigContents.c_costUnagi * g.NowDig >= 0)
        {
            GameParame.I.Money -= DigContents.c_costMoney * g.NowDig;
            GameParame.I.Unagi -= DigContents.c_costUnagi * g.NowDig;
            g.canDig = true;
            SetFaciltyType(FacilityType.DigSet);
            FacilityManager.I.digUi.SetActive(false);
            facilityType = FacilityType.DigSet;
        }
        else
        {

            MessageSystem.I.SetMessage("必要資源が足りない");
        }

    }

    public bool CreatePublicBath()
    {
        if (GameParame.I.Money < GameParame.I.PublicBathCost)
        {
            MessageSystem.I.SetMessage("所持金が足りない");
            return false;
        }

        PowerPlant powerPlant = null;
        bool isok = false;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                IntVector2 index = new IntVector2(i + Index.x, j + Index.y);

                //範囲外なら戻る
                if (index.x < 0 || index.x >= FacilityManager.I.m_facility.Length
                    || index.y < 0 || index.y >= FacilityManager.I.m_facility.Length)
                {
                    continue;
                }

                if (FacilityManager.I.m_facility[index.x][index.y].facilityType == FacilityType.PowerPlant)
                {
                    powerPlant = (PowerPlant)FacilityManager.I.m_facility[index.x][index.y].facilitybase;
                    Debug.Log("CreateBath");
                    isok = true;
                    break;
                }
            }
        }

        if (!isok)
        {
            MessageSystem.I.SetMessage("近くに発電所がないようだ");
            return false;
        }
        Effect = GameObject.Instantiate(EffectDatas.I.onsenSmoke,gameObject.transform);
        GameParame.I.Money -= GameParame.I.PublicBathCost;
        facilitybase = new onsen();
        onsen onsen = (onsen)facilitybase;
        onsen.powerPlant = powerPlant;
        onsen.Start();
        SetFaciltyType(FacilityType.PublicBath);
        return true;

    }
    //うなぎ、金セットできるように、完了も表示
    void CreateDigging()
    {
        var g = this.GetComponentInChildren<DigContents>();

        Debug.Log("DegCount"+g.NowDig);
        setMoney = g.NowDig * DigContents.c_costMoney;
        setUnagi = g.NowDig * DigContents.c_costUnagi;

        FacilityManager.I.SelectFacility = this;
        FacilityManager.I.digUi.SetActive(true);
        SetDigText();

    }
    public void CreateAquaculture()
    {
        if (GameParame.I.Money < GameParame.I.AquacultureCost)
        {
            MessageSystem.I.SetMessage("所持金が足りない");
            return;
        }
        GameParame.I.Money -= GameParame.I.AquacultureCost;
        SetFaciltyType(FacilityType.Aquaculture);
        facilitybase = new Farms();
        facilitybase.Start();
    }

    public void CreatePowerPlant()
    {
        if (GameParame.I.Money < GameParame.I.PowerPlantCost)
        {
            MessageSystem.I.SetMessage("所持金が足りない");
            return;
        }
        Effect = GameObject.Instantiate(EffectDatas.I.elect, gameObject.transform);
        GameParame.I.Money -= GameParame.I.PowerPlantCost;
        SetFaciltyType(FacilityType.PowerPlant);
        slider.gameObject.SetActive(true);
        facilitybase = new PowerPlant();
        PowerPlant power = (PowerPlant)facilitybase;
        power.m_facility = slider;
        power.Start();
    }
}
