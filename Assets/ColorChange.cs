using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField]
    private Color SecondColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void ChangeToSecondColor()
    {
        spriteRenderer.color = SecondColor;
    }

    public void ChangeToOriginalColor()
    {
        spriteRenderer.color = originalColor;
    }
}
