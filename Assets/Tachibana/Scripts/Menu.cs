using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject MenuContent;
	// Use this for initialization
	public void Open()
    {
        MenuContent.SetActive(true);
        FacilityManager.I.SetCreateFacilityState(CreateFacilityState.None);
    }

    public void TitleBack()
    {
        SceneManager.LoadSceneAsync("Title");
    }

    public void Clese()
    {
        MenuContent.SetActive(false);
        SoundManager.m_instance.PlaySE("SE_backbutton", 1.0f);
    }
}
