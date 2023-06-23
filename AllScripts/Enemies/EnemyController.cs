using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float distanceToChase;
    [SerializeField]
    private float distanceToStop;
    [SerializeField]
    private float distanceToStopNearPlayer = 8f;
    private NavMeshAgent agent;

    [SerializeField]
    private Animator anim;

    private Rigidbody rb;
    private bool chasing;
    private Vector3 target,initialPos;
    private bool isAlive;

    [SerializeField]
    private GameObject damager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Move();
        }
       
    }

    public void Move()
    {
        
            target = PlayerController.instance.transform.position;
            target.y = transform.position.y;
            if (!chasing)
            {
                if (Vector3.Distance(transform.position, target) <= distanceToChase)
                {
                    chasing = true;
                    anim.SetBool("Move", true);
                }

                if (Vector3.Distance(transform.position, initialPos) <= .8f)
                {
                    anim.SetBool("ATK", false);
                    anim.SetBool("Move", false);
                    anim.SetBool("Walk", false);
                }
            }
            else
            {
                //  transform.LookAt(target);
                //rb.velocity = transform.forward * moveSpeed;
                if (Vector3.Distance(transform.position, target) > distanceToStopNearPlayer)
                {
                    agent.destination = target;
                    anim.SetBool("ATK", false);
                    anim.SetBool("Move", true);
                    anim.SetBool("Walk", false);
                }
                else
                {
                    agent.destination = transform.position;
                    anim.SetBool("ATK", true);
                    anim.SetBool("Move", false);
                    anim.SetBool("Walk", false);
                }

                if (Vector3.Distance(transform.position, target) > distanceToStop)
                {
                    chasing = false;
                    agent.destination = initialPos;
                    anim.SetBool("ATK", false);
                    anim.SetBool("Move", false);
                    anim.SetBool("Walk", true);
                }


            }

        



    }

    public void Die()
    {
        
        chasing = false;
        isAlive = false;
        agent.destination = transform.position;
        moveSpeed = 0;
        anim.SetBool("ATK", false);
        anim.SetBool("Move", false);
        anim.SetBool("Walk", false);
        anim.SetTrigger("Die");
        damager.SetActive(false);
    }

}

  

