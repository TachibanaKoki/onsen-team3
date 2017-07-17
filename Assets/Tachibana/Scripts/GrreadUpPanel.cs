using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_ANDROID
using UnityEngine.Advertisements;
#endif

[System.Serializable]
public class GreadUpContent
{
    public Image m_facilityImage;

    public Text m_GreadLevelText;

    public Text moneyCost;

    public Text unagiCost;
}


public class GrreadUpPanel : MonoBehaviour
{
    public static GrreadUpPanel I;

    [SerializeField]
    GreadUpContent greadUpContent;

    [SerializeField]
    GreadUpContent androidGreadUpContent;

    [SerializeField]
    GameObject m_androidContent;

    [SerializeField]
    GameObject m_Content;

    [SerializeField]
    Image Panel;

    Facility targetFacility;

    void Start()
    {
        I = this;
        ContentActive(false);
    }

    public void ContentActive(bool flag)
    {
#if UNITY_ANDROID 
        m_androidContent.SetActive(flag);
#else
        m_Content.SetActive(flag);
#endif
    }

    public void Open(Facility facility)
    {
        targetFacility = facility;
        Debug.Log("GradUpOpen");
        ContentActive(true);
#if UNITY_ANDROID
        androidGreadUpContent.m_GreadLevelText.text = facility.GreadLevel.ToString();
        androidGreadUpContent.moneyCost.text = GameParame.I.GreadUpMoney.ToString();
        androidGreadUpContent.unagiCost.text = GameParame.I.GreadUpUnagi.ToString();
        androidGreadUpContent.m_facilityImage.sprite = FacilityManager.I.GetFacilityImage(targetFacility.facilityType, targetFacility.GreadLevel);
#else
        greadUpContent.m_GreadLevelText.text = facility.GreadLevel.ToString();
        greadUpContent.moneyCost.text = GameParame.I.GreadUpMoney.ToString();
        greadUpContent.unagiCost.text = GameParame.I.GreadUpUnagi.ToString();
        greadUpContent.m_facilityImage.sprite = FacilityManager.I.GetFacilityImage(targetFacility.facilityType,targetFacility.GreadLevel);
#endif
    }

    public void Close()
    {
        ContentActive(false);
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
#if UNITY_ANDROID
            androidGreadUpContent.m_GreadLevelText.text = targetFacility.GreadLevel.ToString();
            androidGreadUpContent.moneyCost.text = GameParame.I.GreadUpMoney.ToString();
            androidGreadUpContent.unagiCost.text = GameParame.I.GreadUpUnagi.ToString();
            androidGreadUpContent.m_facilityImage.sprite = FacilityManager.I.GetFacilityImage(targetFacility.facilityType, targetFacility.GreadLevel);
#else
            greadUpContent.m_GreadLevelText.text = targetFacility.GreadLevel.ToString();
            greadUpContent.moneyCost.text = GameParame.I.GreadUpMoney.ToString();
            greadUpContent.unagiCost.text = GameParame.I.GreadUpUnagi.ToString();
            greadUpContent.m_facilityImage.sprite = FacilityManager.I.GetFacilityImage(targetFacility.facilityType,targetFacility.GreadLevel);
#endif
        }
    }

    public void ShowVideo()
    {
#if UNITY_ANDROID
        Panel.raycastTarget = true;
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var option = new ShowOptions { resultCallback = ShowResultAd };
            Advertisement.Show("rewardedVideo",option);
        }

#endif
    }
#if UNITY_ANDROID
    public void ShowResultAd(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GreadUp();
                Panel.raycastTarget = false;
                break;
            case ShowResult.Skipped:
                Panel.raycastTarget = false;
                break;
            case ShowResult.Failed:
                Panel.raycastTarget = false;
                GreadUp();
                break;
                
        }
        Panel.raycastTarget = false;
    }
#endif
}
