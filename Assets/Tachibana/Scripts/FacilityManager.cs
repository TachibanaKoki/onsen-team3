using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CreateFacilityState
{
    None,
    Create,
    GreadeUp,
    Dig
}

public class FacilityManager : MonoBehaviour
{
    public static FacilityManager I;

    public CreateFacilityState m_createFacilityState = CreateFacilityState.None;

    [SerializeField]
    int MapSizeY = 5;

    [SerializeField]
    int MapSizeX = 5;

    [SerializeField]
    GameObject m_facilityPrefab;

 

    public Sprite m_PublicBath;
   
    public Sprite m_Aquaculture;

    public Sprite m_PowerPlant;

    public Sprite m_SetDigging;//セットし掘る方

    public Sprite m_Dig;//掘ってる途中

    public FacilityType SelectFacilityType;

    public Facility[][] m_facility;


    public Facility SelectFacility;

    [SerializeField]
    GameObject m_construction;

    [SerializeField]
    public GameObject digUi;

    [SerializeField]
    Text m_messageBox;

    [SerializeField]
     GameObject digTrun;


    public void SetMessage(string text)
    {
        m_messageBox.text = text;
    }

    public Sprite GetFacilityImage(FacilityType state)
    {
        if (state == FacilityType.PublicBath)
        {
           return FacilityManager.I.m_PublicBath;
        }
        else if (state == FacilityType.PowerPlant)
        {
            return FacilityManager.I.m_PowerPlant;
        }
        else if (state == FacilityType.Aquaculture)
        {
            return FacilityManager.I.m_Aquaculture;
        }
        else if(state==FacilityType.Dig)
        {
            return FacilityManager.I.m_SetDigging;
        }
        else
        {

        }
        return null;
    }
    
    public void SetPublicBath()
    {
        SelectFacilityType = FacilityType.PublicBath;
    }

    public void SetAquaculture()
    {
        SelectFacilityType = FacilityType.Aquaculture;
    }

    public void SetPowerPlant()
    {
        SelectFacilityType = FacilityType.PowerPlant;
    }

    public void SetCreateFacilityState(CreateFacilityState state)
    {
        m_createFacilityState = state;
        if(state!=CreateFacilityState.Create)
        {
            m_construction.SetActive(false);
        }
    }

    public void CreateFacility()
    {
        if (GameSystem.I.NowState != GameSystem.GameState.Installation) return;

        m_createFacilityState = CreateFacilityState.Create;
        m_construction.SetActive(true);
        digUi.SetActive(false);
        SetDigImageNotActiv();
    }

    public void SelectDig()
    {
  
        if (GameSystem.I.NowState != GameSystem.GameState.Installation) return;
        m_createFacilityState = CreateFacilityState.Dig;
        m_construction.SetActive(false);


        digTrun.SetActive(true);
        MessageSystem.I.SetMessage("掘りたい場所を選んでください", 2f);



    }

    void Awake()
    {
        I = this;
    }

    void Start ()
    {
        m_facility = new Facility[MapSizeY][];
        for(int i=0;i<  m_facility.Length; i++)
        {
            m_facility[i] = new Facility[MapSizeX];
        }

        CreateMap();
        GameObject digOb = GameObject.Find("DigContentsSystem");
        digOb.GetComponent<DigContentsSystem>().Initialize(MapSizeY);

    }

    void CreateMap()
    {
        Vector2 vec = new Vector2(0,1);
        vec.Normalize();
        float dis = 170;

        Vector2 offset = new Vector2(-100,-100);

        for(int i=0;i<m_facility.Length;i++)
        {
            for(int j= 0;j< m_facility[i].Length;j++)
            {
                m_facility[i][j] = GameObject.Instantiate(m_facilityPrefab).GetComponent<Facility>();
                m_facility[i][j].transform.SetParent(transform);
                m_facility[i][j].transform.localScale = Vector3.one;
                m_facility[i][j].Initialize(i,j);
                Vector2 v = vec*i*dis;
                m_facility[i][j].rectTransform.anchoredPosition = offset+ v +new Vector2(vec.y,0- vec.x) * j*dis;
            }
        }

        m_facility[1][1].CreatePowerPlant();
        m_facility[1][2].CreatePublicBath();
        m_facility[2][1].CreateAquaculture();
    }

    
    public void PushDigOkButton()
    {
        SelectFacility.GetComponent<Facility>().MoneyPush();
    }
    public void PushDigNOButton()
    {
        digUi.SetActive(false);
    }

    public void SetDigImageNotActiv()
    {
        digTrun.SetActive(false);
    }
}
