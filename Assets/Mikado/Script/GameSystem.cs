using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem I;


    public enum GameState
    {
        Installation,//物を設置するターン
        Practice,//開業するターン
        dig,//掘るターン
    }

    GameState nowState;
    public GameState NowState
    {
        get { return nowState; }
    }


    GameState oldState = GameState.Installation;

    float nowTime;
    const float digTime = 5f;//5秒ごとに
    const float practiceTime = 5f;

    //時間がたったらボタンが押せます
    bool canPush;
    public bool CanPush
    {
        get { return canPush; }
    }




    void Awake()
    {
        if(I!=null)
        {
            Destroy(I);
            I = null;
        }
        I = this;
    }

    // Use this for initialization
    void Start()
    {
        nowTime = 0;
        nowState = GameState.Installation;
        oldState = GameState.dig;
    }

    // Update is called once per frame
    void Update()
    {
        switch (nowState)
        {

            //指定したボタンをクリックしたら次に進む
            case GameState.Installation:

                if (nowState != oldState)
                {
                    canPush = true;

                }

                break;



            //一定時間たったら、指定したボタンで掘るターンへ
            case GameState.Practice:

                if (nowState != oldState)
                {
                    canPush = false;
                }
                nowTime += Time.deltaTime;

                if (nowTime > practiceTime)
                {
                    nowTime = 0;
                    canPush = true;
                }


                break;


            //     //一定時間たったら、指定したボタンで設置ターンへ
            case GameState.dig:

                if (nowState != oldState)
                {
                    canPush = false;
                    nowTime = 0;
                }


                nowTime += Time.deltaTime;
                if (nowTime > digTime)
                {
                    nowTime = 0;
                    canPush = true;
                }

                break;

        }
        oldState = nowState;
    }

    public void NextButton()
    {
        if (nowState == GameState.dig)//最後の
            nowState = GameState.Installation;
        else
            nowState++;
    }


}
