using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MovingPart
{

    protected Rigidbody2D[] bulletClone;

    //protected GameObject[] bulletClone;

    [SerializeField]
    protected Rigidbody2D bullet;

    [SerializeField]
    protected float bulletSpeed = 15;

    [SerializeField]
    protected float fireRate = 0.1f;


    protected void SetBulletCloneAmount(int bulletCloneAmount)
    {
        bulletClone = new Rigidbody2D[bulletCloneAmount];
    }

    private Vector2 bulletSpawnPoint;
    private Quaternion spawnRotation;
    protected float spawnRotationInDegrees = 0;
    protected Rigidbody2D bulletTypeToSpawn;
    protected float bulletsArraySpread;
    protected int bulletsPerArray;

    protected void SpawnAndRotateBullets()
    {
        //Gör antagligen inte det jag hade tänkt:
        bulletSpawnPoint = new Vector2(direction.x + transform.position.x, direction.y + transform.position.y);

        for (int i = 0; i < bulletsPerArray; i++)
        {
            //Creates a Quaternion for the bullets rotation.
            spawnRotation = Quaternion.AngleAxis(spawnRotationInDegrees, Vector3.forward);

            //Is this really needed?
            //direction.Normalize();

            SpawnBullet(i, bulletTypeToSpawn, spawnRotation);

            //Sets the rotation for the next bullet in the array
            spawnRotationInDegrees += bulletsArraySpread / (bulletsPerArray - 1);
        }
    }


    
    protected void SpawnBullet(int bulletIndex, Rigidbody2D bulletType, Quaternion spawnRotation)
    {

        GameObject bulletPooled = ObjectPooler.sharedInstance.GetPooledObject(bulletType);
        if(bulletPooled != null)
        {
            bulletPooled.transform.position = bulletSpawnPoint;
            bulletPooled.transform.rotation = spawnRotation;
            bulletPooled.SetActive(true);
        }
        
        
        bulletClone[bulletIndex] = bulletPooled.GetComponent<Rigidbody2D>();

        //bulletClone[bulletIndex] = Instantiate(bulletType, bulletSpawnPoint, spawnRotation);
        bulletClone[bulletIndex].velocity = transform.TransformDirection(bulletClone[bulletIndex].transform.up * bulletSpeed);


    }


}
