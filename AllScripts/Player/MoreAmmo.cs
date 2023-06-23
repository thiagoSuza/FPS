using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreAmmo : MonoBehaviour
{

   public enum WeaponType
   {
        pistol,
        cas12,
        rifle
   }

    public WeaponType wt;


    public void Action()
    {
        if(wt == WeaponType.pistol)
        {
            WeaponAndAmmoController.instance.AmmoUp(0);
        }else if(wt == WeaponType.cas12)
        {
            WeaponAndAmmoController.instance.AmmoUp(1);
        }
        else
        {
            WeaponAndAmmoController.instance.AmmoUp(2);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Action();
            Destroy(gameObject);
        }
    }
}
