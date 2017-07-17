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
        get { return nowDig + 1; }
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
                old = true;
                break;


            case GameSystem.GameState.dig:
                if (old == true && canDig == true)//掘って獲得
                {
                    Dig();
                    old = false;
                    canDig = false;
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
                GetDigItem.I.Open("何も掘れなかった");
                break;

            case DigItem.Unagi:
                int value = Random.Range(20, 300);
                GameParame.I.Unagi += value;
                GetDigItem.I.Open("電気ウナギを" + value + "匹掘り当てた");
                break;


            case DigItem.Money:
                int money = Random.Range(1, 500);
                GameParame.I.Money += money;//適当
                GetDigItem.I.Open("鉱石を掘り当てた！" + money + "枚レナリウス銀貨を手に入れた");
                break;


            case DigItem.Megami://後でつけよう。。
                GetDigItem.I.Open("女神を掘り当てた！");
                GameObject.Instantiate(EffectDatas.I.Megami,transform);
                break;

            default:
                Debug.Log("掘るアイテムで例外");
                GetDigItem.I.Open("何も掘れなかった");
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


