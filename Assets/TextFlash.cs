using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour
{
    private Text text;
    private Color originalColor;

    [SerializeField]
    private float constantFlashRate;
    [SerializeField]
    private float oneTimeFlashTime;

    [SerializeField]
    private Color constantFlashColor;
    [SerializeField]
    private Color oneTimeFlashColor;

    [SerializeField]
    public bool constantFlash;

    //private bool isFlashing;

    private void Awake()
    {
        text = GetComponent<Text>();
        originalColor = text.color;
    }

    private void Start()
    {
        if(constantFlash)
        {
            StartCoroutine("ConstantFlash");
        }
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ConstantFlash()
    {
        text.color = constantFlashColor;
        yield return new WaitForSeconds(constantFlashRate);
        text.color = originalColor;
        yield return new WaitForSeconds(constantFlashRate);
        StartCoroutine("ConstantFlash");
        yield return null;
    }

    public IEnumerator OneTimeFlash()
    {
        if (constantFlash)
        {
            StopCoroutine("ConstantFlash");
        }

        text.color = oneTimeFlashColor;
        yield return new WaitForSeconds(oneTimeFlashTime);
        text.color = originalColor;

        if (constantFlash)
        {
            StartCoroutine("ConstantFlash");
        }

        yield return null;
    }

    public void StartConstantFlash()
    {
        StartCoroutine("ConstantFlash");
    }

    public void StopAllFlash()
    {
        text.color = originalColor;
        StopAllCoroutines();
    }
}
