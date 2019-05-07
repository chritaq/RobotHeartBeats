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

    [SerializeField]
    private GameObject particleFX;
    private GameObject particleInstance;

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    private float deathPosX;
    private float deathPosY;
    public virtual void GetHit(int damage)
    {
        bulletHealth -= damage;
        if(bulletHealth <= 0)
        {
            //deathPosX = this.transform.position.x;
            //deathPosY = this.transform.position.y;
            //Debug.Log();
            particleInstance = Instantiate(particleFX, this.transform.position, this.transform.rotation);
            //AudioManager.instance.PlayBulletDestroyed();
            Die();
        }
    }


    private void Die()
    {
        AudioManager.instance.PlayBulletDestroyed();
        this.gameObject.SetActive(false);
    }

    private void PlayParticleEffect()
    {
        //particleInstance = Instantiate(particleFX, this.transform);
    }
}
