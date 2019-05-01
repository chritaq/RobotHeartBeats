using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField]
    private int weaponDamage = 1;
    [SerializeField]
    private float swingTime = 0.1f;
    [SerializeField]
    private float swingRate = 0.2f;

    private float nextSwing;

    [SerializeField]
    private Collider2D orangeCollider;
    [SerializeField]
    private Collider2D blueCollider;

    private ColorChange colorChange;
    
    private enum SwordColor {orange, blue};
    private SwordColor activeColor;

    private void Start()
    {
        colorChange = GetComponentInParent<ColorChange>();
        activeColor = SwordColor.orange;
        orangeCollider.enabled = false;
        blueCollider.enabled = false;
        //orangeSpriteRenderer.enabled = false;
        //blueSpriteRenderer.enabled = false;
    }


    public void TrySwingWeapon()
    {
        if (Time.time > nextSwing)
        {
            if(orangeCollider.enabled == false && blueCollider.enabled == false)
            {
                AudioManager.instance.PlayWeaponSwing();
                StartCoroutine("SwingWeapon");
                nextSwing = Time.time + swingRate;
            }
        }
    }


    private IEnumerator SwingWeapon()
    {
        TurnOnWeapon();
        yield return new WaitForSeconds(swingTime);
        TurnOffWeapon();
        yield return null;
    }


    [SerializeField]
    private SpriteRenderer orangeSpriteRenderer;
    [SerializeField]
    private SpriteRenderer blueSpriteRenderer;
    [SerializeField]
    private Animator orangeAnimator;
    [SerializeField]
    private Animator blueAnimator;

    private void TurnOnWeapon()
    {
        if(activeColor == SwordColor.orange)
        {
            //orangeSpriteRenderer.enabled = true;
            orangeAnimator.SetTrigger("TriggerSwooshAnimation");
            orangeCollider.enabled = true;
        }
        if (activeColor == SwordColor.blue)
        {
            //blueSpriteRenderer.enabled = true;
            blueAnimator.SetTrigger("TriggerSwooshAnimation");
            blueCollider.enabled = true;
        }

    }


    private void TurnOffWeapon()
    {
        //orangeSpriteRenderer.enabled = false;
        orangeCollider.enabled = false;
        //blueSpriteRenderer.enabled = false;
        blueCollider.enabled = false;
    }


    public void ChangeWeaponColor()
    {
        AudioManager.instance.PlayChangeWeapon();
        if (activeColor == SwordColor.orange)
        {
            activeColor = SwordColor.blue;
            colorChange.ChangeToSecondColor();
        }
        else if (activeColor == SwordColor.blue)
        {
            activeColor = SwordColor.orange;
            colorChange.ChangeToOriginalColor();
        }
    }


    public int GetWeaponDamage()
    {
        return weaponDamage;
    }
}
