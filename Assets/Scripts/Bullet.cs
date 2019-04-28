using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovingPart, IHitable
{
    private Ship playerShip;

    [SerializeField]
    protected int bulletDamage = 1;
    [SerializeField]
    protected int bulletMaxHealth = 4;

    protected int bulletHealth;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.SendMessage("GetHit", bulletDamage);
            Debug.Log("Bullet Died");
            Die();
            
        }
    }

    private void OnEnable()
    {
        bulletHealth = bulletMaxHealth;
    }


    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void GetHit(int damage)
    {
        bulletHealth -= damage;
        if(bulletHealth <= 0)
        {
            //AudioManager.instance.PlayBulletDestroyed();
            Die();
        }
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }
}
