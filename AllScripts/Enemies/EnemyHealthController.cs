using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealthController : MonoBehaviour
{
    private int currentHelth = 10;
    [SerializeField]
    private GameObject[] ammo;
    
    public void Ammo()
    {
        int x = Random.Range(0, 10);
        if (x <= 1)
        {
            int y = Random.Range(0, ammo.Length);
            Instantiate(ammo[y],transform.position + new Vector3(0,.5f,0),transform.rotation);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHelth-= damage;
        
        if(currentHelth <= 0)
        {
            Ammo();
            GameManager.instance.AddZombieAcount();
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
       this.GetComponent<EnemyController>().Die();        
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
