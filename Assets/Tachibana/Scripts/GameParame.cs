using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParame : MonoBehaviour
{
    public static GameParame I;

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




	void Start ()
    {
		if(I!=null )
        {
            Destroy(I);
            I = null;
        }
        I = this;

        if(PlayerPrefs.HasKey("Money"))
        {
            Money = PlayerPrefs.GetInt("Money");
            Unagi = PlayerPrefs.GetInt("Unagi");
        }
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
