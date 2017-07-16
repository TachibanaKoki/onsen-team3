using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessageSystem : MonoBehaviour
{
    public static MessageSystem I;

    [SerializeField]
    Text message;
    [SerializeField]
    GameObject MessageContent;

    public void SetMessage(string text,float duration = 5.0f)
    {
        MessageContent.SetActive(true);
        MessageContent.transform.DOKill();
        message.text = text;
        StopAllCoroutines();
        StartCoroutine(MessageControl(duration));
    }

    IEnumerator MessageControl(float duration )
    {
        MessageContent.transform.DOScale(1, 0.1f);
        yield return new WaitForSeconds(duration);
        MessageContent.transform.DOScale(0,0.1f);
    }

	// Use this for initializations
	void Start ()
    {
        I = this;
        MessageContent.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
