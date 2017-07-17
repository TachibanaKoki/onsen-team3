using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextEffect : MonoBehaviour
{

    public void Initialize(int add)
    {
        GetComponent<Text>().text = ToString();
    }

	void Start ()
    {
        transform.DOMoveY(transform.position.y + 0.5f, 1.0f).OnKill(()=> { Destroy(gameObject); }) ;
	}
}
