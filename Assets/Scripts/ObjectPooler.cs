using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler sharedInstance;

    public List<GameObject> pooledWhiteBullets;
    public GameObject WhiteBulletsToPool;
    public int amountToPool;

    public List<GameObject> pooledOrangeBullets;
    public GameObject orangeBulletsToPool;

    //public List<GameObject> pooledBlueBullets;
    //public GameObject blueBulletsToPool;

    private void Awake()
    {
        sharedInstance = this;
    }


    private void Start()
    {
        pooledWhiteBullets = new List<GameObject>();
        pooledOrangeBullets = new List<GameObject>();
        //pooledBlueBullets = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject whiteBulletObject = (GameObject)Instantiate(WhiteBulletsToPool, this.transform);
            whiteBulletObject.SetActive(false);
            pooledWhiteBullets.Add(whiteBulletObject);

            GameObject orangeBulletObject = (GameObject)Instantiate(orangeBulletsToPool, this.transform);
            orangeBulletObject.SetActive(false);
            pooledOrangeBullets.Add(orangeBulletObject);

            //GameObject blueBulletObject = (GameObject)Instantiate(blueBulletsToPool, this.transform);
            //blueBulletObject.SetActive(false);
            //pooledBlueBullets.Add(blueBulletObject);
        }
    }

    public GameObject GetPooledObject(Rigidbody2D bulletType)
    {

        if(bulletType.name == "EnemyBullet")
        {
            for (int i = 0; i < pooledWhiteBullets.Count; i++)
            {
                if (!pooledWhiteBullets[i].activeInHierarchy)
                {
                    return pooledWhiteBullets[i];
                }
            }
        }

        else if (bulletType.name == "EnemyBulletOrange")
        {
            for (int i = 0; i < pooledOrangeBullets.Count; i++)
            {
                if (!pooledOrangeBullets[i].activeInHierarchy)
                {
                    return pooledOrangeBullets[i];
                }
            }
        }

        //else if (bulletType.name == "EnemyBulletBlue")
        //{
        //    for (int i = 0; i < pooledBlueBullets.Count; i++)
        //    {
        //        if (!pooledBlueBullets[i].activeInHierarchy)
        //        {
        //            return pooledBlueBullets[i];
        //        }
        //    }
        //}

        return null;
    }

}
