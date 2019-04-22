using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MovingPart
{


    [SerializeField]
    protected int health;


    public virtual void HitShip(int bulletDamage)
    {
        health -= bulletDamage;
        if(health <= 0)
            Kill();
    }


    private void Kill()
    {
        Destroy(this.gameObject);
    }
    


}
