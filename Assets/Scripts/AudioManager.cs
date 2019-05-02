using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;

    [SerializeField]
    private AudioSource bossDamaged;

    [SerializeField]
    private AudioSource bulletDamaged;

    [SerializeField]
    private AudioSource bulletDamagedHard;

    [SerializeField]
    private AudioSource bulletDestroyed;

    [SerializeField]
    private AudioSource bulletDestroyed2;

    [SerializeField]
    private AudioSource weaponSwing;

    [SerializeField]
    private AudioSource playerDamaged;

    [SerializeField]
    private AudioSource getCharge;

    [SerializeField]
    private AudioSource changeWeapon;

    [SerializeField]
    private AudioSource playerDash;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBossDamaged()
    {
        bossDamaged.Play();
        bulletDestroyed.Play();
    }

    public void PlayBulletDamaged()
    {
        bulletDamaged.Play();
        //bulletDestroyed.Play();
    }

    public void PlayBulletDamagedHard()
    {
        bulletDamagedHard.Play();
    }

    public void PlayBulletDestroyed()
    {
        bulletDestroyed2.Play();
    }

    public void PlayPlayerDamaged()
    {
        playerDamaged.Play();
    }

    public void PlayWeaponSwing()
    {
        weaponSwing.Play();
    }

    public void PlayGetCharge()
    {
        getCharge.Play();
    }

    public void PlayChangeWeapon()
    {
        changeWeapon.Play();
    }

    public void PlayPlayerDash()
    {
        playerDash.Play();
    }

}
