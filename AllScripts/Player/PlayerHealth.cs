using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    [SerializeField]
    private Slider sliderBar;
    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        sliderBar.maxValue = 10;
        sliderBar.value = health;
    }

   public void TakeDamage()
    {
        health--;
        sliderBar.value = health;
        WeaponSound.instance.ZombieBits();
        if (health <= 0)
        {
            BtnsController.instance.Dead();
        }
    }
}
