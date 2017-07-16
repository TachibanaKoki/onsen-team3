using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{

    Image image;
    [SerializeField]
    Image m_backImage;

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
                m_backImage.sprite = m_OpenSprite;
                image.fillAmount = 1.0f;
                break;


            case GameSystem.GameState.Practice:
                image.sprite = m_RunnigSpirte;
                m_backImage.sprite = m_RunnigSpirte;
                image.fillAmount = GameSystem.I.m_PracticeProcessed;
                break;



            case GameSystem.GameState.dig:
                image.sprite = m_DigSprite;
                m_backImage.sprite = m_DigSprite;
                image.fillAmount = GameSystem.I.m_DigProcessed;
                break;


        }



    }





    }
