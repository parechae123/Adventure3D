using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoExplane : MonoBehaviour
{
    [SerializeField] Text text;
    Coroutine coroutine;
    void Start()
    {
        GameManager.GetInstance.printInfo = PrintText;
        gameObject.SetActive(false);
    }
    void PrintText(string str)
    {
        gameObject.SetActive(true);
        if (coroutine != null)StopCoroutine(coroutine);
        coroutine = StartCoroutine(Print(str));
    }
    IEnumerator Print(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            yield return new WaitForSeconds(0.03f);
            text.text = str.Substring(0, i);
        }
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    
}
