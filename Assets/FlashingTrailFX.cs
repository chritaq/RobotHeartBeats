using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingTrailFX : MonoBehaviour
{
    private TrailRenderer trailRenderer;
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
        trailRenderer = GetComponent<TrailRenderer>();
        originalColor = trailRenderer.material.color;
    }

    private void OnEnable()
    {
        trailRenderer.material.color = originalColor;
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
        trailRenderer.material.color = constantFlashColor;
        yield return new WaitForSeconds(constantFlashRate);
        trailRenderer.material.color = originalColor;
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

        trailRenderer.material.color = oneTimeFlashColor;
        yield return new WaitForSeconds(oneTimeFlashTime);
        trailRenderer.material.color = originalColor;

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
        trailRenderer.material.color = originalColor;
        StopAllCoroutines();
    }
}
