using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAndAmmoController : MonoBehaviour
{
    public static WeaponAndAmmoController instance;

    [SerializeField]
    private GameObject[] weapons;
    public int weaponIndex;
    public bool canShoot;

    [SerializeField]
    private Text magazine, total;


    private int pistolBulletAmount = 80;
    private int smgBulletAmount = 30;
    private int rifleBulletAmount = 300;

    private int pistolMagazineAmount = 8;
    private int smgMagazineAmount = 6;
    private int rifleMagazineAmount = 30;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        weapons[0].SetActive(true);
        weaponIndex = 0;
        magazine.text = pistolMagazineAmount.ToString();
        total.text = pistolBulletAmount.ToString();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveWeapon();
        ReloadWeapon();
        MagazineController();
    }


    public void ActiveWeapon()
    {
        
        //Pistol
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var weapon in weapons)
            {
                weapon.SetActive(false);
            }
            weapons[0].SetActive(true);
            weaponIndex = 0;
            magazine.text = pistolMagazineAmount.ToString();
            total.text = pistolBulletAmount.ToString();
        }

        //SMG
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (var weapon in weapons)
            {
                weapon.SetActive(false);
            }
            weapons[1].SetActive(true);
            weaponIndex = 1;
            magazine.text = smgMagazineAmount.ToString();
            total.text = smgBulletAmount.ToString();
        }

        //Rifle
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (var weapon in weapons)
            {
                weapon.SetActive(false);
            }
            weapons[2].SetActive(true);
            weaponIndex = 2;
            magazine.text = rifleMagazineAmount.ToString();
            total.text = rifleBulletAmount.ToString();
        }
    }

    public void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(weaponIndex == 0)
            {
                if(pistolBulletAmount >0)
                {
                    WeaponSound.instance.WeaponReload(1);
                    pistolBulletAmount -= 8;
                    pistolMagazineAmount = 8;
                    magazine.text = pistolMagazineAmount.ToString();
                    total.text = pistolBulletAmount.ToString();
                }
                else
                {
                    WeaponSound.instance.WeaponReload(0);
                }
            }

            if (weaponIndex == 1)
            {
                if (smgBulletAmount > 0)
                {
                    WeaponSound.instance.WeaponReload(1);
                    smgBulletAmount -= 6;
                    smgMagazineAmount = 6;
                    magazine.text = smgMagazineAmount.ToString();
                    total.text = smgBulletAmount.ToString();
                }
                else
                {
                    WeaponSound.instance.WeaponReload(0);
                }
            }


            if (weaponIndex == 2)
            {
                if (rifleBulletAmount > 0)
                {
                    WeaponSound.instance.WeaponReload(1);
                    rifleBulletAmount -= 30;
                    rifleMagazineAmount = 30;
                    magazine.text = rifleMagazineAmount.ToString();
                    total.text = rifleBulletAmount.ToString();
                }
                else
                {
                    WeaponSound.instance.WeaponReload(0);
                }
            }
        }
    }

    public void MagazineController()
    {
        if (weaponIndex == 0)
        {
            if(pistolMagazineAmount > 0)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }
        }

        if (weaponIndex == 1)
        {
            if (smgMagazineAmount > 0)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }
        }

        if (weaponIndex == 2)
        {
            if (rifleMagazineAmount > 0)
            {
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }
        }
    }


    public void PistolShoot()
    {
        pistolMagazineAmount--;
        if(weaponIndex == 0)
        {
            magazine.text = pistolMagazineAmount.ToString();
            
        }
    }

    public void Cal12Shoot()
    {
        smgMagazineAmount--;
        if (weaponIndex == 1)
        {
            magazine.text = smgMagazineAmount.ToString();
            
        }
    }

    public void RifleShoot()
    {
        rifleMagazineAmount -= 3;
        if (weaponIndex == 2)
        {
            magazine.text = rifleMagazineAmount.ToString();
           
        }
    }

    public void AmmoUp(int x)
    {
        if(x == 0)
        {
            pistolBulletAmount += 24;
        }else if(x == 1)
        {
            smgBulletAmount += 18;
        }
        else if (x == 2) 
        {
            rifleBulletAmount += 90;
        }

        if(weaponIndex ==0)
        {
            total.text = pistolBulletAmount.ToString();
        }
        else if(weaponIndex == 1)
        {
            total.text = smgBulletAmount.ToString();
        }
        else if(weaponIndex == 2)
        {
            total.text = rifleBulletAmount.ToString();
        }
    }

}
