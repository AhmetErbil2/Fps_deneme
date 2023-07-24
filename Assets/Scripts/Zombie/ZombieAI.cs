using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


enum ZombieState
{
    Idle =0,
    Walk =1,
    Dead =2,
    Attack =3
}


public class ZombieAI : MonoBehaviour
{
    //ýdle
    //walk
    //attack
    //dead
    NavMeshAgent agent;
    Animator animator;
    ZombieState zombieState;
    GameObject playerobject;
    PlayerHealth playerHealth;
    ZombieHealt ZombieHealt;
    // Start is called before the first frame update
    void Start()
    {
        ZombieHealt = GetComponent<ZombieHealt>();
        playerobject = GameObject.FindWithTag("Player");
        playerHealth = playerobject.GetComponent<PlayerHealth>();
        zombieState = ZombieState.Idle;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ZombieHealt.GetHealth() <= 0)
        {
            SetState(ZombieState.Dead);
        }

        switch (zombieState)
        {
            case ZombieState.Dead:
                KillZombie();
                break;
            case ZombieState.Attack:
                Attack();
                break;
            case ZombieState.Walk:
                SearchForTarget();
                break;
            case ZombieState.Idle:
                SearchForTarget();
                break;

            default:
                break;
        }

    }

    private void Attack()
    {
        SetState(ZombieState.Attack);
        agent.isStopped = true;
        
    }
    void MakeAttack()
    {
        Debug.Log("attack");
        playerHealth.DeductHealth(10);
        SearchForTarget();

    }

    private void SearchForTarget()
    {
        float distance = Vector3.Distance(transform.position, playerobject.transform.position);
        if (distance<1.5f)
        {
            Attack();
        }
        else if (distance<10)
        {
            MoveToPlayer();
        }
        else
        {
            SetState(ZombieState.Idle);
            agent.isStopped = true;
        }
    }

    private void SetState(ZombieState state)
    {
        zombieState = state;
        //Animator
        animator.SetInteger("state", (int)state);
    }

    private void MoveToPlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(playerobject.transform.position);
        SetState(ZombieState.Walk);   
    }

    private void KillZombie()
    {
        SetState(ZombieState.Dead);
        agent.isStopped = true;
        Destroy(gameObject, 5);
    }
}
