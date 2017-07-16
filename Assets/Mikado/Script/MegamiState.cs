using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MegamiStateEnum
{
    LeftHand,
    RightHand,
    Hed,
    Body,
    LeftFut,
    RightFut,
}


public class MegamiState : MonoBehaviour
{

    MegamiStateEnum nowState=MegamiStateEnum.Body;
    public MegamiStateEnum NowState
    {
        get { return nowState; }
    }

    public void SetMegami(MegamiStateEnum m)
    {
        nowState = m;
    }




}
