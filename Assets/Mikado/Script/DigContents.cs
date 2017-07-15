using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//何もないところの地下の中身
public class  DigContents: MonoBehaviour {

    //private const int MaxDig = 100;
    //private int nowDig = 0;//今どのぐらい掘っているか

    //[SerializeField]
    //GameObject systemGameObject;

    //[SerializeField]
    //GameObject facilityObject;



    enum DigItem
    {
        None,
        Money,
        Unagi,
        Megami,
    }


  //  int[] digItemList = new int[MaxDig];

    // Use this for initialization
    void Start () {

        ////いったん女神なしのランダムい格納します。
        //for(int i=0;i<digItemList.Length;i++)
        //{
        //    digItemList[i] = Random.Range(0,3);
        //}


    }
	
	// Update is called once per frame
	void Update () {


        //switch (systemGameObject.GetComponent<GameSystem>().NowState)
        //{
        //    case GameSystem.GameState.Installation:
        //    case GameSystem.GameState.Practice:
        //        if (facilityObject.GetComponent<Facility>().facilityType != FacilityType.None)
        //            Destroy(this);
        //        break;


        //    case GameSystem.GameState.dig:


        //        break;

        //}


    }

    public void Dig(int number)//指定した数字分掘ります
    {

        //int initialDig = nowDig;
        //for (int i= initialDig; i<initialDig+number;i++)
        //{
        //    if (nowDig >=100)
        //        break;

        //    nowDig++;
        //   switch((DigItem)digItemList[nowDig])
        //    {
        //        case DigItem.None:
        //            break;

        //        case DigItem.Unagi:
        //            break;
        //        case DigItem.Money:
        //            break;
        //        case DigItem.Megami:

        //        default:
        //            Debug.Log("掘るアイテムで例外");

        //            break;

        //    }

        //}

    }




}


