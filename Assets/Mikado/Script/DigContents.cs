using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//何もないところの地下の中身
public class DigContents : MonoBehaviour
{

    public const int c_maxDig = 10;
    public const int c_costMoney = 100;//適当、１００円ごと増えていく
    public const int c_costUnagi = 10;


    public bool canDig = false;

    bool old = false;

     int nowDig = 0;//今どのぐらい掘っているか
    public int NowDig
    {
        get { return nowDig+1; }
    }


    int digNumber = 0;

    GameObject systemGameObject;

    GameObject facilityObjectParent;

    ////うなぎとお金投資金
    //public int investmentMoney = 0;
    //public int investmentUnagi = 0;



    public enum DigItem
    {
        None,
        Money,
        Unagi,
        Megami,
    }


    DigItem[] digItemList = new DigItem[c_maxDig];

    //Use this for initialization

    void Start()
    {
        systemGameObject = GameObject.Find("GameSystem");
        facilityObjectParent = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {


        switch (systemGameObject.GetComponent<GameSystem>().NowState)
        {
            case GameSystem.GameState.Installation:
            case GameSystem.GameState.Practice:
                if (facilityObjectParent.GetComponent<Facility>().facilityType != FacilityType.None && facilityObjectParent.GetComponent<Facility>().facilityType != FacilityType.Digging)
                    Destroy(this);
                old = true;
                break;


            case GameSystem.GameState.dig:
                if (old == true)//掘って獲得
                {
                    Dig();
                    old = false;
                }
                break;
        }


    }




    private void Dig()
    {
        nowDig++;
            switch ((DigItem)digItemList[nowDig])
            {
                case DigItem.None:
                    break;

                case DigItem.Unagi:
                    GameParame.I.Unagi += Random.Range(20, 100);
                    break;


                case DigItem.Money:
                    GameParame.I.Money += Random.Range(20, 100);//適当
                    break;


                case DigItem.Megami://後でつけよう。。
                    break;

                default:
                    Debug.Log("掘るアイテムで例外");

                    break;

            }

        

    }

    public void SetDigState(DigItem digItem)
    {
        int count = 0;
        while (true)
        {
            count++;
            if (count > 5)
                break;
            int random = Random.Range(0, c_maxDig);
            if (digItemList[random] == DigItem.None)
            {
                digItemList[random] = digItem;
                break;
            }
        }

    }



}


