using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonNew : Cannon
{

    public PatternTest pattern;
    [SerializeField] private Rigidbody2D orangeBullet;
    [SerializeField] private Rigidbody2D bulletType;
    //flytta denna till PatternPattern? Ska kunna ställas in där.
    private float patternTime; 

    private int totalBulletArrays;
    private float totalArraySpread;

    //Samma som rotationAmount?
    private float spinSpeed; 
    private float spinSpeedChangeRate;
    private bool spinReversal;
    private float maxSpinSpeed;

    private float bulletSpeedChangeRate;
    private bool bulletSpeedReversal;
    private float maxBulletSpeed;

    private float cannonWidth;
    private float cannonHeight;

    private float xOffset;
    private float yOffset;


    void Start()
    {
        patternTime = pattern.patternTime + Time.time;
        everyOtherOrange = pattern.everyOtherOrange;
        bulletType = pattern.bulletType;

        bulletsPerArray = pattern.bulletsPerArray;
        bulletsArraySpread = pattern.arraySpread;
        totalBulletArrays = pattern.totalBulletsArray;
        totalArraySpread = pattern.totalArraySpread;

        //Samma som rotationAmount?
        spinSpeed = pattern.spinSpeed; 
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

            //Borde förklara vad detta gör.
            spawnRotationInDegrees = totalArraySpread - bulletsArraySpread;
        }
    }


    //Denna ska skrivas om till nytt system.
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

        //Bör delas upp i mindre funktioner
        if(spinReversal)
        {
            TrySpinReversal();
        }

        TryChangeSpinSpeed();
    }


    private void TrySpinReversal()
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


    private void TryChangeSpinSpeed()
    {
        if (spinSpeed < maxSpinSpeed && !reversed)
        {
            spinSpeed += spinSpeedChangeRate;
        }

        if (spinSpeed > -maxSpinSpeed && reversed)
        {
            spinSpeed -= spinSpeedChangeRate;
        }
    }
}
