using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrreadUpPanel : MonoBehaviour
{
    public static GrreadUpPanel I;

    [SerializeField]
    Image m_facilityImage;

    [SerializeField]
    Text m_GreadLevelText;

    [SerializeField]
    Text moneyCost;

    [SerializeField]
    Text unagiCost;

    [SerializeField]
    GameObject m_Content;



    Facility targetFacility;

    void Start()
    {
        I = this;
        m_Content.SetActive(false);
    }

    public void Open(Facility facility)
    {
        targetFacility = facility;
        Debug.Log("GradUpOpen");
        m_Content.SetActive(true);
        m_GreadLevelText.text = facility.GreadLevel.ToString();
        moneyCost.text = GameParame.I.GreadUpMoney.ToString();
        unagiCost.text = GameParame.I.GreadUpUnagi.ToString();
        m_facilityImage.sprite = FacilityManager.I.GetFacilityImage(targetFacility.facilityType);
    }

	public void Close()
    {
        m_Content.SetActive(false);
    }

    public void Break()
    {
        targetFacility.Reset();
        Close();
    }

    public void GreadUp()
    {
        if (GameParame.I.Money >= GameParame.I.GreadUpMoney && GameParame.I.Unagi >= GameParame.I.GreadUpUnagi)
        {
            GameParame.I.Money -= GameParame.I.GreadUpMoney;
            GameParame.I.Unagi -= GameParame.I.GreadUpUnagi;
            targetFacility.GreadUP();
            m_GreadLevelText.text = targetFacility.GreadLevel.ToString();
            moneyCost.text = GameParame.I.GreadUpMoney.ToString();
            unagiCost.text = GameParame.I.GreadUpUnagi.ToString();


        }
    }


}
