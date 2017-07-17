using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransration : MonoBehaviour {

    public void SceneLoad()
    {
        SceneManager.LoadSceneAsync("main");
    }

    public void LoadScene(string name)
    {
        Camera.main.GetComponent<SceenFade>().LoadSceenWithFade(name);
    }

}
