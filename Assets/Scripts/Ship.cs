using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MovingPart, IHitable
{


    [SerializeField]
    protected int health;

    [SerializeField]
    protected ScreenShake screenShake;

    public virtual void GetHit(int damage)
    {
        screenShake.ShakeCam();
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
