using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameParame : MonoBehaviour
{
    public static GameParame I;

    public int PublicBathCost = 100;

    public int AquacultureCost = 10;

    public int PowerPlantCost = 100;

    private int m_money;
    public int Money
    {
        get { return m_money; }
        set { m_money = Mathf.Max(0, value); }
    }


    private int m_unagi;
    public int Unagi
    {
        get { return m_unagi; }
        set { m_unagi = value; }
    }

    [SerializeField]
    Text MoneyInHand;

    [SerializeField]
    Text UnagiInHand;

    public int GreadUpMoney=300;

    public int GreadUpUnagi=50;

    void Awake()
    {
        if (I != null)
        {
            Destroy(I);
            I = null;
        }
        I = this;

        if (PlayerPrefs.HasKey("Money"))
        {
            Money = PlayerPrefs.GetInt("Money");
            Unagi = PlayerPrefs.GetInt("Unagi");
        }
        else
        {
            //Money = 1000+AquacultureCost+PowerPlantCost+PublicBathCost;
            //Unagi = 100;
            Money = 2000000000;
            Unagi = 1000000;
        }
    }

    void Update()
    {
        MoneyInHand.text = m_money.ToString();
        UnagiInHand.text = m_unagi.ToString();
    }

    void OnDestory()
    {
        I = null;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Money",m_money);
        PlayerPrefs.SetInt("Unagi",m_unagi);
        PlayerPrefs.Save();
    }
}
