using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager m_instance;

    private string cueSheetName = "SE";
    private CriAtomSource atomSourceSE;
    private CriAtomSource atomSourceBGM;

    // Use this for initialization
    void Start ()
    {
        atomSourceSE = gameObject.AddComponent<CriAtomSource>();
        atomSourceSE.cueSheet = cueSheetName;

        atomSourceBGM = gameObject.AddComponent<CriAtomSource>();
        atomSourceBGM.cueSheet = cueSheetName;
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

    /**
     * @param [in] 鳴らしたいサウンド名
     * @param [in] 0.0f～1.0fまでの間で音量調整
     */
    public void PlaySE(string SeName_, float volume_)
    {
        atomSourceSE.volume = volume_;
        atomSourceSE.Play(SeName_);
    }
}
