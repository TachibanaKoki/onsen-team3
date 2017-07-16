using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{

    [SerializeField]
    Image image;

    [SerializeField]
    Sprite m_OpenSprite;

    [SerializeField]
    Sprite m_RunnigSpirte;

    [SerializeField]
    Sprite m_DigSprite;

    [SerializeField]
    GameObject gameSytemObject;

    void Start()
    {
        image = GetComponent<Image>();
    }

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
                image.sprite = m_OpenSprite;

                break;


            case GameSystem.GameState.Practice:
                image.sprite = m_RunnigSpirte;
                break;



            case GameSystem.GameState.dig:
                image.sprite = m_DigSprite;
                break;


        }



    }





    }
