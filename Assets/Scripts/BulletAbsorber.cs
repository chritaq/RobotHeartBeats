using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbsorber : MonoBehaviour
{
    public bool isAbsorbing = false;

    [SerializeField]
    private Collider2D absorbCollider;

    [SerializeField]
    private PlayerCannon playerCannon;



    private void Start()
    {
        absorbCollider.enabled = false;
        //playerController = this.GetComponentInParent<PlayerCannon>();
    }



    public void ActivateBulletAbsorb()
    {
        if(absorbCollider.enabled == false)
        {
            StartCoroutine("StartBulletAbsorb");
        }

        
    }



    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float absorbTime = 3;

    private IEnumerator StartBulletAbsorb()
    {
        TurnOnAbsorber();
        yield return new WaitForSeconds(absorbTime);
        TurnOffAbsorber();
        yield return null;
    }

    private void TurnOnAbsorber()
    {
        isAbsorbing = true;
        spriteRenderer.enabled = true;
        absorbCollider.enabled = true;
    }

    private void TurnOffAbsorber()
    {
        isAbsorbing = false;
        spriteRenderer.enabled = false;
        absorbCollider.enabled = false;
    }



    private bool startCoroutine;

    public void OnBulletAbsorbed()
    {
        StartCoroutine(playerCannon.FireRateUp());
    }

}
