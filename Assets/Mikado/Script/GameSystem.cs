using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem I;

    //自動進行中
    public bool isAuto = false;
    public enum GameState
    {
        Installation,//物を設置するターン
        Practice,//開業するターン
        dig,//掘るターン
        Wait
    }

    public float m_DigProcessed
    {
        get { return nowTime / digTime; }
    }
    public float m_PracticeProcessed
    {
        get { return nowTime / practiceTime; }
    }

    public UnityEngine.Events.UnityAction practiceCallback;
    GameState nowState;
    public GameState NowState
    {
        get {
            if (isAuto)
            {
                return nowState;
            }
            else if(nowState==GameState.Installation)
            {
                return nowState;
            }
            else
            {
                return GameState.Wait;
            }
        }
    }


    GameState oldState = GameState.Installation;


    float nowTime;
    const float digTime = 5f;//5秒ごとに
    const float practiceTime = 30.0f;

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
                    isAuto = false;
                }

                break;



            //一定時間たったら、指定したボタンで掘るターンへ
            case GameState.Practice:

                if (nowState != oldState)
                {
                    canPush = false;
                    isAuto = true;
                }
                nowTime += Time.deltaTime;
                
                if (nowTime > practiceTime)
                {
                    nowTime = 0;
                    isAuto = false;
                    canPush = true;
                }


                break;


            //     //一定時間たったら、指定したボタンで設置ターンへ
            case GameState.dig:

                if (nowState != oldState)
                {
                    isAuto = true;
                    canPush = false;
                    nowTime = 0;
                }


                nowTime += Time.deltaTime;
                if (nowTime > digTime)
                {
                    isAuto = false;
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

        if(nowState == GameState.dig)
        {
            MessageSystem.I.SetMessage("君を掘り当てる！");
        }
        else if(nowState==GameState.Practice)
        {
            MessageSystem.I.SetMessage("さあ、今日の仕事を始めようぞ");
            if(practiceCallback!=null)practiceCallback.Invoke();
        }
    }


}
