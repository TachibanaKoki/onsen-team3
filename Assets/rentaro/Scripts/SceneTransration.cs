using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransration : MonoBehaviour {

    public void SceneLoad()
    {
        SoundManager.m_instance.PlaySE("SE_startbutton", 1.0f);
        SceneManager.LoadSceneAsync("main");
    }

}
