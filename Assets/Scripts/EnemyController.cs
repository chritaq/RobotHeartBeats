using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Ship
{
    public override void GetHit(int damage)
    {
        base.GetHit(damage);
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
