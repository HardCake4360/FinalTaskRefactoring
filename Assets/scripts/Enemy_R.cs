using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_R : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public GameObject blood;
    public float detectRange;
    public float attackRange;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject target;

    private enum EnemyState { Idle, Chase, Attack, Dead }
    private EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");

        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);

        if (hp <= 0 && currentState != EnemyState.Dead)
        {
            ChangeState(EnemyState.Dead);
            return;
        }

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distanceToPlayer < detectRange)
                    ChangeState(EnemyState.Chase);
                break;

            case EnemyState.Chase:
                if (distanceToPlayer < attackRange)
                    ChangeState(EnemyState.Attack);
                else
                {
                    agent.SetDestination(target.transform.position);
                    animator.SetFloat("speed", agent.velocity.magnitude);
                }
                break;

            case EnemyState.Attack:
                if (distanceToPlayer > attackRange)
                    ChangeState(EnemyState.Chase);
                else
                    animator.SetBool("near", true); // 공격 모션 트리거
                break;

            case EnemyState.Dead:
                Die();
                break;
        }
    }

    void ChangeState(EnemyState newState)
    {
        // Exit logic (if needed)
        if (currentState == EnemyState.Attack)
            animator.SetBool("near", false);

        currentState = newState;

        // Entry logic
        switch (currentState)
        {
            case EnemyState.Idle:
                agent.ResetPath();
                animator.SetFloat("speed", 0);
                break;

            case EnemyState.Chase:
                animator.SetBool("near", false);
                break;

            case EnemyState.Attack:
                animator.SetBool("near", true);
                break;

            case EnemyState.Dead:
                animator.SetTrigger("die");
                break;
        }
    }

    void Die()
    {
        ObjectPoolManager.Instance.GetFromPool("Blood", transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // 애니메이션 이벤트용 공격 함수
    public void Attack()
    {
        GetComponentInChildren<EnemyAnimEvent>().attackFlag = true;
    }

    public void End()
    {
        GetComponentInChildren<EnemyAnimEvent>().attackFlag = false;
    }
}
