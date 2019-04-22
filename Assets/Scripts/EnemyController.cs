using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Ship
{
    //[SerializeField]
    //private EnemyCannon cannon;

    private void FixedUpdate()
    {
        //cannon.RotateAndShoot();
    }


    public override void HitShip(int bulletDamage)
    {
        base.HitShip(bulletDamage);
        UpdateHealthUI();
    }


    //Temp! Should be updated to healthbar!
    [SerializeField]
    private Text healthText;
    private void UpdateHealthUI()
    {
        healthText.text = "Enemy Health is: " + health;
    }

    
}
