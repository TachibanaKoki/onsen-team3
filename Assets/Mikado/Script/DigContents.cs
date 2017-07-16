using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//何もないところの地下の中身
public class DigContents : MonoBehaviour
{

    public const int MaxDig = 30;
    private int nowDig = 0;//今どのぐらい掘っているか

   
    GameObject systemGameObject;

    GameObject facilityObjectParent;



    public enum DigItem
    {
        None,
        Money,
        Unagi,
        Megami,
    }


    DigItem[] digItemList = new DigItem[MaxDig];

    //Use this for initialization

    void Start()
    {
        systemGameObject=GameObject.Find("GameSystem");
        facilityObjectParent = this.transform.parent.gameObject;

        //いったん女神なしのランダム格納します。
        for (int i = 0; i < digItemList.Length; i++)
        {
            digItemList[i] = (DigItem)Random.Range(0, 3);
        }


    }

    // Update is called once per frame
    void Update()
    {


        switch (systemGameObject.GetComponent<GameSystem>().NowState)
        {
            case GameSystem.GameState.Installation:
            case GameSystem.GameState.Practice:
                if (facilityObjectParent.GetComponent<Facility>().facilityType != FacilityType.None)
                    Destroy(this);
                break;


            case GameSystem.GameState.dig:


                break;

        }


    }

    public void Dig(int number)//指定した数字分掘ります
    {

        int initialDig = nowDig;
        for (int i = initialDig; i < initialDig + number; i++)
        {
            if (nowDig >= 100)
                break;

            nowDig++;
            switch ((DigItem)digItemList[nowDig])
            {
                case DigItem.None:
                    break;

                case DigItem.Unagi:
                    break;
                case DigItem.Money:
                    break;
                case DigItem.Megami:

                default:
                    Debug.Log("掘るアイテムで例外");

                    break;

            }

        }

    }

    public void SetDigState(DigItem digItem)
    {
        while (true)
        {
            int random = Random.Range(0, MaxDig);
            if (digItemList[random] == DigItem.None)
            {
                digItemList[random] = digItem;
                break;
            }
        }

    }



}


