using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimEvent : MonoBehaviour
{
    public bool attackFlag = false;
    public float damage;

    private void OnTriggerStay(Collider other)
    {
        if (attackFlag && other.tag == "Enemy")
        {
            Enemy_B enemy = other.GetComponent<Enemy_B>();
            enemy.Hp -= damage;
            Debug.Log("hit");
            attackFlag = false;
        }
    }
}
