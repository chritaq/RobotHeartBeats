using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        bulletDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity(direction);
    }
}
