using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
   public static WeaponSound instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField]
    private AudioSource aud;

    [SerializeField]
    private AudioClip[] weapons;

    [SerializeField]
    private AudioClip[] reloadOnOff;

    [SerializeField]
    private AudioClip[] zombieBit;

    [SerializeField]
    private AudioClip[] zombieDamaged;


    public void WeaponShoot(int aux)
    {
        aud.PlayOneShot(weapons[aux]);
    }

    public void WeaponReload(int aux)
    {
        aud.PlayOneShot(reloadOnOff[aux]);
    }

    public void ZombieBits()
    {
        int x = Random.Range(0,zombieBit.Length);
        aud.PlayOneShot(zombieBit[x]);
    }
}
