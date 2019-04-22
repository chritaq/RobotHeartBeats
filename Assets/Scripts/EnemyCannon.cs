using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : Cannon
{
    [SerializeField] PatternTest pattern;

    [SerializeField]
    protected Rigidbody2D orangeBullet;

    private void Start()
    {
        bulletSpeed = pattern.bulletSpeed;
        fireRate = pattern.fireRate;
        everyOtherOrange = pattern.everyOtherOrange;
        bulletsPerArray = pattern.bulletsPerFire;
        bulletsArraySpread = pattern.arraySpread;
        rotationAmount = pattern.rotationAmount;
    }

    public void RotateAndShoot()
    {
        Rotate();
        ShootTest();
    }

    private float timeToNextShoot;

    private void ShootTest()
    {
        if (Time.time > timeToNextShoot)
        {
            timeToNextShoot = Time.time + fireRate;
            ChooseAndSpawnBullet();
        }
    }


    private int loadCount = 0;
    private int everyOtherOrange;


    
    private void ChooseAndSpawnBullet()
    {
        
        SetBulletCloneAmount(bulletsPerArray);
        TrySetBulletToOrange();
        spawnRotationInDegrees = 0;
        SpawnAndRotateBullets();
        
    }

    private void TrySetBulletToOrange()
    {
        if (loadCount % everyOtherOrange == 0)
        {
            bulletTypeToSpawn = orangeBullet;
        }

        else
        {
            bulletTypeToSpawn = bullet;
        }

        loadCount++;
    }

    /*[SerializeField]*/
    private float rotationAmount;
    private void Rotate()
    {
        this.transform.Rotate(new Vector3(0, 0, rotationAmount));
    }
}
