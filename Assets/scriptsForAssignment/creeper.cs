using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class creeper : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        changeState(EnemyState.Stroll);
        changeTriggerState(TriggerState.None);
        nav = GetComponent<NavMeshAgent>();
        strollIdx = 0;
        isArrived = false;
    }

    public override void attack(){
        Debug.Log("attack");
    }

    // Update is called once per frame
    void Update()
    {
        //any state
        if (HP < 20) 
        {
            changeState(EnemyState.Flee);
            if (HP < 0)
            {
                changeState(EnemyState.Dead);
            }
        } 
        

        switch (currentState)
        {
            case EnemyState.Attack:
                if (triggerState == TriggerState.Chase)
                {
                    changeState(EnemyState.MoveTowardsPlayer);
                    break;
                }
                if(triggerState == TriggerState.None)
                {
                    changeState(EnemyState.Stroll);
                    break;
                }
                attack();
                break;

            case EnemyState.Flee:
                if (HP > 60)
                {
                    changeState(EnemyState.Stroll);
                    break;
                }
                addToHp(Time.deltaTime);
                flee();
                break;

            case EnemyState.Stroll:
                if(triggerState == TriggerState.Chase)
                {
                    changeState(EnemyState.MoveTowardsPlayer);
                    break;
                }
                stroll();
                if (nav.velocity.magnitude < 0.0001)
                {
                    changeState(EnemyState.Idle);
                    break;
                }
                isArrived = false;
                break;

            case EnemyState.Idle:
                if (!isArrived)
                {
                    goToNextPoint();
                    isArrived = true;
                }
                changeState(EnemyState.Stroll);
                break;

            case EnemyState.MoveTowardsPlayer:
                if(triggerState == TriggerState.None)
                {
                    changeState(EnemyState.Stroll);
                    break;
                }
                if(triggerState == TriggerState.Attack)
                {
                    changeState(EnemyState.Attack);
                }
                moveTowards(player);
                break;

            case EnemyState.Dead:
                kill();
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
        }
    }
}
