using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private bool isActive;

    private FlashingFX flashingFx;

    void Start()
    {
        flashingFx = GetComponent<FlashingFX>();
    }

    public override void GetHit(int damage)
    {
        StartCoroutine(flashingFx.OneTimeFlash());
        base.GetHit(damage);
    }

}
