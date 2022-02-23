using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent nav;
    public Transform[] loc;
    public Transform player;


    int randomDes;

    //timer for the next point (also for patrolling)
    private float waitTime;
    public float startWithTime = 1.0f;

    //for enemy gizmo sphere
    public float radius;

    //a variabler declared to calculate between the player position
    //and the enemy position
    float distanceToPlayer = Mathf.Infinity;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        nav = GetComponent<NavMeshAgent>();

        waitTime = startWithTime;
        //randomSpot 
        randomDes = Random.Range(0, loc.Length);
    }

    // Update is called once per frame
    void Update()
    {
        ////if the player inside the enemies sphere
        ///the enemy will attack 
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer > radius)
        {
            Patrol();
        }
        else if (distanceToPlayer <= radius)
        {
            AttackPlayer();
        }

    }

    //the attack function logic
    //1 if the player bigger than or equals the enemies stopping distance, the enemy will chase
    //2 if the player less than or equals the enemies stopping distance, the enemy will shoot
    private void AttackPlayer()
    {
        if (distanceToPlayer >= nav.stoppingDistance)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= nav.stoppingDistance)
        {
            ShootPlayer();
        }
    }

    //function for when the enemy chases the player
    private void ChasePlayer()
    {
        nav.SetDestination(player.position);
    }

    //function when the player shoots the enemy
    private void ShootPlayer()
    {
        anim.Play("Lumbering");
        Debug.Log("SHOT");
    }

    //enemy patrol in 3 random directions
    private void Patrol()
    {
        nav.SetDestination(loc[randomDes].position);

        if (Vector3.Distance(transform.position, loc[randomDes].position) < 2.0f)
        {
            if (waitTime <= 0)
            {
                anim.Play("Walk");
                randomDes = Random.Range(0, loc.Length);

                waitTime = startWithTime;
            }
            else
            {
                anim.Play("Idle");
                waitTime -= Time.deltaTime;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}