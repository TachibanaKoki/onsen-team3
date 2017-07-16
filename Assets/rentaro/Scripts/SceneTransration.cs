using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransration : MonoBehaviour {

    public void SceneLoad()
    {
        Debug.Log("Push Button");
        SceneManager.LoadSceneAsync("main");
    }

}
