using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
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
    private bool constantFlash;

    //private bool isFlashing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnEnable()
    {
        spriteRenderer.color = originalColor;
        if (constantFlash)
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
        spriteRenderer.color = constantFlashColor;
        yield return new WaitForSeconds(constantFlashRate);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(constantFlashRate);
        StartCoroutine("ConstantFlash");
        yield return null;
    }

    public IEnumerator OneTimeFlash()
    {
        if(constantFlash)
        {
            StopCoroutine("ConstantFlash");
        }
        
        spriteRenderer.color = oneTimeFlashColor;
        yield return new WaitForSeconds(oneTimeFlashTime);
        spriteRenderer.color = originalColor;

        if(constantFlash) {
            StartCoroutine("ConstantFlash");
        }

        yield return null;
    }
}
