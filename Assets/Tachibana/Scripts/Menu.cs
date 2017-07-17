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
        Camera.main.GetComponent<SceenFade>().LoadSceenWithFade("Title");
    }

    public void Clese()
    {
        MenuContent.SetActive(false);
    }
}
