using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovingPart
{
    protected string hitTag;
    protected string hitTag2;
    private Ship shipHit;

    [SerializeField]
    protected int bulletDamage = 1;

    private void Start()
    {
        //Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == hitTag || other.gameObject.tag == hitTag2)
        {
            CheckTagOfObject(other);
            this.gameObject.SetActive(false);
        }

        
    }

    private void CheckTagOfObject(Collider2D other)
    {
        HitShip(other);
    }

    

    private void HitShip(Collider2D other)
    {
        if (hitTag == "Player")
        {
            shipHit = other.gameObject.GetComponent<PlayerController>();
            shipHit.HitShip(bulletDamage);
        }

        else if (hitTag == "Enemy")
        {
            shipHit = other.gameObject.GetComponent<Ship>();
            shipHit.HitShip(bulletDamage);
        }
        
        
    }


    //When bullet is out of screen
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
