using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager m_instance;

    private string cueSheetSEName = "SE";
    private string cueSheetBGMName = "BGM";
    private CriAtomSource atomSourceSE;
    private CriAtomSource atomSourceBGM;

    // Use this for initialization
    void Start ()
    {
        atomSourceSE = gameObject.AddComponent<CriAtomSource>();
        atomSourceSE.cueSheet = cueSheetSEName;

        atomSourceBGM = gameObject.AddComponent<CriAtomSource>();
        atomSourceBGM.cueSheet = cueSheetBGMName;
	}

    void Awake()
    {
        if (m_instance != null)
        {
            Destroy(m_instance);
            m_instance = null;
        }
        m_instance = this;
    }

    public enum BGMKind
    {
        EINGYO,
        UNEI,

    }

    public void ClickSound()
    {
        atomSourceSE.androidUseLowLatencyVoicePool = true;
        atomSourceSE.Play("SE_okbutton");
    }

    public void BackButtonSound()
    {
        atomSourceSE.androidUseLowLatencyVoicePool = true;
        atomSourceSE.Play("SE_backbutton");
    }

    public void CraftFacilitySound()
    {
        atomSourceSE.androidUseLowLatencyVoicePool = true;
        atomSourceSE.Play("SE_constraction 1");
    }

    public void OnButtonSoound()
    {
        atomSourceSE.androidUseLowLatencyVoicePool = true;
        atomSourceSE.Play("SE_mouseover");
    }

    public void EigyoBGM()
    {
        atomSourceBGM.Play("bgm_eigyou_min");
    }

    public void ClicStartButton()
    {
        atomSourceSE.androidUseLowLatencyVoicePool = true;
        atomSourceSE.Play("SE_startbutton");
    }

    public void CoinGetSound()
    {
        atomSourceSE.androidUseLowLatencyVoicePool = true;
        atomSourceSE.Play("SE_coinget");
    }

    public void PauseBGM()
    {
        atomSourceBGM.Pause(true);
    }

    public void ResumeBGM()
    {
        atomSourceBGM.Pause(false);
    }

    public void StopBGM()
    {
        atomSourceBGM.Stop();
    }
}
