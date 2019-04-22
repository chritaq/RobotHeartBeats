using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonNew : Cannon
{
    public PatternTest pattern;

    [SerializeField] private Rigidbody2D orangeBullet;

    [SerializeField] private Rigidbody2D bulletType;

    private float patternTime; //flytta denna till PatternPattern? Ska kunna ställas in där.

    //private int bulletsPerArray;
    //private float arraySpread;

    private int totalBulletArrays;
    private float totalArraySpread;

    private float spinSpeed; //Samma som rotationAmount?
    private float spinSpeedChangeRate;
    private bool spinReversal;
    private float maxSpinSpeed;

    //private float fireRate;
    //private float bulletSpeed;
    private float bulletSpeedChangeRate;
    private bool bulletSpeedReversal;
    private float maxBulletSpeed;

    private float cannonWidth;
    private float cannonHeight;

    private float xOffset;
    private float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        patternTime = pattern.patternTime + Time.time;

        everyOtherOrange = pattern.everyOtherOrange;

        bulletType = pattern.bulletType;

        bulletsPerArray = pattern.bulletsPerArray;
        bulletsArraySpread = pattern.arraySpread;

        totalBulletArrays = pattern.totalBulletsArray;
        totalArraySpread = pattern.totalArraySpread;

        spinSpeed = pattern.spinSpeed; //Samma som rotationAmount?
        spinSpeedChangeRate = pattern.spinSpeedChangeRate;
        spinReversal = pattern.spinReversal;
        maxSpinSpeed = pattern.maxSpinSpeed;

        fireRate = pattern.fireRate;
        bulletSpeed = pattern.bulletSpeed;
        bulletSpeedChangeRate = pattern.bulletSpeedChangeRate;
        bulletSpeedReversal = pattern.bulletSpeedReversal;
        maxBulletSpeed = pattern.maxBulletSpeed;

        cannonWidth = pattern.cannonWidth;
        cannonHeight = pattern.cannonHeight;

        xOffset = pattern.xOffset;
        yOffset = pattern.yOffset;


    }

    // Update is called once per frame
    void Update()
    {
        StartPattern();
    }



    private void StartPattern()
    {
        if (Time.time < patternTime)
        {
            Shoot();
            Spin();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private float timeToNextShoot;
    private void Shoot()
    {
        if (Time.time > timeToNextShoot)
        {
            timeToNextShoot = Time.time + fireRate;
            ChooseAndSpawnBullets();
        }
    }

    private void ChooseAndSpawnBullets()
    {
        spawnRotationInDegrees = 0;

        for (int i = 0; i < totalBulletArrays; i++)
        {
            SetBulletCloneAmount(bulletsPerArray);

            TrySetBulletsToOrange();

            SpawnAndRotateBullets();

            spawnRotationInDegrees = totalArraySpread - bulletsArraySpread;
        }

    }


    private int loadCount = 0;
    private int everyOtherOrange;
    private void TrySetBulletsToOrange()
    {
        if(everyOtherOrange != 0)
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
        else
        {
            bulletTypeToSpawn = bullet;
        }
    }


    private bool reversed = false;
    private void Spin()
    {
        
        this.transform.Rotate(new Vector3(0, 0, spinSpeed));

        if(spinReversal)
        {
            if (spinSpeed >= maxSpinSpeed)
            {
                reversed = true;
            }

            if (spinSpeed <= -maxSpinSpeed)
            {
                reversed = false;
            }
        }

        if (spinSpeed < maxSpinSpeed && !reversed)
        {
            spinSpeed += spinSpeedChangeRate;
        }

        if(spinSpeed > -maxSpinSpeed && reversed)
        {
            spinSpeed -= spinSpeedChangeRate;
        }
        

    }
}
