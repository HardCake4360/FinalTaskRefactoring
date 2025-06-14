using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState{
    Attack,
    Flee,
    Stroll,
    Idle,
    MoveTowardsPlayer,
    Dead
}
public enum TriggerState{
    Exit,
    Chase,
    Attack,
    None
}

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyState currentState;
    public TriggerState triggerState;

    public NavMeshAgent nav;
    public GameObject target;
    public GameObject player;
    public GameObject[] strollPoints;
    public int strollIdx;
    public bool isArrived;
    public float HP;
    public float speed;

    public Vector3 destDebug;
    public Vector3 transDebug;
    public GameObject destVisual;

    public abstract void attack();
    public void flee() 
    {
        if (!target) return;
        setNav(true);
        nav.destination = (transform.position - target.transform.position).normalized*5 + transform.position;
        //degug zone########################
        destDebug = nav.destination;
        transDebug = transform.position;
        destVisual.transform.position = destDebug;
        //degug zone########################
    }
    public void stroll()
    {
        setNav(true);
        target = strollPoints[strollIdx];
        moveTowards(target);
    } 

    public void goToNextPoint()
    {
        strollIdx++;
        if (strollIdx > strollPoints.Length-1) strollIdx = 0;
    }

    public void Idle(int count)
    {
        
    }
    public void moveTowards(GameObject newTarget)
    {
        if (!newTarget) return;
        setNav(true);
        nav.destination = newTarget.transform.position;
        
    }

    public void changeState(EnemyState newState){
        currentState = newState;
    }
    public void changeTriggerState(TriggerState newState){
        triggerState = newState;
    }

    public void addToHp(float value) { HP += value; }

    public void setNav(bool flag) { nav.enabled = flag; }
    public void kill() { Destroy(gameObject); }

}
