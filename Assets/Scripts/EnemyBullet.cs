﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private bool isActive;

    [SerializeField]

    private bool isOrange = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        hitTag = "Player";
        if(isOrange) {
            hitTag2 = "Absorber";
        }
    }

}
