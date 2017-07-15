using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{

    [SerializeField]
    Text tx;

    [SerializeField]
    GameObject gameSytemObject;

    public void ButtonPush()
    {
        if (gameSytemObject.GetComponent<GameSystem>().CanPush)
        gameSytemObject.GetComponent<GameSystem>().NextButton();
 

    }


    //後で消します
    void Update()
    {


        GameSystem.GameState gameState = gameSytemObject.GetComponent<GameSystem>().NowState ;


        switch (gameState)
        {

            case GameSystem.GameState.Installation:
                tx.text = "Installation";

                break;


            case GameSystem.GameState.Practice:
                tx.text = "Practice";

                break;



            case GameSystem.GameState.dig:
                tx.text = "dig";

                break;


        }



    }





    }
