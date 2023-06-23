using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int damage;
    private Rigidbody rb;

    [SerializeField]
    private GameObject zombieEffectHit;
    [SerializeField]
    private GameObject EffectHit;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject,10f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealthController>().TakeDamage(damage);
            Instantiate(zombieEffectHit, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(EffectHit, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
