using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour {

    public RectTransform rectTransform;

	// Use this for initialization
	public void GetRectTransform()
    {
        rectTransform = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
