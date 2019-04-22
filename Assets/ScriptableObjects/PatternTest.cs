using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Test Pattern", menuName = "Patterns")]

public class PatternTest : ScriptableObject
{
    

    //public float bulletSpeed;

    //public float fireRate;
    public int everyOtherOrange;

    public int bulletsPerFire;
    public float rotationAmount;

    public float patternTime;


    //New stuff
    public Rigidbody2D bulletType;

    //public float startDirection

    public int bulletsPerArray; //Samma som bulletsPerFire typ?
    public float arraySpread;

    public int totalBulletsArray;
    public float totalArraySpread;

    public float spinSpeed; //Samma som rotationAmount?
    public float spinSpeedChangeRate;
    public bool spinReversal;
    public float maxSpinSpeed;

    public float fireRate;
    public float bulletSpeed;
    public float bulletSpeedChangeRate;
    public bool bulletSpeedReversal;
    public float maxBulletSpeed;

    public float cannonWidth;
    public float cannonHeight;

    public float xOffset;
    public float yOffset;


}
