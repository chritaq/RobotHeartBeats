using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : Cannon
{



    [SerializeField] private int bulletsToShoot = 3;
    [SerializeField] private float spreadBetweenBullets = 30;

    private void Start()
    {
        startingFireRate = fireRate;
        bulletTypeToSpawn = bullet;
        bulletsPerArray = bulletsToShoot;
        bulletsArraySpread = spreadBetweenBullets;
    }



    private float nextFire;
    
    public void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            
            SetBulletCloneAmount(bulletsPerArray);
            spawnRotationInDegrees = -(bulletsArraySpread / 2);
            SpawnAndRotateBullets();
        }
    }



    [SerializeField]
    private float fastFireRate = 0.08f;

    [SerializeField]
    private float fastRateTime = 5f;

    private float startingFireRate;

    public IEnumerator FireRateUp()
    {
        fireRate = fastFireRate;
        nextFire = Time.time + fireRate;
        yield return new WaitForSeconds(fastRateTime);
        fireRate = startingFireRate;
        yield return null;
    }



}
