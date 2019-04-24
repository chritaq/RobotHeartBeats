using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovingPart, IHitable
{
    private Ship playerShip;

    [SerializeField]
    protected int bulletDamage = 1;
    [SerializeField]
    protected int bulletHealth = 4;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.SendMessage("GetHit", bulletDamage);
            Die();
        }
    }


    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    public void GetHit(int damage)
    {
        bulletHealth -= damage;
        if(bulletHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
