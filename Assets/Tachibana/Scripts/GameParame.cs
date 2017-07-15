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
	}

    void OnDestory()
    {
        I = null;
    }
	
	void Update ()
    {
		
	}
}
