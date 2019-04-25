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
    private AudioSource bulletDestroyed;

    [SerializeField]
    private AudioSource weaponSwing;

    [SerializeField]
    private AudioSource playerDamaged;

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
    }

    public void PlayBulletDestroyed()
    {
        bulletDestroyed.Play();
    }

    public void PlayPlayerDamaged()
    {
        playerDamaged.Play();
    }

    public void PlayWeaponSwing()
    {
        weaponSwing.Play();
    }
}
