using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GetDigItem : MonoBehaviour
{
    public static GetDigItem I;

    [SerializeField]
    Text GetItemText;

    Queue<string> texts = new Queue<string>();

    void Start()
    {
        I = this;
    }

	public void Open(string text)
    {
        texts.Enqueue(text);
        Debug.Log("DigItemOpen");
        if (texts.Count==1)
        {
            WindowAction(texts.Peek());
        }
       
    }

    public void WindowAction(string text)
    {
        GetItemText.text = text;
        transform.localScale = Vector3.zero;
        StartCoroutine(WindowActionEnumerator());
    }

    IEnumerator WindowActionEnumerator()
    {
        float duration = 2.0f;
        Debug.Log("WindowAction");
        transform.DOScale(1, 0.1f);
        yield return new WaitForSeconds(duration);
        transform.DOScale(0, 0.1f).OnKill(()=>
        {
            texts.Dequeue();
            if(texts.Count>0)
            {
                WindowAction(texts.Peek());
            }
        });
    }
}
