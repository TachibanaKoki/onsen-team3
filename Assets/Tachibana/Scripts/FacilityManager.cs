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
    int MapSize = 5;

    [SerializeField]
    GameObject m_facilityPrefab;

 

    public Sprite m_PublicBath;
   
    public Sprite m_Aquaculture;

    public Sprite m_PowerPlant;




    public FacilityType SelectFacilityType;

    public Facility[][] m_facility;


    public Facility SelectFacility;

    [SerializeField]
    GameObject m_construction;

    [SerializeField]
    public GameObject digUi;

    [SerializeField]
    Text m_messageBox;




    public void SetMessage(string text)
    {
        m_messageBox.text = text;
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

        m_createFacilityState = CreateFacilityState.Create;
        m_construction.SetActive(true);
        digUi.SetActive(false);
    }

    public void SelectDig()
    {
        SelectFacilityType = FacilityType.Digging;
        m_construction.SetActive(false);
        m_createFacilityState = CreateFacilityState.Create;

    }





    void Start ()
    {
        m_facility = new Facility[MapSize][];
        for(int i=0;i<  m_facility.Length; i++)
        {
            m_facility[i] = new Facility[MapSize];
        }
        CreateMap();
        I = this;


        GameObject digOb = GameObject.Find("DigContentsSystem");
        digOb.GetComponent<DigContentsSystem>().Initialize(MapSize);

    }

    void CreateMap()
    {
        Vector2 vec = new Vector2(3,30);
        vec.Normalize();
        float dis = 170;

        Vector2 offset = new Vector2(-100,-100);

        for(int i=0;i<m_facility.Length;i++)
        {
            for(int j= 0;j< m_facility[i].Length;j++)
            {
                m_facility[i][j] = GameObject.Instantiate(m_facilityPrefab).GetComponent<Facility>();
                m_facility[i][j].transform.SetParent(transform);
                m_facility[i][j].Initialize(i,j);
                Vector2 v = vec*i*dis;
                m_facility[i][j].rectTransform.anchoredPosition = offset+ v +new Vector2(vec.y,0- vec.x) * j*dis;
            }
        }
    }


    public void MoneyUpPush()
    {
        SelectFacility.GetComponent<Facility>().MoneyPush();
    }

}
