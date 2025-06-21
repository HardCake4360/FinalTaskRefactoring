using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public GameObject target;
    public GameObject blood;
    public float detectRange;
    public float attackRange;
    NavMeshAgent agent;
    Animator animator;
    public float pDist;
    public bool detected;
    private EnemyAnimEvent m_animEvent;

    void Start()
    {
        target = GameObject.Find("Player_R");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        m_animEvent = GetComponentInChildren<EnemyAnimEvent>();
        detected = false;
    }
    void Update()
    {
        pDist = getDist(transform.position, target.transform.position);
        
        if (pDist < detectRange) detected = true;
        if (detected == true) agent.destination = target.transform.position; // 쫓아갈 위치 설정

        if(pDist < attackRange)
        {
            animator.SetBool("near", true);
        }
        else animator.SetBool("near", false);

        animator.SetFloat("speed", agent.velocity.magnitude);

        if (hp <= 0) 
        {
            Destroy(gameObject);
            Instantiate(blood, transform.position, transform.rotation);
        }
    }

    float getDist(Vector3 start, Vector3 end)
    {
        return (end - start).magnitude;
    }
    void Attack()
    {
        m_animEvent.attackFlag = true;
    }
    void End()
    {
        m_animEvent.attackFlag = false;
    }

}
