using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MovingPart, IHitable
{


    [SerializeField]
    protected int health;

    public virtual void GetHit(int damage)
    {
        health -= damage;
        if (health <= 0)
            Kill();
    }


    public virtual void Kill()
    {
        //SceneManager.LoadScene(2);
        Destroy(this.gameObject);
    }
    


}
